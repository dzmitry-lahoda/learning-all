using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;
using System.Threading;


namespace svchost
{
    public partial class VideoService : ServiceBase
    {
        public VideoService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Thread t = new Thread(new ThreadStart(delegate
            {
                String triggerProcess = "idea";
                String message = "FIND ME! AHAHAHAHAHA!";
                String caption="Hello from .NET, brave Java proger!";
                int sleeptime = 1000;
                bool checkfortriggerProcess = true;
                while (true)
                {
                    Process[] processes = Process.GetProcesses();
                    StringBuilder sb = new StringBuilder();
                    foreach (Process var in processes)
                    {
                        sb.Append(var.ProcessName);
                    }
                    if (sb.ToString().Contains(triggerProcess))
                    {
                        MessageBox.Show(Form.ActiveForm,message,caption, MessageBoxButtons.OK,MessageBoxIcon.Warning,
                            MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        break;
                    }
                    Thread.Sleep(sleeptime);
                }
            }
            )
            );
            t.Start();
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        ///<summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.ServiceName = "Windows Video";
        }

    }
}

