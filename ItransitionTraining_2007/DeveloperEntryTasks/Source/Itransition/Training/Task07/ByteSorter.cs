using System;
using System.ComponentModel;
using System.IO;

namespace Itransition.Training.Task07
{

    #region ByteSorterEventArgs class

    /// <summary>
    /// Provides data for the Itransition.Training.Task07.ByteSorter's events
    /// </summary>
    [Serializable]
    public class ByteSorterEventArgs : EventArgs
    {
        protected string message;
        protected Exception currentException;

        /// <summary>
        ///  Initializes a new instance of the 
        /// Itransition.Training.Task07.ByteSorterEventArgs class with a specified message and .
        /// </summary>
        /// <param name="message">The event message</param>
        /// <param name="exception">
        /// The exception that is the cause of the current event, or a null reference 
        /// if no exception is specified. 
        /// Is used for handling of SortingInterrupted event of Itransition.Training.Task07.ByteSorter
        /// </param>
        public ByteSorterEventArgs(String message,Exception exception)
        {
            this.message = message;
            currentException = exception;
        }

        /// <summary>
        ///  Initializes a new instance of the 
        /// Itransition.Training.Task07.ByteSorterEventArgs class with a specified message.
        /// </summary>
        /// <param name="message">The event message</param>
        public ByteSorterEventArgs(String message)
        {
            this.message = message;
            currentException = null;
        }

        /// <summary>
        /// Gets current event message
        /// </summary>
        public String Message
        {
            get { return message; }
        }

        /// <summary>
        /// Gets current exception
        /// </summary>
        public Exception CurrentExeption
        {
            get { return currentException; }
        }
    }
    #endregion

    /// <summary>
    /// Represents the method that will handle an Itransition.Training.Task07.ByteSorter's events 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e">Itransition.Training.Task07.ByteSorterEventArgs</param>
    public delegate void ByteSorterEventHandler(Object sender, ByteSorterEventArgs e);

    // TODO: <reporting options>
    #region ByteSorter class
    /// <summary>
    /// Realizes sorting of huge amount of bytes (reads data from file and writes to another file)
    /// </summary>
    public class ByteSorter
    {
        #region Fields
        private BackgroundWorker bw = new BackgroundWorker();

        /// <summary>
        /// Source filename
        /// </summary>
        protected String inputfilename;
        /// <summary>
        /// Destination filename
        /// </summary>
        protected String outputfilename;
        #endregion Fields

        #region Properties
        /// <summary>
        /// Gets or sets source filename
        /// </summary>
        public String InputFileName
        {
            get
            { 
                return inputfilename;
            }
            set 
            {
                //if (!bw.IsBusy)
                //{
                    inputfilename = value;
                //}
            }
        }

        /// <summary>
        /// Gets or sets destination filename
        /// </summary>
        public String OutputFileName
        {
            get
            {
                return outputfilename;
            }
            set
            {
                //if (!bw.IsBusy)
                //{
                    outputfilename = value;
                //}
            }
        }

        /// <summary>
        /// Get ByteSorter's condition; true if it is sorting
        /// </summary>
        public bool IsSorting
        {
            get
            {
                return bw.IsBusy;
            }
        }
        #endregion

        #region Events and EventMessages
        /// <summary>
        ///  Occurs when the Itransition.Training.Task07.ByteSorter.Start method was called
        /// </summary>
        public event ByteSorterEventHandler SortingStarted;
        /// <summary>
        ///  Occurs if the Itransition.Training.Task07.ByteSorter sorted bytes successfully 
        /// </summary>
        public event ByteSorterEventHandler SortingFinished;
        /// <summary>
        /// Occurs if bytes' sorting was interrupted
        /// </summary>
        public event ByteSorterEventHandler SortingInterrupted;
        /// <summary>
        /// Report about total percent progress of sorting
        /// </summary>
        public event ByteSorterEventHandler SortingProgressChanged;

        private const String StartedMessage = "Started.";
        private const String FinishedMessage = "Finished.";
        private const String InterruptedMessage = "Interrupted.";
        private const String CanceledMessage = "Canceled.";
        private const String SuccessfulResultMessage = "Successful result.";
        private const Int64 reportafterNbytes=5000000;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the Itransition.Training.Task08.ByteSorter class
        /// </summary>
        /// <param name="inputfilename">Source file</param>
        /// <param name="outputfilename">Destination file</param>
        public ByteSorter(String inputfilename, String outputfilename)
        {
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += bw_DoWork;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            bw.ProgressChanged += bw_ProgressChanged;
            this.inputfilename = inputfilename;
            this.outputfilename = outputfilename;
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Starts sorting
        /// </summary>
        virtual public void StartSorting()
        {
            if (!bw.IsBusy)
            {
                if (SortingStarted != null)
                {
                    SortingStarted(this,
                        new ByteSorterEventArgs(DateTime.Now.ToLongTimeString() + " " + StartedMessage));
                }
                bw.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Cancels sorting
        /// </summary>
        virtual public void CancelSorting()
        {
            bw.CancelAsync();
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (SortingProgressChanged != null)
            {
                SortingProgressChanged(this,
                    new ByteSorterEventArgs(e.ProgressPercentage.ToString()));
            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //reading bytes to array
                FileStream inputFileStream = new FileStream(InputFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                double totalprogress = inputFileStream.Length / 50.0;
                Int64 filesize = inputFileStream.Length;
                FileStream outputFileStream = new FileStream(OutputFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                Int64[] count = new Int64[256];
                int b;
                while ((b = inputFileStream.ReadByte()) > -1)
                {
                    if (inputFileStream.Position % reportafterNbytes == 0)
                    {
                        bw.ReportProgress((int)Math.Ceiling(inputFileStream.Position / totalprogress));
                        if (bw.CancellationPending)
                        {
                            inputFileStream.Close();
                            outputFileStream.Close();
                            e.Cancel = true;
                            return;
                        }
                    }
                    count[b]++;
                }
                inputFileStream.Close();
                //writing bytes to new file
                for (int i = 0; i < 256; i++)
                {
                    for (int j = 1; j <= count[i]; j++)
                    {
                        if (outputFileStream.Position % reportafterNbytes == 0)
                        {
                            bw.ReportProgress((int)Math.Ceiling((outputFileStream.Position + filesize) / totalprogress));
                            if (bw.CancellationPending)
                            {
                                outputFileStream.Close();
                                File.Delete(outputfilename);
                                e.Cancel = true;
                                return;
                            }
                        }
                        outputFileStream.WriteByte((byte)i);
                    }
                }
                outputFileStream.Close();
                bw.ReportProgress(100);
                e.Result = SuccessfulResultMessage;
            }
            catch (Exception ex)
            {
                e.Result = ex;
            }
        }


        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                SortingInterrupted(this,
                                        new ByteSorterEventArgs(DateTime.Now.ToLongTimeString()
                                        + " " + InterruptedMessage, new Exception(CanceledMessage)));   
            }

            else if (e.Error != null)
            {
                if (SortingInterrupted != null)
                {
                    SortingInterrupted(this,
                        new ByteSorterEventArgs(DateTime.Now.ToLongTimeString() 
                        + " " + InterruptedMessage, e.Error));
                }   
            }
            else if (e.Result is Exception)
            {
                if (SortingInterrupted != null)
                {
                    SortingInterrupted(this,
                        new ByteSorterEventArgs(DateTime.Now.ToLongTimeString()
                        + " " + InterruptedMessage, (Exception)e.Result));
                }   
            }
            else 
            {
                if (SortingFinished != null)
                {
                    SortingFinished(this,
                        new ByteSorterEventArgs(DateTime.Now.ToLongTimeString() + " " + FinishedMessage+" "+e.Result.ToString()));
                }
            }
        }
        #endregion Methods
    }
    #endregion ByteSorter class
}

