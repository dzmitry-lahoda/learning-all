using System;
using System.Windows.Forms;

namespace Itransition.Training.Task07
{
    // TODO: <popup controls info, CPU usage, digit percent progress>
    public partial class Task07Form : Form
    {
        private TimeSpan elapsedTimeSpan = new TimeSpan(0,0,0);
        private ByteSorter bs=new ByteSorter("","");

        public Task07Form()
        {
            bs.SortingStarted += ByteSorter_SortingStarted;
            bs.SortingFinished += ByteSorter_SortingFinished;
            bs.SortingInterrupted += ByteSorter_SortingInterrupted;
            bs.SortingProgressChanged += ByteSorter_SortingProgressChanged;
            InitializeComponent();
        }

        private void ByteSorter_SortingProgressChanged(object sender, ByteSorterEventArgs e)
        {
            progressBarSorting.Value = int.Parse(e.Message);
        }

        private void StartAvailable()
        {
            AllAvailable();
            buttonCancel.Enabled = false;
        }

        private void CancelAvailable()
        {
            NothingAvailable();
            buttonCancel.Enabled = true;
        }

        private void AllAvailable()
        {
            buttonInput.Enabled = true;
            textBoxInput.Enabled = true;
            buttonOutput.Enabled = true;
            textBoxOutput.Enabled = true;
            buttonStart.Enabled = true;
            buttonCancel.Enabled = true;
        }

        private void NothingAvailable()
        {
            buttonInput.Enabled = false;
            textBoxInput.Enabled = false;
            buttonOutput.Enabled = false;
            textBoxOutput.Enabled = false;
            buttonStart.Enabled = false;
            buttonCancel.Enabled = false;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                CancelAvailable();
                bs.InputFileName=textBoxInput.Text;
                bs.OutputFileName=textBoxOutput.Text;
                bs.StartSorting();
            }
            catch (Exception ex)
            {
                StartAvailable();
                listBoxLog.Items.Add(ex.Message);
                listBoxLog.SelectedIndex = listBoxLog.Items.Count - 1;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (bs != null)
            {
                NothingAvailable();
                bs.CancelSorting();
                StartAvailable();
            }
        }

        void ByteSorter_SortingFinished(object sender, ByteSorterEventArgs e)
        {
            timerTask07.Enabled = false;
            StartAvailable();
            listBoxLog.Items.Add(e.Message);
            listBoxLog.SelectedIndex = listBoxLog.Items.Count - 1;
        }

        void ByteSorter_SortingInterrupted(object sender, ByteSorterEventArgs e)
        {
            timerTask07.Enabled = false;
            StartAvailable();
            listBoxLog.Items.Add(e.Message + " " + e.CurrentExeption.Message);
            listBoxLog.SelectedIndex = listBoxLog.Items.Count - 1;
        }

        void ByteSorter_SortingStarted(object sender, ByteSorterEventArgs e)
        {
            CancelAvailable();
            elapsedTimeSpan = new TimeSpan(0, 0, 0);
            timerTask07.Enabled = true;
            listBoxLog.Items.Add(e.Message);
            listBoxLog.SelectedIndex = listBoxLog.Items.Count - 1;
        }

        private void buttonInput_Click(object sender, EventArgs e)
        {
            openFileDialogTask07.CheckFileExists = true;
            openFileDialogTask07.CheckPathExists = true;
            if (openFileDialogTask07.ShowDialog() == DialogResult.OK)
            {
                 textBoxInput.Text = openFileDialogTask07.FileName;
            }
        }

        private void buttonOutput_Click(object sender, EventArgs e)
        {
            openFileDialogTask07.CheckFileExists = false;
            openFileDialogTask07.CheckPathExists = false;
            if (openFileDialogTask07.ShowDialog() == DialogResult.OK)
            {
                textBoxOutput.Text = openFileDialogTask07.FileName;
            }
        }

        private void textBoxInputOutput_TextChanged(object sender, EventArgs e)
        {
            if ((textBoxInput.Text.Length == 0) || (textBoxOutput.Text.Length == 0))
            {
                buttonStart.Enabled = false;
            }
            else
            {
                buttonStart.Enabled = true;
            }
        }

        private void timerTask07_Tick(object sender, EventArgs e)
        {
            elapsedTimeSpan=elapsedTimeSpan.Add(new TimeSpan(0, 0, 1));
            labelCurrentTime.Text = elapsedTimeSpan.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bs.InputFileName = textBoxInput.Text;
            bs.OutputFileName = textBoxOutput.Text;
            bs.StartSorting();
        }

    }
}