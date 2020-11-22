using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TimeLapseExp.Cameras;

namespace TimeLapseExp.WinFormsGUI
{
    internal partial class FormExp : Form
    {

        private Dictionary<CameraApplicationStates, List<Control>> _controlGoups = 
            new Dictionary<CameraApplicationStates, List<Control>>(Enum.GetValues(typeof(CameraApplicationStates)).Length);



        private void InitizalisePreseneteEvents()
        {
            var _cameraNotConnectedControls = new List<Control>();
            var _cameraConnectedControls = new List<Control>();
            var _cameraOccupiedControls = new List<Control>();

            _controlGoups.Add(CameraApplicationStates.NotConnected, _cameraNotConnectedControls);
            _controlGoups.Add(CameraApplicationStates.Connected, _cameraConnectedControls);
            _controlGoups.Add(CameraApplicationStates.Occupied, _cameraOccupiedControls);

            AddNotConnectedControls(_cameraNotConnectedControls);
            AddConnectedControls(_cameraConnectedControls);
            AddCameraOcupiedConnectedControls(_cameraOccupiedControls);
        }



        private void SubscribeOnPresenterEvents()
        {
            Presenter.OnStoreDirectoryChanged += Presenter_OnStoreDirectoryChanged;
            Presenter.OnCurrentCameraApplicationStateChanged += Presenter_OnCurrentCameraApplicationStateChanged;
            Presenter.OnCameraIdChanged += Presenter_OnCameraIdChanged;
        }


        void Presenter_OnStoreDirectoryChanged(object sender, PresenterEventArgs e)
        {
            textBoxStoreDirectory.Text = e.Tag.ToString();
        }

  
        void Presenter_OnCurrentCameraApplicationStateChanged(object sender, PresenterEventArgs e)
        {
            switch ((CameraApplicationStates)e.Tag)
            {
                case CameraApplicationStates.NotConnected:
                    {
                        _controlGoups[CameraApplicationStates.NotConnected].ForEach(control=>control.Enabled = true);
                        _controlGoups[CameraApplicationStates.Connected].ForEach(control => control.Enabled = false);
                        _controlGoups[CameraApplicationStates.Occupied].ForEach(control => control.Enabled = false);
                        break;
                    }
                case CameraApplicationStates.Connected:
                    {
                        _controlGoups[CameraApplicationStates.NotConnected].ForEach(control => control.Enabled = false);
                        _controlGoups[CameraApplicationStates.Connected].ForEach(control => control.Enabled = true);
                        _controlGoups[CameraApplicationStates.Occupied].ForEach(control => control.Enabled = false);
                        break;
                    }
                case CameraApplicationStates.Occupied:
                    {
                        _controlGoups[CameraApplicationStates.NotConnected].ForEach(control => control.Enabled = false);
                        _controlGoups[CameraApplicationStates.Connected].ForEach(control => control.Enabled = false);
                        _controlGoups[CameraApplicationStates.Occupied].ForEach(control => control.Enabled = true);
                        break;
                    }
            }
        }

        void Presenter_OnCameraIdChanged(object sender, PresenterEventArgs e)
        {
            var id = (int) e.Tag;
            _cameraController = new OlympusRyeCameraController(id);
            Presenter.SetCurrentCameraApplicationState(CameraApplicationStates.Connected);
            _session = new Session (_cameraController );
            _session.SessionStarted += session_SessionStarted;
            _session.SequenceStarted += session_SequenceStarted;
            _session.CapturingStarted += session_CapturingStarted;
            _session.CapturingFinished += session_CapturingFinished;
            _session.TransportingStarted += session_TransportingStarted;
            _session.TransportingFinished += session_TransportingFinished;
            _session.SequenceFinished += session_SequenceFinished;
            _session.ProgressChanged += session_ProgressChanged;
            _session.SessionFinished += session_SessionFinished;
            _session.SessionCanceled += session_SessionCanceled;
        }

        private void AddNotConnectedControls(List<Control> controls)
        {
            controls.Add(buttonConnect);
           
        }

        private void AddConnectedControls(List<Control> controls)
        {
            controls.Add(buttonTestCapture);
            controls.Add(buttonSendToCamera);
            controls.Add(buttonPreview);
            //controls.Add(groupBoxCameraInfo);
            controls.Add(buttonCleanFlash);
            //controls.Add(groupBoxCaptureSettings);
            //controls.Add(comboBoxExposureMode);
           // controls.Add(comboBoxCaptureMode);
            //controls.Add(groupBoxFocusSettings);
            //controls.Add(comboBoxFocusMode);
            //controls.Add(textBoxFocusPosition);
            //controls.Add(buttonSaveToFile);
            //controls.Add(textBoxOpticalZoom);
            controls.Add(buttonSendToCamera);
            //controls.Add(comboBoxWhiteBalance);
            //controls.Add(textBoxExposureBias);
            //controls.Add(comboBoxISOSpeed);
            //controls.Add(comboBoxResolution);
            //controls.Add(comboBoxFlashMode);
            controls.Add(buttonCaptureSequence);
            //controls.Add(textBoxExposureSequenceCount);
            //controls.Add(textBoxSequenceCount);
            //controls.Add(textBoxTimeSpan);
            controls.Add(buttonCopyAllImages);
            controls.Add(buttonDisconnect);
        }

        private void AddCameraOcupiedConnectedControls(List<Control> controls)
        {
            controls.Add(buttonCancelSequence);
        }
    }
}
