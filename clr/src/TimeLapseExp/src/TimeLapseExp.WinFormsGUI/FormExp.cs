using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;

using TimeLapseExp.Instrumentation;
using TimeLapseExp.Cameras;


namespace TimeLapseExp.WinFormsGUI
{
    internal partial class FormExp : Form
    {
        private EnvironmentSetupUtility _environmentSetupUtility = new EnvironmentSetupUtility();

        private Session _session;

        private CameraControllerBase _cameraController;

        public FormExp()
        {
            //TODO:Create DisableControld method with invocation or invoke delegates depending on UserState in progres report
            CheckForIllegalCrossThreadCalls = false;
            Debug.Listeners.Add(new TextWriterTraceListener(FileNamingEngine.GetPictureNamePrefix() + ".debug"));
            InitializeComponent();
            SubscribeOnPresenterEvents();
            InitializeSettings();
            InitializeLoggers();
            InitizalisePreseneteEvents();
            Presenter.SetCurrentCameraApplicationState(CameraApplicationStates.NotConnected);
            Helper.Ahahah();
        }

        private void session_ContrastMeasurmentNumberComputed(object sender, ImageCharacteristicsEventArgs e)
        {
            if (!picturesInfo.ContainsKey(e.PictureFullName))
            {
                picturesInfo[e.PictureFullName] = new FocusExposureContrastShannon()
                {
                    Contrast = e.ReturnedValue,
                    Exposure = e.ExposureTime,
                    Focus = e.FocusPosition
                };
            }
            else
            {
                picturesInfo[e.PictureFullName].Contrast = e.ReturnedValue;
            }
            NumberFormatInfo numberFormatInfo = CultureInfo.InvariantCulture.NumberFormat;
            textBoxContrastMeasurment.Text = e.ReturnedValue.ToString("F6", numberFormatInfo);
            _loggerContrastMeasurment.Info(e.ReturnedValue.ToString("F6", numberFormatInfo) + " " + e.PictureFullName);
            //ContrastMeasurmentLogger.WriteLine(e.ReturnedValue.ToString("F6", numberFormatInfo) + " " + e.PictureFullName, LoggingLevel.Info);
        }

        [Serializable]
        internal class FocusExposureContrastShannon
        {
            private int _focus = Int32.MinValue;
            private int _exposure = Int32.MinValue;
            private double _contrast = Double.NaN;
            private double _shannon = Double.NaN;

            public double Shannon
            {
                get { return _shannon; }
                set { _shannon = value; }
            }

            public double Contrast
            {
                get { return _contrast; }
                set { _contrast = value; }
            }

            public int Exposure
            {
                get { return _exposure; }
                set { _exposure = value; }
            }

            public int Focus
            {
                get { return _focus; }
                set { _focus = value; }
            }

            public bool Equals(FocusExposureContrastShannon other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return other._shannon == _shannon && other._contrast == _contrast && other._exposure == _exposure && other._focus == _focus;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != typeof(FocusExposureContrastShannon)) return false;
                return Equals((FocusExposureContrastShannon)obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    int result = _shannon.GetHashCode();
                    result = (result * 397) ^ _contrast.GetHashCode();
                    result = (result * 397) ^ _exposure;
                    result = (result * 397) ^ _focus;
                    return result;
                }
            }
        }

        private Dictionary<string, FocusExposureContrastShannon> picturesInfo = new Dictionary<string, FocusExposureContrastShannon>();

        private void session_ShannonEntropyComputed(object sender, ImageCharacteristicsEventArgs e)
        {
            if (!picturesInfo.ContainsKey(e.PictureFullName))
            {
                picturesInfo[e.PictureFullName] = new FocusExposureContrastShannon()
                                                      {
                                                          Shannon = e.ReturnedValue,
                                                          Exposure = e.ExposureTime,
                                                          Focus = e.FocusPosition
                                                      };
            }
            else
            {
                picturesInfo[e.PictureFullName].Shannon = e.ReturnedValue;
            }
            var numberFormatInfo = CultureInfo.InvariantCulture.NumberFormat;
            textBoxShannonEntropy.Text = e.ReturnedValue.ToString("F6", numberFormatInfo);
            _loggerShannonEntropy.Info(e.ReturnedValue.ToString("F6", numberFormatInfo) + " " + e.PictureFullName);
        }

        private void session_SessionCanceled(object sender, SessionEventArgs e)
        {
            SessionEnded(e);
        }

        private void SessionEnded(SessionEventArgs e)
        {
            timerEx.Stop();

            _loggerSession.Info(e.Message + " " + e.DateTime);
            _loggerSession.Info(ExperimentDelimeter);
            EnableCameraConnect();
            
            _session.ShannonEntropyComputed -= session_ShannonEntropyComputed;
            _session.ContrastMeasurmentNumberComputed -= session_ContrastMeasurmentNumberComputed;
            var enumerator = picturesInfo.Values.GetEnumerator();
            switch (picturesInfo.Count)
            {
                case 0:
                    break; 
                case 1:
                    enumerator.MoveNext();
                    textBoxShannonEntropy.Text = enumerator.Current.Shannon.ToString();
                    textBoxContrastMeasurment.Text = enumerator.Current.Contrast.ToString();
                    break;
                default:
                    var numberOfBestResults = 3;
                    var data = GetNBestExposuresBasedOnShannonEntropy(numberOfBestResults);
                    _loggerShannonEntropy.Info("Best Shannon entropy was obtained using such exposure times:");
                    foreach (var focusExposureContrastShannon in data)
                    {
                        _loggerShannonEntropy.Info(focusExposureContrastShannon.Value.Exposure + "( " + focusExposureContrastShannon.Value.Shannon + " " + focusExposureContrastShannon.Key + " )");
                    }
                    
                    break;
            }

            _loggerContrastMeasurment.Info(ExperimentDelimeter);
            _loggerShannonEntropy.Info(ExperimentDelimeter);
            Presenter.SetCurrentCameraApplicationState(CameraApplicationStates.Connected);
            picturesInfo.Clear();
        }

        /// <summary>
        /// Gets several <see cref="FocusExposureContrastShannon"/> which has maximum Shannon entropy.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private Dictionary<string,FocusExposureContrastShannon> GetNBestExposuresBasedOnShannonEntropy(int numberOfBestResults)
        {
            if (numberOfBestResults > picturesInfo.Count)
            {
                return new Dictionary<string,FocusExposureContrastShannon>(picturesInfo);
            }
            Helper.ShowChart("Shannon Entropy", picturesInfo);

            var data = new Dictionary<string, FocusExposureContrastShannon>(numberOfBestResults);
            for (var i = 0; i < numberOfBestResults; i++)
            {
                var key = "";
                var max = Double.NegativeInfinity;
                foreach (var info in picturesInfo)
                {
                    if (info.Value.Shannon >= max)
                    {
                        max = info.Value.Shannon;
                        key = info.Key;
                    }
                }
                data.Add(key,picturesInfo[key]);
                picturesInfo.Remove(key);
            }
            return data;

        }

        private void session_SessionFinished(object sender, SessionEventArgs e)
        {
            SessionEnded(e);
        }

        private void session_ProgressChanged(object sender, SessionEventArgs e)
        {
            progressBarCapturingProgress.Value = Int32.Parse(e.Message);
        }

        private void session_SequenceFinished(object sender, SessionEventArgs e)
        {
            _loggerSession.Debug(e.Message + " " + e.DateTime);
        }

        private void session_TransportingFinished(object sender, SessionEventArgs e)
        {
            _loggerSession.Debug(e.Message + " " + e.DateTime);
        }

        private void session_TransportingStarted(object sender, SessionEventArgs e)
        {
            _loggerSession.Debug(e.Message + " " + e.DateTime);
        }

        private void session_CapturingFinished(object sender, SessionEventArgs e)
        {
            _loggerSession.Trace(e.Message + " " + e.DateTime);
        }

        private void session_CapturingStarted(object sender, SessionEventArgs e)
        {
            _loggerSession.Trace(e.Message + " " + e.DateTime);
        }

        private void session_SequenceStarted(object sender, SessionEventArgs e)
        {
            _loggerSession.Debug(e.Message + " " + e.DateTime);
        }

        private void session_SessionStarted(object sender, SessionEventArgs e)
        {
            progressBarCapturingProgress.Value = 0;
            picturesInfo = new Dictionary<string, FocusExposureContrastShannon>();
            labelElapsedTime.Text = TimeSpan.FromSeconds(0).ToString();
            labelRemainingTime.Text = TimeSpan.FromSeconds(_session.SessionInfo.SequenceCount * _session.SessionInfo.TimeSpanBetweenSequences.TotalSeconds).ToString();
            timerEx.Start();
            DisableCameraConnect();

            _loggerShannonEntropy.Info(ExperimentDelimeter);
            _loggerContrastMeasurment.Info(ExperimentDelimeter);
            _loggerSession.Info(e.Message + " " + e.DateTime);
        }



        private void RefreshCameraList()
        {
            var dictionary = OlympusRyeCameraController.GetCameras();
            var strings = new String[dictionary.Count];
            dictionary.Values.CopyTo(strings, 0);
            listBoxCamera.Items.Clear();
            listBoxCamera.Items.AddRange(strings);
            if (listBoxCamera.Items.Count > 0)
            {
                listBoxCamera.SelectedIndex = 0;
            }
        }

        private void FormEx_Load(object sender, EventArgs e)
        {
            RefreshCameraList();
        }


        private void tabPageCapture_Enter(object sender, EventArgs e)
        {
            if (listBoxCamera.SelectedIndex >= 0 && !_session.IsCapturing)
            {
                groupBoxSequenceCapture.Enabled = true;
                groupBoxCameraInfo.Enabled = true;
                groupBoxCameraInfo.Enabled = true;
                InicializeComponents();
                FillComponents();
                EnableCapturing();
            }
            else
            {
                groupBoxSequenceCapture.Enabled = false;
                groupBoxCameraInfo.Enabled = false;
                groupBoxCameraInfo.Enabled = false;
            }
        }

        private void FillComponents()
        {
            var cameraInfo = _cameraController.GetAllCameraInfo();
            comboBoxCaptureMode.SelectedItem = cameraInfo.CaptureInfo.Capture.Mode.ToString();
            comboBoxResolution.SelectedItem = cameraInfo.CaptureInfo.Resolution.ToString();
            comboBoxFlashMode.SelectedItem = cameraInfo.CaptureInfo.FlashMode.ToString();
            comboBoxExposureMode.SelectedItem = cameraInfo.CaptureInfo.Exposure.Mode.ToString();
            comboBoxWhiteBalance.SelectedItem = cameraInfo.CaptureInfo.WhiteBalance.ToString();
            comboBoxISOSpeed.SelectedItem = cameraInfo.CaptureInfo.ISOSpeed.ToString();
            comboBoxFocusMode.SelectedItem = cameraInfo.CaptureInfo.Focus.Mode.ToString();
            textBoxContinuousTime.Text = cameraInfo.CaptureInfo.Capture.ContinuousTime.ToString();
            textBoxContinuousNumber.Text = cameraInfo.CaptureInfo.Capture.ContinuousNumber.ToString();
            textBoxAperture.Text = cameraInfo.CaptureInfo.Exposure.Aperture.ToString();
            textBoxExposureTime.Text = cameraInfo.CaptureInfo.Exposure.Time.ToString();
            textBoxOpticalZoom.Text = cameraInfo.CaptureInfo.OpticalZoom.ToString();
            textBoxExposureBias.Text = cameraInfo.CaptureInfo.ExposureBias.ToString();
            textBoxFocusPosition.Text = cameraInfo.CaptureInfo.Focus.Position.ToString();
        }

        private void InicializeComponents()
        {
            comboBoxResolution.Items.Clear();
            comboBoxResolution.Items.AddRange(Enum.GetNames(typeof(Resolutions)));

            comboBoxFlashMode.Items.Clear();
            comboBoxFlashMode.Items.AddRange(Enum.GetNames(typeof(FlashModes)));

            comboBoxExposureMode.Items.Clear();
            comboBoxExposureMode.Items.AddRange(Enum.GetNames(typeof(ExposureModes)));

            comboBoxWhiteBalance.Items.Clear();
            comboBoxWhiteBalance.Items.AddRange(Enum.GetNames(typeof(WhiteBalance)));

            comboBoxISOSpeed.Items.Clear();
            comboBoxISOSpeed.Items.AddRange(Enum.GetNames(typeof(ISOSpeed)));

            comboBoxCaptureMode.Items.Clear();
            comboBoxCaptureMode.Items.AddRange(Enum.GetNames(typeof(CaptureModes)));

            comboBoxFocusMode.Items.Clear();
            comboBoxFocusMode.Items.AddRange(Enum.GetNames(typeof(FocusModes)));
        }

        private void buttonSendToCamera_Click(object sender, EventArgs e)
        {
            SendComponents();
            FillComponents();
            EnableCapturing();
        }

        private void EnableCapturing()
        {
            buttonCaptureSequence.Enabled = true;
            buttonTestCapture.Enabled = true;
        }

        private void DisableCapturing()
        {
            buttonCaptureSequence.Enabled = false;
            buttonTestCapture.Enabled = false;
        }

        private CameraInfo SendComponents()
        {
            var cameraInfo = _cameraController.GetAllCameraInfo();
            cameraInfo.CaptureInfo.Capture.Mode = (CaptureModes)Enum.Parse(typeof(CaptureModes), comboBoxCaptureMode.SelectedItem.ToString());
            cameraInfo.CaptureInfo.Resolution = (Resolutions)Enum.Parse(typeof(Resolutions), comboBoxResolution.SelectedItem.ToString());
            cameraInfo.CaptureInfo.FlashMode = (FlashModes)Enum.Parse(typeof(FlashModes), comboBoxFlashMode.SelectedItem.ToString());
            cameraInfo.CaptureInfo.Exposure.Mode = (ExposureModes)Enum.Parse(typeof(ExposureModes), comboBoxExposureMode.SelectedItem.ToString());
            cameraInfo.CaptureInfo.WhiteBalance = (WhiteBalance)Enum.Parse(typeof(WhiteBalance), comboBoxWhiteBalance.SelectedItem.ToString());
            cameraInfo.CaptureInfo.ISOSpeed = (ISOSpeed)Enum.Parse(typeof(ISOSpeed), comboBoxISOSpeed.SelectedItem.ToString());
            cameraInfo.CaptureInfo.Focus.Mode = (FocusModes)Enum.Parse(typeof(FocusModes), comboBoxFocusMode.SelectedItem.ToString());
            cameraInfo.CaptureInfo.Exposure.Aperture = Int32.Parse(textBoxAperture.Text);
            cameraInfo.CaptureInfo.Exposure.Time = Int32.Parse(textBoxExposureTime.Text);
            cameraInfo.CaptureInfo.OpticalZoom = Int32.Parse(textBoxOpticalZoom.Text);
            cameraInfo.CaptureInfo.ExposureBias = Int32.Parse(textBoxExposureBias.Text);
            cameraInfo.CaptureInfo.Focus.Position = Int32.Parse(textBoxFocusPosition.Text);
            cameraInfo.CaptureInfo.Capture.ContinuousNumber = Int32.Parse(textBoxContinuousNumber.Text);
            cameraInfo.CaptureInfo.Capture.ContinuousTime = Int32.Parse(textBoxContinuousTime.Text);
            _cameraController.SetAllCameraInfo(cameraInfo);
            return cameraInfo;
        }





        private void buttonCleanFlash_Click(object sender, EventArgs e)
        {
            _cameraController.Transport.EraseAllPictures();
        }

        private void textBoxPicturesPerSequence_ValueChanged(object sender, EventArgs e)
        {

        }

        private void buttonInstallDriver_Click(object sender, EventArgs e)
        {
            _environmentSetupUtility.SetupDriver();
        }

        private void buttonRegisterCamera_Click(object sender, EventArgs e)
        {
            _environmentSetupUtility.InstallCamera();
        }

        private void textBoxExposureTimeStep_Validating(object sender, CancelEventArgs e)
        {
            if (IsExposureTimeStepValid())
            {
                errorProviderEx.SetError(textBoxExposureTimeStep, "");
            }
            else
            {
                errorProviderEx.SetError(textBoxExposureTimeStep, "Should be >0, because exposure sequence count>1!");
            }
        }

        private bool IsExposureTimeStepValid()
        {
            if (Int32.Parse(textBoxExposureSequenceCount.Text) > 1 && Int32.Parse(textBoxExposureTimeStep.Text) == 0)
            {
                return false;
            }
            return true;
        }

        private void textBoxFocusPositionStep_Validating(object sender, CancelEventArgs e)
        {
            if (IsFocusPositionValid())
            {
                errorProviderEx.SetError(textBoxFocusPositionStep, "");
            }
            else
            {
                errorProviderEx.SetError(textBoxFocusPositionStep, "Should be >0, because focus sequence count>1!");
            }
        }

        private bool IsFocusPositionValid()
        {
            if (Int32.Parse(textBoxFocusSequenceCount.Text) > 1 && Int32.Parse(textBoxFocusPositionStep.Text) == 0)
            {
                return false;
            }
            return true;
        }

        private void comboBoxFocusMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisableCapturing();
        }

        private void textBoxFocusPosition_ValueChanged(object sender, EventArgs e)
        {
            DisableCapturing();
        }

        private void comboBoxExposureMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisableCapturing();
        }

        private void textBoxExposureTime_ValueChanged(object sender, EventArgs e)
        {
            DisableCapturing();
        }

        private void textBoxAperture_ValueChanged(object sender, EventArgs e)
        {
            DisableCapturing();
        }

        private void comboBoxCaptureMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisableCapturing();
        }

        private void textBoxContinuousTime_ValueChanged(object sender, EventArgs e)
        {
            DisableCapturing();
        }

        private void textBoxContinuousNumber_ValueChanged(object sender, EventArgs e)
        {
            DisableCapturing();
        }

        private void comboBoxFlashMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisableCapturing();
        }

        private void comboBoxResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisableCapturing();
        }

        private void comboBoxISOSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisableCapturing();
        }

        private void comboBoxWhiteBalance_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisableCapturing();
        }

        private void textBoxOpticalZoom_ValueChanged(object sender, EventArgs e)
        {
            DisableCapturing();
        }

        private void textBoxExposureBias_ValueChanged(object sender, EventArgs e)
        {
            DisableCapturing();
        }

        private void LogLevelChanged()
        {
            if (radioButtonInfo.Checked)
            {
                //_sessionLogger.CurrentLoggingLevel = LoggingLevel.Info;
                return;
            }
            if (radioButtonDebug.Checked)
            {
                //_loggerSession.CurrentLoggingLevel = LoggingLevel.Debug;
                return;
            }
            //_loggerSession.CurrentLoggingLevel = LoggingLevel.Trace;
        }

        private void FormEx_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSettings();
        }

        private void buttonCancelSequence_Click(object sender, EventArgs e)
        {
            _session.Cancel();
            Presenter.SetCurrentCameraApplicationState(CameraApplicationStates.Connected);
        }

        private void radioButtonLog_CheckedChanged(object sender, EventArgs e)
        {
            LogLevelChanged();
        }

        private void buttonClearLog_Click(object sender, EventArgs e)
        {
           richTextBoxSession.Clear();
        }

        private void buttonClearJ_Click(object sender, EventArgs e)
        {
            richTextBoxContrastMeasurment.Clear();
        }

        private void EnableCameraConnect()
        {
            groupBoxSequenceCapture.Enabled = true;
            groupBoxCameraInfo.Enabled = true;
            groupBoxCameraInfo.Enabled = true;
            tabPageCamera.Enabled = true;
        }

        private void DisableCameraConnect()
        {
            groupBoxSequenceCapture.Enabled = false;
            groupBoxCameraInfo.Enabled = false;
            groupBoxCameraInfo.Enabled = false;
            tabPageCamera.Enabled = false;
        }

        private void timerEx_Tick(object sender, EventArgs e)
        {
            labelElapsedTime.Text = (TimeSpan.Parse(labelElapsedTime.Text) + TimeSpan.FromSeconds(1)).ToString();
            labelRemainingTime.Text = (TimeSpan.Parse(labelRemainingTime.Text) - TimeSpan.FromSeconds(1)).ToString();
        }

        private void groupBoxCameraInfo_Enter(object sender, EventArgs e)
        {

        }

        private void buttonCopyAllImages_Click(object sender, EventArgs e)
        {
           _cameraController.Transport.GetAllPictures(true, Model.StoreDirectory,false);
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            var id = listBoxCamera.SelectedIndex;
            Presenter.SetCameraId(id);
        }

        private void buttonPreview_Click(object sender, EventArgs e)
        {
            _cameraController.Preview();
            var fileName = _cameraController.Transport.GetPreview(Model.StoreDirectory);
            var previewBitmap = new Bitmap(fileName);
            pictureBoxPreview.Image = previewBitmap;
        }



    }
}