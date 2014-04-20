namespace Itransition.Training.Task07
{
    partial class Task07Form
    {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.openFileDialogTask07 = new System.Windows.Forms.OpenFileDialog();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.labelInput = new System.Windows.Forms.Label();
            this.labelOutput = new System.Windows.Forms.Label();
            this.buttonInput = new System.Windows.Forms.Button();
            this.buttonOutput = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.progressBarSorting = new System.Windows.Forms.ProgressBar();
            this.labelTimeElapsed = new System.Windows.Forms.Label();
            this.labelTotalProgress = new System.Windows.Forms.Label();
            this.labelCurrentTime = new System.Windows.Forms.Label();
            this.timerTask07 = new System.Windows.Forms.Timer(this.components);
            this.buttonTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxInput
            // 
            this.textBoxInput.AccessibleDescription = "";
            this.textBoxInput.AllowDrop = true;
            this.textBoxInput.BackColor = System.Drawing.Color.White;
            this.textBoxInput.Location = new System.Drawing.Point(57, 16);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(581, 20);
            this.textBoxInput.TabIndex = 1;
            this.textBoxInput.WordWrap = false;
            this.textBoxInput.DoubleClick += new System.EventHandler(this.buttonInput_Click);
            this.textBoxInput.TextChanged += new System.EventHandler(this.textBoxInputOutput_TextChanged);
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.AllowDrop = true;
            this.textBoxOutput.BackColor = System.Drawing.Color.White;
            this.textBoxOutput.Location = new System.Drawing.Point(57, 55);
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.Size = new System.Drawing.Size(581, 20);
            this.textBoxOutput.TabIndex = 2;
            this.textBoxOutput.WordWrap = false;
            this.textBoxOutput.DoubleClick += new System.EventHandler(this.buttonOutput_Click);
            this.textBoxOutput.TextChanged += new System.EventHandler(this.textBoxInputOutput_TextChanged);
            // 
            // labelInput
            // 
            this.labelInput.AutoSize = true;
            this.labelInput.Location = new System.Drawing.Point(12, 19);
            this.labelInput.Name = "labelInput";
            this.labelInput.Size = new System.Drawing.Size(34, 13);
            this.labelInput.TabIndex = 3;
            this.labelInput.Text = "Input:";
            // 
            // labelOutput
            // 
            this.labelOutput.AutoSize = true;
            this.labelOutput.Location = new System.Drawing.Point(12, 58);
            this.labelOutput.Name = "labelOutput";
            this.labelOutput.Size = new System.Drawing.Size(42, 13);
            this.labelOutput.TabIndex = 4;
            this.labelOutput.Text = "Output:";
            // 
            // buttonInput
            // 
            this.buttonInput.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.buttonInput.FlatAppearance.BorderSize = 2;
            this.buttonInput.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buttonInput.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buttonInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonInput.Location = new System.Drawing.Point(655, 13);
            this.buttonInput.Name = "buttonInput";
            this.buttonInput.Size = new System.Drawing.Size(64, 25);
            this.buttonInput.TabIndex = 5;
            this.buttonInput.Text = "Browse...";
            this.buttonInput.UseVisualStyleBackColor = true;
            this.buttonInput.Click += new System.EventHandler(this.buttonInput_Click);
            // 
            // buttonOutput
            // 
            this.buttonOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOutput.Location = new System.Drawing.Point(655, 52);
            this.buttonOutput.Name = "buttonOutput";
            this.buttonOutput.Size = new System.Drawing.Size(64, 25);
            this.buttonOutput.TabIndex = 6;
            this.buttonOutput.Text = "Browse...";
            this.buttonOutput.UseVisualStyleBackColor = true;
            this.buttonOutput.Click += new System.EventHandler(this.buttonOutput_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Enabled = false;
            this.buttonStart.Location = new System.Drawing.Point(655, 172);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(64, 30);
            this.buttonStart.TabIndex = 7;
            this.buttonStart.Text = "&Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Enabled = false;
            this.buttonCancel.Location = new System.Drawing.Point(655, 208);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(64, 30);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // listBoxLog
            // 
            this.listBoxLog.BackColor = System.Drawing.Color.LemonChiffon;
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.Location = new System.Drawing.Point(15, 172);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(623, 121);
            this.listBoxLog.TabIndex = 10;
            // 
            // progressBarSorting
            // 
            this.progressBarSorting.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.progressBarSorting.Location = new System.Drawing.Point(15, 117);
            this.progressBarSorting.Name = "progressBarSorting";
            this.progressBarSorting.Size = new System.Drawing.Size(623, 23);
            this.progressBarSorting.Step = 5;
            this.progressBarSorting.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarSorting.TabIndex = 15;
            // 
            // labelTimeElapsed
            // 
            this.labelTimeElapsed.AutoSize = true;
            this.labelTimeElapsed.Location = new System.Drawing.Point(12, 143);
            this.labelTimeElapsed.Name = "labelTimeElapsed";
            this.labelTimeElapsed.Size = new System.Drawing.Size(71, 13);
            this.labelTimeElapsed.TabIndex = 17;
            this.labelTimeElapsed.Text = "TimeElapsed:";
            // 
            // labelTotalProgress
            // 
            this.labelTotalProgress.AutoSize = true;
            this.labelTotalProgress.Location = new System.Drawing.Point(12, 101);
            this.labelTotalProgress.Name = "labelTotalProgress";
            this.labelTotalProgress.Size = new System.Drawing.Size(78, 13);
            this.labelTotalProgress.TabIndex = 18;
            this.labelTotalProgress.Text = "Total Progress:";
            // 
            // labelCurrentTime
            // 
            this.labelCurrentTime.AutoSize = true;
            this.labelCurrentTime.Location = new System.Drawing.Point(89, 143);
            this.labelCurrentTime.Name = "labelCurrentTime";
            this.labelCurrentTime.Size = new System.Drawing.Size(0, 13);
            this.labelCurrentTime.TabIndex = 19;
            // 
            // timerTask07
            // 
            this.timerTask07.Interval = 1000;
            this.timerTask07.Tick += new System.EventHandler(this.timerTask07_Tick);
            // 
            // buttonTest
            // 
            this.buttonTest.Enabled = false;
            this.buttonTest.Location = new System.Drawing.Point(655, 117);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(64, 30);
            this.buttonTest.TabIndex = 20;
            this.buttonTest.Text = "Test";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Visible = false;
            this.buttonTest.Click += new System.EventHandler(this.button1_Click);
            // 
            // Task07Form
            // 
            this.AcceptButton = this.buttonStart;
            this.BackColor = System.Drawing.Color.Silver;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(740, 303);
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.labelCurrentTime);
            this.Controls.Add(this.labelTotalProgress);
            this.Controls.Add(this.labelTimeElapsed);
            this.Controls.Add(this.textBoxInput);
            this.Controls.Add(this.textBoxOutput);
            this.Controls.Add(this.labelInput);
            this.Controls.Add(this.labelOutput);
            this.Controls.Add(this.buttonInput);
            this.Controls.Add(this.buttonOutput);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.listBoxLog);
            this.Controls.Add(this.progressBarSorting);
            this.ForeColor = System.Drawing.Color.Black;
            this.MaximizeBox = false;
            this.Name = "Task07Form";
            this.Text = "Byte Sorter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialogTask07;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.Label labelInput;
        private System.Windows.Forms.Label labelOutput;
        private System.Windows.Forms.Button buttonInput;
        private System.Windows.Forms.Button buttonOutput;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.ProgressBar progressBarSorting;
        private System.Windows.Forms.Label labelTimeElapsed;
        private System.Windows.Forms.Label labelTotalProgress;
        private System.Windows.Forms.Label labelCurrentTime;
        private System.Windows.Forms.Timer timerTask07;
        private System.Windows.Forms.Button buttonTest;
    }
}