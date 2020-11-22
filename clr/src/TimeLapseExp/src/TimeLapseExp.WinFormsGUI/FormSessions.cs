using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using TimeLapseExp.Instrumentation;


namespace TimeLapseExp.WinFormsGUI
{
    internal partial class FormExp : Form
    {
        private void buttonCaptureSequence_Click(object sender, EventArgs e)
        {
            if (IsFocusPositionValid() && IsExposureTimeStepValid())
            {
                if (!_session.IsCapturing)
                {
                    //try
                    {
                        CheckCheckBoxes();
                        SendComponents();
                        var cameraInfo = _cameraController.GetAllCameraInfo();
                        var sequenceInfo = new SequenceInfo(
                            cameraInfo,
                            (radioButtonExposurePriority.Checked ? SequencePriorities.Exposure : SequencePriorities.Focus),
                                                                                 Int32.Parse(textBoxExposureSequenceCount.Text),
                                      Int32.Parse(textBoxExposureTimeStep.Text),
                                      Int32.Parse(textBoxFocusSequenceCount.Text),
                                            Int32.Parse(textBoxFocusPositionStep.Text)

                            );
                        var sessionInfo = new SessionInfo(Int32.Parse(textBoxSequenceCount.Text),
                           TimeSpan.FromSeconds(Int32.Parse(textBoxTimeSpan.Text)),
                           sequenceInfo,
                           Model.StoreDirectory,
                            checkBoxCopyImages.Checked
                                       );
                        labelElapsedTime.Text = TimeSpan.FromSeconds(0).ToString();
                        labelRemainingTime.Text = TimeSpan.FromSeconds(sessionInfo.SequenceCount * sessionInfo.TimeSpanBetweenSequences.TotalSeconds).ToString();
                        progressBarCapturingProgress.Value = 0;
                        timerEx.Enabled = true;
                        timerEx.Start();

                        DoBeforeStartCapturingSetups(sessionInfo);
                        Presenter.SetCurrentCameraApplicationState(CameraApplicationStates.Occupied);
                        InitializeLoggers();
                        _session.Start(sessionInfo);
                        tabControlMain.SelectTab(tabPageCapturing);
                    }
                    //catch (Exception ex)
                    {
                        //Debug.WriteLine(ex);
                   }
                }
                else
                {
                    _loggerSession.Info("Wait. Still is capturing!");
                }
            }
        }


        private void buttonTestCapture_Click(object sender, EventArgs e)
        {
            CheckCheckBoxes();
            SendComponents();

            var cameraInfo = _cameraController.GetAllCameraInfo();
            var sequenceInfo = new SequenceInfo(cameraInfo,SequencePriorities.Exposure,1,0,1,0);
            var sessionInfo = new SessionInfo(1, new TimeSpan(0,0,0,60), sequenceInfo, Model.StoreDirectory, true);
            DoBeforeStartCapturingSetups(sessionInfo);
            
            _session.Start(sessionInfo);

        }
    }
}
