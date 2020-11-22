using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using TimeLapseExp.Cameras;
using TimeLapseExp.Instrumentation;

namespace TimeLapseExp.WinFormsGUI
{
    internal partial class FormExp:Form
    {
        private void buttonSaveApplicationSettings_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void InitializeSettings()
        {
            Presenter.SetStoreDirectory(Settings.Default.StoreDirectory);
        }

        private void InitializePresenterControls()
        {

        }

        private void pictureBoxChooseStoreDirectory_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialogEx.ShowDialog() == DialogResult.OK)
            {
                textBoxStoreDirectory.Text = folderBrowserDialogEx.SelectedPath;
            }
        }

        private void textBoxStoreDirectory_TextChanged(object sender, EventArgs e)
        {
            Presenter.SetStoreDirectory(textBoxStoreDirectory.Text);
        }

        private void SaveSettings()
        {
            Settings.Default["StoreDirectory"] = Model.StoreDirectory;
            Settings.Default.Save();
        }


        private void CheckCheckBoxes()
        {
            if (checkBoxEraseAllPictures.Checked)
            {
                _cameraController.Transport.EraseAllPictures(); 
            }
            if (checkBoxShannonEntropy.Checked)
            {
                _session.ShannonEntropyComputed += session_ShannonEntropyComputed;
            }
            if (checkBoxlContrastMeasurment.Checked)
            {
                _session.ContrastMeasurmentNumberComputed += session_ContrastMeasurmentNumberComputed;
            }
        }


    }

       
}
