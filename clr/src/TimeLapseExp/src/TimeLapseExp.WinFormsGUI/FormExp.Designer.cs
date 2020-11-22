using System.Windows.Forms;

namespace TimeLapseExp.WinFormsGUI
{
    partial class FormExp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormExp));
            this.groupBoxCameraInfo = new System.Windows.Forms.GroupBox();
            this.buttonCopyAllImages = new System.Windows.Forms.Button();
            this.textBoxContrastMeasurment = new System.Windows.Forms.TextBox();
            this.textBoxShannonEntropy = new System.Windows.Forms.TextBox();
            this.labelShannonEntropy = new System.Windows.Forms.Label();
            this.labelContrastMeasurment = new System.Windows.Forms.Label();
            this.buttonCleanFlash = new System.Windows.Forms.Button();
            this.groupBoxExposureSettings = new System.Windows.Forms.GroupBox();
            this.textBoxAperture = new System.Windows.Forms.NumericUpDown();
            this.textBoxExposureTime = new System.Windows.Forms.NumericUpDown();
            this.labelExposureMode = new System.Windows.Forms.Label();
            this.labelAperture = new System.Windows.Forms.Label();
            this.labelExposureTime = new System.Windows.Forms.Label();
            this.comboBoxExposureMode = new System.Windows.Forms.ComboBox();
            this.groupBoxCaptureSettings = new System.Windows.Forms.GroupBox();
            this.textBoxContinuousNumber = new System.Windows.Forms.NumericUpDown();
            this.textBoxContinuousTime = new System.Windows.Forms.NumericUpDown();
            this.labelContinuousNumber = new System.Windows.Forms.Label();
            this.labelContinuousTime = new System.Windows.Forms.Label();
            this.labelCaptureMode = new System.Windows.Forms.Label();
            this.comboBoxCaptureMode = new System.Windows.Forms.ComboBox();
            this.groupBoxFocusSettings = new System.Windows.Forms.GroupBox();
            this.comboBoxFocusMode = new System.Windows.Forms.ComboBox();
            this.labelFocusMode = new System.Windows.Forms.Label();
            this.textBoxFocusPosition = new System.Windows.Forms.NumericUpDown();
            this.labelFocusPosition = new System.Windows.Forms.Label();
            this.buttonTestCapture = new System.Windows.Forms.Button();
            this.buttonSaveToFile = new System.Windows.Forms.Button();
            this.textBoxOpticalZoom = new System.Windows.Forms.NumericUpDown();
            this.labelOpticalZoom = new System.Windows.Forms.Label();
            this.buttonSendToCamera = new System.Windows.Forms.Button();
            this.comboBoxWhiteBalance = new System.Windows.Forms.ComboBox();
            this.textBoxExposureBias = new System.Windows.Forms.NumericUpDown();
            this.comboBoxISOSpeed = new System.Windows.Forms.ComboBox();
            this.comboBoxResolution = new System.Windows.Forms.ComboBox();
            this.labelWhiteBalance = new System.Windows.Forms.Label();
            this.labelISOSpeed = new System.Windows.Forms.Label();
            this.labelResolution = new System.Windows.Forms.Label();
            this.labelExposureBias = new System.Windows.Forms.Label();
            this.labelFlashMode = new System.Windows.Forms.Label();
            this.comboBoxFlashMode = new System.Windows.Forms.ComboBox();
            this.menuStripEx = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemCalculator = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpenCurrentStoreDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundWorkerEx = new System.ComponentModel.BackgroundWorker();
            this.timerStatusStripEx = new System.Windows.Forms.Timer(this.components);
            this.toolTipEx = new System.Windows.Forms.ToolTip(this.components);
            this.statusStripEx = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBarInicialization = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabelError = new System.Windows.Forms.ToolStripStatusLabel();
            this.folderBrowserDialogEx = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageCamera = new System.Windows.Forms.TabPage();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonPreview = new System.Windows.Forms.Button();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.buttonRegisterCamera = new System.Windows.Forms.Button();
            this.buttonInstallDriver = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.listBoxCamera = new System.Windows.Forms.ListBox();
            this.tabPageCaptureSettings = new System.Windows.Forms.TabPage();
            this.checkBoxPlotShannonEntropy = new System.Windows.Forms.CheckBox();
            this.checkBoxEraseAllPictures = new System.Windows.Forms.CheckBox();
            this.groupBoxSequenceCapture = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonCaptureSequence = new System.Windows.Forms.Button();
            this.textBoxFocusPositionStep = new System.Windows.Forms.NumericUpDown();
            this.labelFocusPositionStep = new System.Windows.Forms.Label();
            this.textBoxFocusSequenceCount = new System.Windows.Forms.NumericUpDown();
            this.labelFocusSequenceCount = new System.Windows.Forms.Label();
            this.radioButtonFocusPriority = new System.Windows.Forms.RadioButton();
            this.textBoxSequenceCount = new System.Windows.Forms.NumericUpDown();
            this.radioButtonExposurePriority = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTimeSpan = new System.Windows.Forms.NumericUpDown();
            this.labeExposureSequenceCount = new System.Windows.Forms.Label();
            this.textBoxExposureTimeStep = new System.Windows.Forms.NumericUpDown();
            this.labelExposureTimeStep = new System.Windows.Forms.Label();
            this.textBoxExposureSequenceCount = new System.Windows.Forms.NumericUpDown();
            this.checkBoxCopyImages = new System.Windows.Forms.CheckBox();
            this.checkBoxlContrastMeasurment = new System.Windows.Forms.CheckBox();
            this.checkBoxShannonEntropy = new System.Windows.Forms.CheckBox();
            this.tabPageApplicationSettings = new System.Windows.Forms.TabPage();
            this.pictureBoxChooseStoreDirectory = new System.Windows.Forms.PictureBox();
            this.buttonSaveApplicationSettings = new System.Windows.Forms.Button();
            this.labelStoreDirectory = new System.Windows.Forms.Label();
            this.textBoxStoreDirectory = new System.Windows.Forms.TextBox();
            this.tabPageCapturing = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.richTextBoxShannonEntropy = new System.Windows.Forms.RichTextBox();
            this.buttonClearShannonEntropy = new System.Windows.Forms.Button();
            this.buttonClearContrastMeasurment = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.richTextBoxContrastMeasurment = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBoxLog = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.richTextBoxSession = new System.Windows.Forms.RichTextBox();
            this.buttonClearLog = new System.Windows.Forms.Button();
            this.radioButtonDebug = new System.Windows.Forms.RadioButton();
            this.radioButtonTrace = new System.Windows.Forms.RadioButton();
            this.radioButtonInfo = new System.Windows.Forms.RadioButton();
            this.panelCapturingProgress = new System.Windows.Forms.Panel();
            this.labelElapsedTime = new System.Windows.Forms.Label();
            this.labelRemainingTime = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.progressBarCapturingProgress = new System.Windows.Forms.ProgressBar();
            this.buttonCancelSequence = new System.Windows.Forms.Button();
            this.tabPageTests = new System.Windows.Forms.TabPage();
            this.buttonTestSerialization = new System.Windows.Forms.Button();
            this.buttonTestChart = new System.Windows.Forms.Button();
            this.errorProviderEx = new System.Windows.Forms.ErrorProvider(this.components);
            this.timerEx = new System.Windows.Forms.Timer(this.components);
            this.groupBoxCameraInfo.SuspendLayout();
            this.groupBoxExposureSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxAperture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExposureTime)).BeginInit();
            this.groupBoxCaptureSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxContinuousNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxContinuousTime)).BeginInit();
            this.groupBoxFocusSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxFocusPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxOpticalZoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExposureBias)).BeginInit();
            this.menuStripEx.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageCamera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            this.tabPageCaptureSettings.SuspendLayout();
            this.groupBoxSequenceCapture.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxFocusPositionStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxFocusSequenceCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxSequenceCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxTimeSpan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExposureTimeStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExposureSequenceCount)).BeginInit();
            this.tabPageApplicationSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxChooseStoreDirectory)).BeginInit();
            this.tabPageCapturing.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxLog.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelCapturingProgress.SuspendLayout();
            this.tabPageTests.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderEx)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxCameraInfo
            // 
            this.groupBoxCameraInfo.AutoSize = true;
            this.groupBoxCameraInfo.Controls.Add(this.buttonCopyAllImages);
            this.groupBoxCameraInfo.Controls.Add(this.textBoxContrastMeasurment);
            this.groupBoxCameraInfo.Controls.Add(this.textBoxShannonEntropy);
            this.groupBoxCameraInfo.Controls.Add(this.labelShannonEntropy);
            this.groupBoxCameraInfo.Controls.Add(this.labelContrastMeasurment);
            this.groupBoxCameraInfo.Controls.Add(this.buttonCleanFlash);
            this.groupBoxCameraInfo.Controls.Add(this.groupBoxExposureSettings);
            this.groupBoxCameraInfo.Controls.Add(this.groupBoxCaptureSettings);
            this.groupBoxCameraInfo.Controls.Add(this.groupBoxFocusSettings);
            this.groupBoxCameraInfo.Controls.Add(this.buttonTestCapture);
            this.groupBoxCameraInfo.Controls.Add(this.buttonSaveToFile);
            this.groupBoxCameraInfo.Controls.Add(this.textBoxOpticalZoom);
            this.groupBoxCameraInfo.Controls.Add(this.labelOpticalZoom);
            this.groupBoxCameraInfo.Controls.Add(this.buttonSendToCamera);
            this.groupBoxCameraInfo.Controls.Add(this.comboBoxWhiteBalance);
            this.groupBoxCameraInfo.Controls.Add(this.textBoxExposureBias);
            this.groupBoxCameraInfo.Controls.Add(this.comboBoxISOSpeed);
            this.groupBoxCameraInfo.Controls.Add(this.comboBoxResolution);
            this.groupBoxCameraInfo.Controls.Add(this.labelWhiteBalance);
            this.groupBoxCameraInfo.Controls.Add(this.labelISOSpeed);
            this.groupBoxCameraInfo.Controls.Add(this.labelResolution);
            this.groupBoxCameraInfo.Controls.Add(this.labelExposureBias);
            this.groupBoxCameraInfo.Controls.Add(this.labelFlashMode);
            this.groupBoxCameraInfo.Controls.Add(this.comboBoxFlashMode);
            this.groupBoxCameraInfo.Location = new System.Drawing.Point(256, 17);
            this.groupBoxCameraInfo.Name = "groupBoxCameraInfo";
            this.groupBoxCameraInfo.Size = new System.Drawing.Size(645, 352);
            this.groupBoxCameraInfo.TabIndex = 6;
            this.groupBoxCameraInfo.TabStop = false;
            this.groupBoxCameraInfo.Text = "CameraInfo";
            this.groupBoxCameraInfo.Enter += new System.EventHandler(this.groupBoxCameraInfo_Enter);
            // 
            // buttonCopyAllImages
            // 
            this.buttonCopyAllImages.Location = new System.Drawing.Point(383, 300);
            this.buttonCopyAllImages.Name = "buttonCopyAllImages";
            this.buttonCopyAllImages.Size = new System.Drawing.Size(106, 23);
            this.buttonCopyAllImages.TabIndex = 47;
            this.buttonCopyAllImages.Text = "Copy all images";
            this.buttonCopyAllImages.UseVisualStyleBackColor = true;
            this.buttonCopyAllImages.Click += new System.EventHandler(this.buttonCopyAllImages_Click);
            // 
            // textBoxContrastMeasurment
            // 
            this.textBoxContrastMeasurment.AllowDrop = true;
            this.textBoxContrastMeasurment.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxContrastMeasurment.Location = new System.Drawing.Point(162, 257);
            this.textBoxContrastMeasurment.Name = "textBoxContrastMeasurment";
            this.textBoxContrastMeasurment.ReadOnly = true;
            this.textBoxContrastMeasurment.Size = new System.Drawing.Size(64, 20);
            this.textBoxContrastMeasurment.TabIndex = 46;
            this.textBoxContrastMeasurment.TabStop = false;
            this.textBoxContrastMeasurment.WordWrap = false;
            // 
            // textBoxShannonEntropy
            // 
            this.textBoxShannonEntropy.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxShannonEntropy.Location = new System.Drawing.Point(341, 257);
            this.textBoxShannonEntropy.Name = "textBoxShannonEntropy";
            this.textBoxShannonEntropy.ReadOnly = true;
            this.textBoxShannonEntropy.Size = new System.Drawing.Size(100, 20);
            this.textBoxShannonEntropy.TabIndex = 45;
            this.textBoxShannonEntropy.TabStop = false;
            // 
            // labelShannonEntropy
            // 
            this.labelShannonEntropy.AutoSize = true;
            this.labelShannonEntropy.Location = new System.Drawing.Point(236, 257);
            this.labelShannonEntropy.Name = "labelShannonEntropy";
            this.labelShannonEntropy.Size = new System.Drawing.Size(89, 13);
            this.labelShannonEntropy.TabIndex = 44;
            this.labelShannonEntropy.Text = "ShannonEntropy:";
            // 
            // labelContrastMeasurment
            // 
            this.labelContrastMeasurment.AutoSize = true;
            this.labelContrastMeasurment.Location = new System.Drawing.Point(10, 261);
            this.labelContrastMeasurment.Name = "labelContrastMeasurment";
            this.labelContrastMeasurment.Size = new System.Drawing.Size(107, 13);
            this.labelContrastMeasurment.TabIndex = 43;
            this.labelContrastMeasurment.Text = "ContrastMeasurment:";
            // 
            // buttonCleanFlash
            // 
            this.buttonCleanFlash.Location = new System.Drawing.Point(265, 300);
            this.buttonCleanFlash.Name = "buttonCleanFlash";
            this.buttonCleanFlash.Size = new System.Drawing.Size(112, 23);
            this.buttonCleanFlash.TabIndex = 41;
            this.buttonCleanFlash.Text = "Clean flash";
            this.buttonCleanFlash.UseVisualStyleBackColor = true;
            this.buttonCleanFlash.Click += new System.EventHandler(this.buttonCleanFlash_Click);
            // 
            // groupBoxExposureSettings
            // 
            this.groupBoxExposureSettings.Controls.Add(this.textBoxAperture);
            this.groupBoxExposureSettings.Controls.Add(this.textBoxExposureTime);
            this.groupBoxExposureSettings.Controls.Add(this.labelExposureMode);
            this.groupBoxExposureSettings.Controls.Add(this.labelAperture);
            this.groupBoxExposureSettings.Controls.Add(this.labelExposureTime);
            this.groupBoxExposureSettings.Controls.Add(this.comboBoxExposureMode);
            this.groupBoxExposureSettings.Location = new System.Drawing.Point(301, 12);
            this.groupBoxExposureSettings.Name = "groupBoxExposureSettings";
            this.groupBoxExposureSettings.Size = new System.Drawing.Size(310, 114);
            this.groupBoxExposureSettings.TabIndex = 40;
            this.groupBoxExposureSettings.TabStop = false;
            this.groupBoxExposureSettings.Text = "Exposure settings";
            // 
            // textBoxAperture
            // 
            this.textBoxAperture.Location = new System.Drawing.Point(158, 78);
            this.textBoxAperture.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.textBoxAperture.Name = "textBoxAperture";
            this.textBoxAperture.Size = new System.Drawing.Size(112, 20);
            this.textBoxAperture.TabIndex = 29;
            this.textBoxAperture.ValueChanged += new System.EventHandler(this.textBoxAperture_ValueChanged);
            // 
            // textBoxExposureTime
            // 
            this.textBoxExposureTime.Location = new System.Drawing.Point(158, 49);
            this.textBoxExposureTime.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.textBoxExposureTime.Name = "textBoxExposureTime";
            this.textBoxExposureTime.Size = new System.Drawing.Size(112, 20);
            this.textBoxExposureTime.TabIndex = 28;
            this.textBoxExposureTime.ValueChanged += new System.EventHandler(this.textBoxExposureTime_ValueChanged);
            // 
            // labelExposureMode
            // 
            this.labelExposureMode.AutoSize = true;
            this.labelExposureMode.Location = new System.Drawing.Point(6, 16);
            this.labelExposureMode.Name = "labelExposureMode";
            this.labelExposureMode.Size = new System.Drawing.Size(83, 13);
            this.labelExposureMode.TabIndex = 27;
            this.labelExposureMode.Text = "Exposure mode:";
            // 
            // labelAperture
            // 
            this.labelAperture.AutoSize = true;
            this.labelAperture.Location = new System.Drawing.Point(6, 85);
            this.labelAperture.Name = "labelAperture";
            this.labelAperture.Size = new System.Drawing.Size(50, 13);
            this.labelAperture.TabIndex = 26;
            this.labelAperture.Text = "Aperture:";
            // 
            // labelExposureTime
            // 
            this.labelExposureTime.AutoSize = true;
            this.labelExposureTime.Location = new System.Drawing.Point(6, 52);
            this.labelExposureTime.Name = "labelExposureTime";
            this.labelExposureTime.Size = new System.Drawing.Size(118, 13);
            this.labelExposureTime.TabIndex = 25;
            this.labelExposureTime.Text = "Exposure time(10E-6 s):";
            // 
            // comboBoxExposureMode
            // 
            this.comboBoxExposureMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxExposureMode.FormattingEnabled = true;
            this.comboBoxExposureMode.Location = new System.Drawing.Point(95, 13);
            this.comboBoxExposureMode.Name = "comboBoxExposureMode";
            this.comboBoxExposureMode.Size = new System.Drawing.Size(175, 21);
            this.comboBoxExposureMode.TabIndex = 24;
            this.comboBoxExposureMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxExposureMode_SelectedIndexChanged);
            // 
            // groupBoxCaptureSettings
            // 
            this.groupBoxCaptureSettings.Controls.Add(this.textBoxContinuousNumber);
            this.groupBoxCaptureSettings.Controls.Add(this.textBoxContinuousTime);
            this.groupBoxCaptureSettings.Controls.Add(this.labelContinuousNumber);
            this.groupBoxCaptureSettings.Controls.Add(this.labelContinuousTime);
            this.groupBoxCaptureSettings.Controls.Add(this.labelCaptureMode);
            this.groupBoxCaptureSettings.Controls.Add(this.comboBoxCaptureMode);
            this.groupBoxCaptureSettings.Location = new System.Drawing.Point(6, 84);
            this.groupBoxCaptureSettings.Name = "groupBoxCaptureSettings";
            this.groupBoxCaptureSettings.Size = new System.Drawing.Size(263, 92);
            this.groupBoxCaptureSettings.TabIndex = 39;
            this.groupBoxCaptureSettings.TabStop = false;
            this.groupBoxCaptureSettings.Text = "Capture settings";
            // 
            // textBoxContinuousNumber
            // 
            this.textBoxContinuousNumber.Location = new System.Drawing.Point(104, 66);
            this.textBoxContinuousNumber.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.textBoxContinuousNumber.Name = "textBoxContinuousNumber";
            this.textBoxContinuousNumber.Size = new System.Drawing.Size(105, 20);
            this.textBoxContinuousNumber.TabIndex = 37;
            this.textBoxContinuousNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.textBoxContinuousNumber.ValueChanged += new System.EventHandler(this.textBoxContinuousNumber_ValueChanged);
            // 
            // textBoxContinuousTime
            // 
            this.textBoxContinuousTime.Location = new System.Drawing.Point(104, 39);
            this.textBoxContinuousTime.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.textBoxContinuousTime.Name = "textBoxContinuousTime";
            this.textBoxContinuousTime.Size = new System.Drawing.Size(105, 20);
            this.textBoxContinuousTime.TabIndex = 36;
            this.textBoxContinuousTime.ValueChanged += new System.EventHandler(this.textBoxContinuousTime_ValueChanged);
            // 
            // labelContinuousNumber
            // 
            this.labelContinuousNumber.AutoSize = true;
            this.labelContinuousNumber.Location = new System.Drawing.Point(6, 69);
            this.labelContinuousNumber.Name = "labelContinuousNumber";
            this.labelContinuousNumber.Size = new System.Drawing.Size(101, 13);
            this.labelContinuousNumber.TabIndex = 35;
            this.labelContinuousNumber.Text = "Continuous number:";
            // 
            // labelContinuousTime
            // 
            this.labelContinuousTime.AutoSize = true;
            this.labelContinuousTime.Location = new System.Drawing.Point(6, 44);
            this.labelContinuousTime.Name = "labelContinuousTime";
            this.labelContinuousTime.Size = new System.Drawing.Size(85, 13);
            this.labelContinuousTime.TabIndex = 34;
            this.labelContinuousTime.Text = "Continuous time:";
            // 
            // labelCaptureMode
            // 
            this.labelCaptureMode.AutoSize = true;
            this.labelCaptureMode.Location = new System.Drawing.Point(6, 16);
            this.labelCaptureMode.Name = "labelCaptureMode";
            this.labelCaptureMode.Size = new System.Drawing.Size(76, 13);
            this.labelCaptureMode.TabIndex = 33;
            this.labelCaptureMode.Text = "Capture mode:";
            // 
            // comboBoxCaptureMode
            // 
            this.comboBoxCaptureMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCaptureMode.FormattingEnabled = true;
            this.comboBoxCaptureMode.Location = new System.Drawing.Point(104, 14);
            this.comboBoxCaptureMode.Name = "comboBoxCaptureMode";
            this.comboBoxCaptureMode.Size = new System.Drawing.Size(134, 21);
            this.comboBoxCaptureMode.TabIndex = 32;
            this.comboBoxCaptureMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxCaptureMode_SelectedIndexChanged);
            // 
            // groupBoxFocusSettings
            // 
            this.groupBoxFocusSettings.Controls.Add(this.comboBoxFocusMode);
            this.groupBoxFocusSettings.Controls.Add(this.labelFocusMode);
            this.groupBoxFocusSettings.Controls.Add(this.textBoxFocusPosition);
            this.groupBoxFocusSettings.Controls.Add(this.labelFocusPosition);
            this.groupBoxFocusSettings.Location = new System.Drawing.Point(6, 12);
            this.groupBoxFocusSettings.Name = "groupBoxFocusSettings";
            this.groupBoxFocusSettings.Size = new System.Drawing.Size(263, 71);
            this.groupBoxFocusSettings.TabIndex = 38;
            this.groupBoxFocusSettings.TabStop = false;
            this.groupBoxFocusSettings.Text = "Focus settings";
            // 
            // comboBoxFocusMode
            // 
            this.comboBoxFocusMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFocusMode.FormattingEnabled = true;
            this.comboBoxFocusMode.Location = new System.Drawing.Point(77, 13);
            this.comboBoxFocusMode.Name = "comboBoxFocusMode";
            this.comboBoxFocusMode.Size = new System.Drawing.Size(143, 21);
            this.comboBoxFocusMode.TabIndex = 37;
            this.comboBoxFocusMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxFocusMode_SelectedIndexChanged);
            // 
            // labelFocusMode
            // 
            this.labelFocusMode.AutoSize = true;
            this.labelFocusMode.Location = new System.Drawing.Point(3, 14);
            this.labelFocusMode.Name = "labelFocusMode";
            this.labelFocusMode.Size = new System.Drawing.Size(68, 13);
            this.labelFocusMode.TabIndex = 36;
            this.labelFocusMode.Text = "Focus mode:";
            // 
            // textBoxFocusPosition
            // 
            this.textBoxFocusPosition.Location = new System.Drawing.Point(86, 40);
            this.textBoxFocusPosition.Maximum = new decimal(new int[] {
            240,
            0,
            0,
            0});
            this.textBoxFocusPosition.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.textBoxFocusPosition.Name = "textBoxFocusPosition";
            this.textBoxFocusPosition.Size = new System.Drawing.Size(114, 20);
            this.textBoxFocusPosition.TabIndex = 35;
            this.textBoxFocusPosition.ValueChanged += new System.EventHandler(this.textBoxFocusPosition_ValueChanged);
            // 
            // labelFocusPosition
            // 
            this.labelFocusPosition.AutoSize = true;
            this.labelFocusPosition.Location = new System.Drawing.Point(4, 42);
            this.labelFocusPosition.Name = "labelFocusPosition";
            this.labelFocusPosition.Size = new System.Drawing.Size(78, 13);
            this.labelFocusPosition.TabIndex = 34;
            this.labelFocusPosition.Text = "Focus position:";
            // 
            // buttonTestCapture
            // 
            this.buttonTestCapture.Location = new System.Drawing.Point(128, 300);
            this.buttonTestCapture.Name = "buttonTestCapture";
            this.buttonTestCapture.Size = new System.Drawing.Size(100, 23);
            this.buttonTestCapture.TabIndex = 35;
            this.buttonTestCapture.Text = "Test capture";
            this.buttonTestCapture.UseVisualStyleBackColor = true;
            this.buttonTestCapture.Click += new System.EventHandler(this.buttonTestCapture_Click);
            // 
            // buttonSaveToFile
            // 
            this.buttonSaveToFile.Location = new System.Drawing.Point(493, 251);
            this.buttonSaveToFile.Name = "buttonSaveToFile";
            this.buttonSaveToFile.Size = new System.Drawing.Size(78, 23);
            this.buttonSaveToFile.TabIndex = 34;
            this.buttonSaveToFile.Text = "Save to file";
            this.buttonSaveToFile.UseVisualStyleBackColor = true;
            this.buttonSaveToFile.Visible = false;
            // 
            // textBoxOpticalZoom
            // 
            this.textBoxOpticalZoom.Location = new System.Drawing.Point(92, 176);
            this.textBoxOpticalZoom.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.textBoxOpticalZoom.Name = "textBoxOpticalZoom";
            this.textBoxOpticalZoom.Size = new System.Drawing.Size(172, 20);
            this.textBoxOpticalZoom.TabIndex = 27;
            this.textBoxOpticalZoom.ValueChanged += new System.EventHandler(this.textBoxOpticalZoom_ValueChanged);
            // 
            // labelOpticalZoom
            // 
            this.labelOpticalZoom.AutoSize = true;
            this.labelOpticalZoom.Location = new System.Drawing.Point(6, 179);
            this.labelOpticalZoom.Name = "labelOpticalZoom";
            this.labelOpticalZoom.Size = new System.Drawing.Size(71, 13);
            this.labelOpticalZoom.TabIndex = 26;
            this.labelOpticalZoom.Text = "Optical zoom:";
            // 
            // buttonSendToCamera
            // 
            this.buttonSendToCamera.Location = new System.Drawing.Point(9, 300);
            this.buttonSendToCamera.Name = "buttonSendToCamera";
            this.buttonSendToCamera.Size = new System.Drawing.Size(98, 23);
            this.buttonSendToCamera.TabIndex = 25;
            this.buttonSendToCamera.Text = "Send to camera";
            this.buttonSendToCamera.UseVisualStyleBackColor = true;
            this.buttonSendToCamera.Click += new System.EventHandler(this.buttonSendToCamera_Click);
            // 
            // comboBoxWhiteBalance
            // 
            this.comboBoxWhiteBalance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWhiteBalance.FormattingEnabled = true;
            this.comboBoxWhiteBalance.Location = new System.Drawing.Point(383, 213);
            this.comboBoxWhiteBalance.Name = "comboBoxWhiteBalance";
            this.comboBoxWhiteBalance.Size = new System.Drawing.Size(216, 21);
            this.comboBoxWhiteBalance.TabIndex = 21;
            this.comboBoxWhiteBalance.SelectedIndexChanged += new System.EventHandler(this.comboBoxWhiteBalance_SelectedIndexChanged);
            // 
            // textBoxExposureBias
            // 
            this.textBoxExposureBias.Location = new System.Drawing.Point(92, 207);
            this.textBoxExposureBias.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.textBoxExposureBias.Name = "textBoxExposureBias";
            this.textBoxExposureBias.Size = new System.Drawing.Size(172, 20);
            this.textBoxExposureBias.TabIndex = 20;
            this.textBoxExposureBias.ValueChanged += new System.EventHandler(this.textBoxExposureBias_ValueChanged);
            // 
            // comboBoxISOSpeed
            // 
            this.comboBoxISOSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxISOSpeed.FormattingEnabled = true;
            this.comboBoxISOSpeed.Location = new System.Drawing.Point(383, 186);
            this.comboBoxISOSpeed.Name = "comboBoxISOSpeed";
            this.comboBoxISOSpeed.Size = new System.Drawing.Size(216, 21);
            this.comboBoxISOSpeed.TabIndex = 18;
            this.comboBoxISOSpeed.SelectedIndexChanged += new System.EventHandler(this.comboBoxISOSpeed_SelectedIndexChanged);
            // 
            // comboBoxResolution
            // 
            this.comboBoxResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxResolution.FormattingEnabled = true;
            this.comboBoxResolution.Location = new System.Drawing.Point(384, 155);
            this.comboBoxResolution.Name = "comboBoxResolution";
            this.comboBoxResolution.Size = new System.Drawing.Size(215, 21);
            this.comboBoxResolution.TabIndex = 17;
            this.comboBoxResolution.SelectedIndexChanged += new System.EventHandler(this.comboBoxResolution_SelectedIndexChanged);
            // 
            // labelWhiteBalance
            // 
            this.labelWhiteBalance.AutoSize = true;
            this.labelWhiteBalance.Location = new System.Drawing.Point(295, 216);
            this.labelWhiteBalance.Name = "labelWhiteBalance";
            this.labelWhiteBalance.Size = new System.Drawing.Size(82, 13);
            this.labelWhiteBalance.TabIndex = 13;
            this.labelWhiteBalance.Text = " White balance:";
            // 
            // labelISOSpeed
            // 
            this.labelISOSpeed.AutoSize = true;
            this.labelISOSpeed.Location = new System.Drawing.Point(303, 186);
            this.labelISOSpeed.Name = "labelISOSpeed";
            this.labelISOSpeed.Size = new System.Drawing.Size(59, 13);
            this.labelISOSpeed.TabIndex = 10;
            this.labelISOSpeed.Text = "ISOSpeed:";
            // 
            // labelResolution
            // 
            this.labelResolution.AutoSize = true;
            this.labelResolution.Location = new System.Drawing.Point(298, 158);
            this.labelResolution.Name = "labelResolution";
            this.labelResolution.Size = new System.Drawing.Size(60, 13);
            this.labelResolution.TabIndex = 9;
            this.labelResolution.Text = "Resolution:";
            // 
            // labelExposureBias
            // 
            this.labelExposureBias.AutoSize = true;
            this.labelExposureBias.Location = new System.Drawing.Point(10, 211);
            this.labelExposureBias.Name = "labelExposureBias";
            this.labelExposureBias.Size = new System.Drawing.Size(76, 13);
            this.labelExposureBias.TabIndex = 7;
            this.labelExposureBias.Text = "Exposure bias:";
            // 
            // labelFlashMode
            // 
            this.labelFlashMode.AutoSize = true;
            this.labelFlashMode.Location = new System.Drawing.Point(298, 136);
            this.labelFlashMode.Name = "labelFlashMode";
            this.labelFlashMode.Size = new System.Drawing.Size(64, 13);
            this.labelFlashMode.TabIndex = 5;
            this.labelFlashMode.Text = "Flash mode:";
            // 
            // comboBoxFlashMode
            // 
            this.comboBoxFlashMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFlashMode.FormattingEnabled = true;
            this.comboBoxFlashMode.Location = new System.Drawing.Point(384, 128);
            this.comboBoxFlashMode.Name = "comboBoxFlashMode";
            this.comboBoxFlashMode.Size = new System.Drawing.Size(215, 21);
            this.comboBoxFlashMode.TabIndex = 3;
            this.comboBoxFlashMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxFlashMode_SelectedIndexChanged);
            // 
            // menuStripEx
            // 
            this.menuStripEx.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCalculator,
            this.toolStripMenuItemOpenCurrentStoreDirectory});
            this.menuStripEx.Location = new System.Drawing.Point(0, 0);
            this.menuStripEx.Name = "menuStripEx";
            this.menuStripEx.Size = new System.Drawing.Size(898, 24);
            this.menuStripEx.TabIndex = 2;
            this.menuStripEx.Text = "menuStripEx";
            // 
            // toolStripMenuItemCalculator
            // 
            this.toolStripMenuItemCalculator.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemCalculator.Image")));
            this.toolStripMenuItemCalculator.Name = "toolStripMenuItemCalculator";
            this.toolStripMenuItemCalculator.Size = new System.Drawing.Size(82, 20);
            this.toolStripMenuItemCalculator.Text = "Calculator";
            this.toolStripMenuItemCalculator.Click += new System.EventHandler(this.toolStripMenuItemCalculator_Click);
            // 
            // toolStripMenuItemOpenCurrentStoreDirectory
            // 
            this.toolStripMenuItemOpenCurrentStoreDirectory.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemOpenCurrentStoreDirectory.Image")));
            this.toolStripMenuItemOpenCurrentStoreDirectory.Name = "toolStripMenuItemOpenCurrentStoreDirectory";
            this.toolStripMenuItemOpenCurrentStoreDirectory.Size = new System.Drawing.Size(166, 20);
            this.toolStripMenuItemOpenCurrentStoreDirectory.Text = "Open current store directory";
            this.toolStripMenuItemOpenCurrentStoreDirectory.Click += new System.EventHandler(this.toolStripMenuItemOpenCurrentFolder_Click);
            // 
            // statusStripEx
            // 
            this.statusStripEx.Location = new System.Drawing.Point(0, 547);
            this.statusStripEx.Name = "statusStripEx";
            this.statusStripEx.Size = new System.Drawing.Size(898, 22);
            this.statusStripEx.TabIndex = 3;
            this.statusStripEx.Text = "statusStrip1";
            // 
            // toolStripProgressBarInicialization
            // 
            this.toolStripProgressBarInicialization.Name = "toolStripProgressBarInicialization";
            this.toolStripProgressBarInicialization.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabelError
            // 
            this.toolStripStatusLabelError.AutoSize = false;
            this.toolStripStatusLabelError.ForeColor = System.Drawing.Color.Red;
            this.toolStripStatusLabelError.Name = "toolStripStatusLabelError";
            this.toolStripStatusLabelError.Size = new System.Drawing.Size(700, 17);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.tabControlMain);
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(896, 520);
            this.panel1.TabIndex = 4;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageCamera);
            this.tabControlMain.Controls.Add(this.tabPageCaptureSettings);
            this.tabControlMain.Controls.Add(this.tabPageApplicationSettings);
            this.tabControlMain.Controls.Add(this.tabPageCapturing);
            this.tabControlMain.Controls.Add(this.tabPageTests);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(896, 520);
            this.tabControlMain.TabIndex = 1;
            // 
            // tabPageCamera
            // 
            this.tabPageCamera.Controls.Add(this.buttonDisconnect);
            this.tabPageCamera.Controls.Add(this.buttonConnect);
            this.tabPageCamera.Controls.Add(this.buttonPreview);
            this.tabPageCamera.Controls.Add(this.pictureBoxPreview);
            this.tabPageCamera.Controls.Add(this.buttonRegisterCamera);
            this.tabPageCamera.Controls.Add(this.buttonInstallDriver);
            this.tabPageCamera.Controls.Add(this.buttonRefresh);
            this.tabPageCamera.Controls.Add(this.listBoxCamera);
            this.tabPageCamera.Location = new System.Drawing.Point(4, 22);
            this.tabPageCamera.Name = "tabPageCamera";
            this.tabPageCamera.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCamera.Size = new System.Drawing.Size(888, 494);
            this.tabPageCamera.TabIndex = 0;
            this.tabPageCamera.Text = "Camera";
            this.tabPageCamera.UseVisualStyleBackColor = true;
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Location = new System.Drawing.Point(338, 95);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(75, 23);
            this.buttonDisconnect.TabIndex = 9;
            this.buttonDisconnect.Text = "Disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(338, 54);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonConnect.TabIndex = 8;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonPreview
            // 
            this.buttonPreview.Location = new System.Drawing.Point(486, 209);
            this.buttonPreview.Name = "buttonPreview";
            this.buttonPreview.Size = new System.Drawing.Size(215, 59);
            this.buttonPreview.TabIndex = 7;
            this.buttonPreview.Text = "Preview";
            this.buttonPreview.UseVisualStyleBackColor = true;
            this.buttonPreview.Click += new System.EventHandler(this.buttonPreview_Click);
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPreview.Location = new System.Drawing.Point(486, 30);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(215, 173);
            this.pictureBoxPreview.TabIndex = 6;
            this.pictureBoxPreview.TabStop = false;
            // 
            // buttonRegisterCamera
            // 
            this.buttonRegisterCamera.Location = new System.Drawing.Point(11, 330);
            this.buttonRegisterCamera.Name = "buttonRegisterCamera";
            this.buttonRegisterCamera.Size = new System.Drawing.Size(178, 31);
            this.buttonRegisterCamera.TabIndex = 5;
            this.buttonRegisterCamera.Text = "Register camera";
            this.buttonRegisterCamera.UseVisualStyleBackColor = true;
            this.buttonRegisterCamera.Click += new System.EventHandler(this.buttonRegisterCamera_Click);
            // 
            // buttonInstallDriver
            // 
            this.buttonInstallDriver.Location = new System.Drawing.Point(8, 294);
            this.buttonInstallDriver.Name = "buttonInstallDriver";
            this.buttonInstallDriver.Size = new System.Drawing.Size(181, 30);
            this.buttonInstallDriver.TabIndex = 4;
            this.buttonInstallDriver.Text = "Install driver";
            this.buttonInstallDriver.UseVisualStyleBackColor = true;
            this.buttonInstallDriver.Click += new System.EventHandler(this.buttonInstallDriver_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(3, 221);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(254, 47);
            this.buttonRefresh.TabIndex = 1;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // listBoxCamera
            // 
            this.listBoxCamera.FormattingEnabled = true;
            this.listBoxCamera.Location = new System.Drawing.Point(3, 3);
            this.listBoxCamera.Name = "listBoxCamera";
            this.listBoxCamera.Size = new System.Drawing.Size(254, 212);
            this.listBoxCamera.TabIndex = 0;
            // 
            // tabPageCaptureSettings
            // 
            this.tabPageCaptureSettings.Controls.Add(this.checkBoxPlotShannonEntropy);
            this.tabPageCaptureSettings.Controls.Add(this.checkBoxEraseAllPictures);
            this.tabPageCaptureSettings.Controls.Add(this.groupBoxSequenceCapture);
            this.tabPageCaptureSettings.Controls.Add(this.checkBoxCopyImages);
            this.tabPageCaptureSettings.Controls.Add(this.groupBoxCameraInfo);
            this.tabPageCaptureSettings.Controls.Add(this.checkBoxlContrastMeasurment);
            this.tabPageCaptureSettings.Controls.Add(this.checkBoxShannonEntropy);
            this.tabPageCaptureSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageCaptureSettings.Name = "tabPageCaptureSettings";
            this.tabPageCaptureSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCaptureSettings.Size = new System.Drawing.Size(888, 494);
            this.tabPageCaptureSettings.TabIndex = 1;
            this.tabPageCaptureSettings.Text = "Capture settings";
            this.tabPageCaptureSettings.UseVisualStyleBackColor = true;
            this.tabPageCaptureSettings.Enter += new System.EventHandler(this.tabPageCapture_Enter);
            // 
            // checkBoxPlotShannonEntropy
            // 
            this.checkBoxPlotShannonEntropy.AutoSize = true;
            this.checkBoxPlotShannonEntropy.Location = new System.Drawing.Point(190, 394);
            this.checkBoxPlotShannonEntropy.Name = "checkBoxPlotShannonEntropy";
            this.checkBoxPlotShannonEntropy.Size = new System.Drawing.Size(128, 17);
            this.checkBoxPlotShannonEntropy.TabIndex = 36;
            this.checkBoxPlotShannonEntropy.Text = "Plot Shannon entropy";
            this.checkBoxPlotShannonEntropy.UseVisualStyleBackColor = true;
            // 
            // checkBoxEraseAllPictures
            // 
            this.checkBoxEraseAllPictures.AutoSize = true;
            this.checkBoxEraseAllPictures.Location = new System.Drawing.Point(190, 442);
            this.checkBoxEraseAllPictures.Name = "checkBoxEraseAllPictures";
            this.checkBoxEraseAllPictures.Size = new System.Drawing.Size(158, 17);
            this.checkBoxEraseAllPictures.TabIndex = 35;
            this.checkBoxEraseAllPictures.Text = "Clean flash before capturing";
            this.checkBoxEraseAllPictures.UseVisualStyleBackColor = true;
            // 
            // groupBoxSequenceCapture
            // 
            this.groupBoxSequenceCapture.Controls.Add(this.tableLayoutPanel2);
            this.groupBoxSequenceCapture.Location = new System.Drawing.Point(10, 17);
            this.groupBoxSequenceCapture.Name = "groupBoxSequenceCapture";
            this.groupBoxSequenceCapture.Size = new System.Drawing.Size(240, 371);
            this.groupBoxSequenceCapture.TabIndex = 34;
            this.groupBoxSequenceCapture.TabStop = false;
            this.groupBoxSequenceCapture.Text = "Capture";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonCaptureSequence, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.textBoxFocusPositionStep, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.labelFocusPositionStep, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.textBoxFocusSequenceCount, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.labelFocusSequenceCount, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.radioButtonFocusPriority, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.textBoxSequenceCount, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.radioButtonExposurePriority, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.textBoxTimeSpan, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.labeExposureSequenceCount, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.textBoxExposureTimeStep, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.labelExposureTimeStep, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.textBoxExposureSequenceCount, 1, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 9;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(234, 352);
            this.tableLayoutPanel2.TabIndex = 42;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Session length:";
            // 
            // buttonCaptureSequence
            // 
            this.buttonCaptureSequence.Location = new System.Drawing.Point(3, 243);
            this.buttonCaptureSequence.Name = "buttonCaptureSequence";
            this.buttonCaptureSequence.Size = new System.Drawing.Size(110, 23);
            this.buttonCaptureSequence.TabIndex = 27;
            this.buttonCaptureSequence.Text = "Capture sequence";
            this.buttonCaptureSequence.UseVisualStyleBackColor = true;
            this.buttonCaptureSequence.Click += new System.EventHandler(this.buttonCaptureSequence_Click);
            // 
            // textBoxFocusPositionStep
            // 
            this.textBoxFocusPositionStep.Location = new System.Drawing.Point(120, 213);
            this.textBoxFocusPositionStep.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.textBoxFocusPositionStep.Name = "textBoxFocusPositionStep";
            this.textBoxFocusPositionStep.Size = new System.Drawing.Size(94, 20);
            this.textBoxFocusPositionStep.TabIndex = 36;
            this.textBoxFocusPositionStep.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxFocusPositionStep_Validating);
            // 
            // labelFocusPositionStep
            // 
            this.labelFocusPositionStep.AutoSize = true;
            this.labelFocusPositionStep.Location = new System.Drawing.Point(3, 210);
            this.labelFocusPositionStep.Name = "labelFocusPositionStep";
            this.labelFocusPositionStep.Size = new System.Drawing.Size(101, 13);
            this.labelFocusPositionStep.TabIndex = 37;
            this.labelFocusPositionStep.Text = "Focus position step:";
            // 
            // textBoxFocusSequenceCount
            // 
            this.textBoxFocusSequenceCount.Location = new System.Drawing.Point(120, 183);
            this.textBoxFocusSequenceCount.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.textBoxFocusSequenceCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.textBoxFocusSequenceCount.Name = "textBoxFocusSequenceCount";
            this.textBoxFocusSequenceCount.Size = new System.Drawing.Size(94, 20);
            this.textBoxFocusSequenceCount.TabIndex = 39;
            this.textBoxFocusSequenceCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelFocusSequenceCount
            // 
            this.labelFocusSequenceCount.AutoSize = true;
            this.labelFocusSequenceCount.Location = new System.Drawing.Point(3, 180);
            this.labelFocusSequenceCount.Name = "labelFocusSequenceCount";
            this.labelFocusSequenceCount.Size = new System.Drawing.Size(89, 26);
            this.labelFocusSequenceCount.TabIndex = 38;
            this.labelFocusSequenceCount.Text = "Focus sequence length:";
            // 
            // radioButtonFocusPriority
            // 
            this.radioButtonFocusPriority.AutoSize = true;
            this.radioButtonFocusPriority.Location = new System.Drawing.Point(3, 153);
            this.radioButtonFocusPriority.Name = "radioButtonFocusPriority";
            this.radioButtonFocusPriority.Size = new System.Drawing.Size(87, 17);
            this.radioButtonFocusPriority.TabIndex = 41;
            this.radioButtonFocusPriority.Text = "Focus priority";
            this.radioButtonFocusPriority.UseVisualStyleBackColor = true;
            // 
            // textBoxSequenceCount
            // 
            this.textBoxSequenceCount.Location = new System.Drawing.Point(120, 3);
            this.textBoxSequenceCount.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.textBoxSequenceCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.textBoxSequenceCount.Name = "textBoxSequenceCount";
            this.textBoxSequenceCount.Size = new System.Drawing.Size(107, 20);
            this.textBoxSequenceCount.TabIndex = 33;
            this.textBoxSequenceCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // radioButtonExposurePriority
            // 
            this.radioButtonExposurePriority.AutoSize = true;
            this.radioButtonExposurePriority.Checked = true;
            this.radioButtonExposurePriority.Location = new System.Drawing.Point(3, 63);
            this.radioButtonExposurePriority.Name = "radioButtonExposurePriority";
            this.radioButtonExposurePriority.Size = new System.Drawing.Size(102, 17);
            this.radioButtonExposurePriority.TabIndex = 40;
            this.radioButtonExposurePriority.TabStop = true;
            this.radioButtonExposurePriority.Text = "Exposure priority";
            this.radioButtonExposurePriority.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 26);
            this.label1.TabIndex = 29;
            this.label1.Text = "Time span(in seconds):";
            // 
            // textBoxTimeSpan
            // 
            this.textBoxTimeSpan.Location = new System.Drawing.Point(120, 33);
            this.textBoxTimeSpan.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.textBoxTimeSpan.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.textBoxTimeSpan.Name = "textBoxTimeSpan";
            this.textBoxTimeSpan.Size = new System.Drawing.Size(107, 20);
            this.textBoxTimeSpan.TabIndex = 32;
            this.textBoxTimeSpan.Value = new decimal(new int[] {
            45,
            0,
            0,
            0});
            // 
            // labeExposureSequenceCount
            // 
            this.labeExposureSequenceCount.AutoSize = true;
            this.labeExposureSequenceCount.Location = new System.Drawing.Point(3, 90);
            this.labeExposureSequenceCount.Name = "labeExposureSequenceCount";
            this.labeExposureSequenceCount.Size = new System.Drawing.Size(104, 26);
            this.labeExposureSequenceCount.TabIndex = 28;
            this.labeExposureSequenceCount.Text = "Exposure sequence length:";
            // 
            // textBoxExposureTimeStep
            // 
            this.textBoxExposureTimeStep.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.textBoxExposureTimeStep.Location = new System.Drawing.Point(120, 123);
            this.textBoxExposureTimeStep.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.textBoxExposureTimeStep.Name = "textBoxExposureTimeStep";
            this.textBoxExposureTimeStep.Size = new System.Drawing.Size(91, 20);
            this.textBoxExposureTimeStep.TabIndex = 34;
            this.textBoxExposureTimeStep.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.textBoxExposureTimeStep.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxExposureTimeStep_Validating);
            // 
            // labelExposureTimeStep
            // 
            this.labelExposureTimeStep.AutoSize = true;
            this.labelExposureTimeStep.Location = new System.Drawing.Point(3, 120);
            this.labelExposureTimeStep.Name = "labelExposureTimeStep";
            this.labelExposureTimeStep.Size = new System.Drawing.Size(88, 26);
            this.labelExposureTimeStep.TabIndex = 35;
            this.labelExposureTimeStep.Text = "Exposure timestep(10E-6s):";
            // 
            // textBoxExposureSequenceCount
            // 
            this.textBoxExposureSequenceCount.Location = new System.Drawing.Point(120, 93);
            this.textBoxExposureSequenceCount.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.textBoxExposureSequenceCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.textBoxExposureSequenceCount.Name = "textBoxExposureSequenceCount";
            this.textBoxExposureSequenceCount.Size = new System.Drawing.Size(91, 20);
            this.textBoxExposureSequenceCount.TabIndex = 31;
            this.textBoxExposureSequenceCount.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // checkBoxCopyImages
            // 
            this.checkBoxCopyImages.AutoSize = true;
            this.checkBoxCopyImages.Location = new System.Drawing.Point(10, 442);
            this.checkBoxCopyImages.Name = "checkBoxCopyImages";
            this.checkBoxCopyImages.Size = new System.Drawing.Size(86, 17);
            this.checkBoxCopyImages.TabIndex = 32;
            this.checkBoxCopyImages.Text = "Copy images";
            this.checkBoxCopyImages.UseVisualStyleBackColor = true;
            // 
            // checkBoxlContrastMeasurment
            // 
            this.checkBoxlContrastMeasurment.AutoSize = true;
            this.checkBoxlContrastMeasurment.Location = new System.Drawing.Point(10, 418);
            this.checkBoxlContrastMeasurment.Name = "checkBoxlContrastMeasurment";
            this.checkBoxlContrastMeasurment.Size = new System.Drawing.Size(172, 17);
            this.checkBoxlContrastMeasurment.TabIndex = 31;
            this.checkBoxlContrastMeasurment.Text = "Calculate Contrast measurment";
            this.checkBoxlContrastMeasurment.UseVisualStyleBackColor = true;
            // 
            // checkBoxShannonEntropy
            // 
            this.checkBoxShannonEntropy.AutoSize = true;
            this.checkBoxShannonEntropy.Location = new System.Drawing.Point(10, 394);
            this.checkBoxShannonEntropy.Name = "checkBoxShannonEntropy";
            this.checkBoxShannonEntropy.Size = new System.Drawing.Size(154, 17);
            this.checkBoxShannonEntropy.TabIndex = 30;
            this.checkBoxShannonEntropy.Text = "Calculate Shannon entropy";
            this.checkBoxShannonEntropy.UseVisualStyleBackColor = true;
            // 
            // tabPageApplicationSettings
            // 
            this.tabPageApplicationSettings.Controls.Add(this.pictureBoxChooseStoreDirectory);
            this.tabPageApplicationSettings.Controls.Add(this.buttonSaveApplicationSettings);
            this.tabPageApplicationSettings.Controls.Add(this.labelStoreDirectory);
            this.tabPageApplicationSettings.Controls.Add(this.textBoxStoreDirectory);
            this.tabPageApplicationSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageApplicationSettings.Name = "tabPageApplicationSettings";
            this.tabPageApplicationSettings.Size = new System.Drawing.Size(888, 494);
            this.tabPageApplicationSettings.TabIndex = 2;
            this.tabPageApplicationSettings.Text = "Application settings";
            this.tabPageApplicationSettings.UseVisualStyleBackColor = true;
            // 
            // pictureBoxChooseStoreDirectory
            // 
            this.pictureBoxChooseStoreDirectory.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxChooseStoreDirectory.Image")));
            this.pictureBoxChooseStoreDirectory.Location = new System.Drawing.Point(507, 14);
            this.pictureBoxChooseStoreDirectory.Name = "pictureBoxChooseStoreDirectory";
            this.pictureBoxChooseStoreDirectory.Size = new System.Drawing.Size(36, 44);
            this.pictureBoxChooseStoreDirectory.TabIndex = 8;
            this.pictureBoxChooseStoreDirectory.TabStop = false;
            this.pictureBoxChooseStoreDirectory.Click += new System.EventHandler(this.pictureBoxChooseStoreDirectory_Click);
            // 
            // buttonSaveApplicationSettings
            // 
            this.buttonSaveApplicationSettings.Location = new System.Drawing.Point(23, 64);
            this.buttonSaveApplicationSettings.Name = "buttonSaveApplicationSettings";
            this.buttonSaveApplicationSettings.Size = new System.Drawing.Size(150, 30);
            this.buttonSaveApplicationSettings.TabIndex = 4;
            this.buttonSaveApplicationSettings.Text = "Save application settings";
            this.buttonSaveApplicationSettings.UseVisualStyleBackColor = true;
            this.buttonSaveApplicationSettings.Visible = false;
            this.buttonSaveApplicationSettings.Click += new System.EventHandler(this.buttonSaveApplicationSettings_Click);
            // 
            // labelStoreDirectory
            // 
            this.labelStoreDirectory.AutoSize = true;
            this.labelStoreDirectory.Location = new System.Drawing.Point(20, 26);
            this.labelStoreDirectory.Name = "labelStoreDirectory";
            this.labelStoreDirectory.Size = new System.Drawing.Size(78, 13);
            this.labelStoreDirectory.TabIndex = 1;
            this.labelStoreDirectory.Text = "Store directory:";
            // 
            // textBoxStoreDirectory
            // 
            this.textBoxStoreDirectory.Location = new System.Drawing.Point(104, 23);
            this.textBoxStoreDirectory.Name = "textBoxStoreDirectory";
            this.textBoxStoreDirectory.Size = new System.Drawing.Size(397, 20);
            this.textBoxStoreDirectory.TabIndex = 0;
            this.textBoxStoreDirectory.TextChanged += new System.EventHandler(this.textBoxStoreDirectory_TextChanged);
            // 
            // tabPageCapturing
            // 
            this.tabPageCapturing.Controls.Add(this.tableLayoutPanel1);
            this.tabPageCapturing.Controls.Add(this.groupBoxLog);
            this.tabPageCapturing.Controls.Add(this.buttonCancelSequence);
            this.tabPageCapturing.Location = new System.Drawing.Point(4, 22);
            this.tabPageCapturing.Name = "tabPageCapturing";
            this.tabPageCapturing.Size = new System.Drawing.Size(888, 494);
            this.tabPageCapturing.TabIndex = 3;
            this.tabPageCapturing.Text = "Capturing";
            this.tabPageCapturing.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.richTextBoxShannonEntropy, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonClearShannonEntropy, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonClearContrastMeasurment, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label6, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.richTextBoxContrastMeasurment, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(11, 22);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.815951F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.18405F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(839, 179);
            this.tableLayoutPanel1.TabIndex = 24;
            // 
            // richTextBoxShannonEntropy
            // 
            this.richTextBoxShannonEntropy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxShannonEntropy.Location = new System.Drawing.Point(422, 17);
            this.richTextBoxShannonEntropy.Name = "richTextBoxShannonEntropy";
            this.richTextBoxShannonEntropy.Size = new System.Drawing.Size(414, 128);
            this.richTextBoxShannonEntropy.TabIndex = 25;
            this.richTextBoxShannonEntropy.Text = "";
            // 
            // buttonClearShannonEntropy
            // 
            this.buttonClearShannonEntropy.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonClearShannonEntropy.Location = new System.Drawing.Point(789, 151);
            this.buttonClearShannonEntropy.Name = "buttonClearShannonEntropy";
            this.buttonClearShannonEntropy.Size = new System.Drawing.Size(47, 25);
            this.buttonClearShannonEntropy.TabIndex = 19;
            this.buttonClearShannonEntropy.Text = "Clear";
            this.buttonClearShannonEntropy.UseVisualStyleBackColor = true;
            // 
            // buttonClearContrastMeasurment
            // 
            this.buttonClearContrastMeasurment.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonClearContrastMeasurment.Location = new System.Drawing.Point(376, 151);
            this.buttonClearContrastMeasurment.Name = "buttonClearContrastMeasurment";
            this.buttonClearContrastMeasurment.Size = new System.Drawing.Size(40, 25);
            this.buttonClearContrastMeasurment.TabIndex = 2;
            this.buttonClearContrastMeasurment.Text = "Clear";
            this.buttonClearContrastMeasurment.UseVisualStyleBackColor = true;
            this.buttonClearContrastMeasurment.Click += new System.EventHandler(this.buttonClearJ_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(422, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Shannon entropy";
            // 
            // richTextBoxContrastMeasurment
            // 
            this.richTextBoxContrastMeasurment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxContrastMeasurment.Location = new System.Drawing.Point(3, 17);
            this.richTextBoxContrastMeasurment.Name = "richTextBoxContrastMeasurment";
            this.richTextBoxContrastMeasurment.Size = new System.Drawing.Size(413, 128);
            this.richTextBoxContrastMeasurment.TabIndex = 23;
            this.richTextBoxContrastMeasurment.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Contrast measurment";
            // 
            // groupBoxLog
            // 
            this.groupBoxLog.Controls.Add(this.panel2);
            this.groupBoxLog.Controls.Add(this.panelCapturingProgress);
            this.groupBoxLog.Location = new System.Drawing.Point(8, 267);
            this.groupBoxLog.Name = "groupBoxLog";
            this.groupBoxLog.Size = new System.Drawing.Size(893, 211);
            this.groupBoxLog.TabIndex = 17;
            this.groupBoxLog.TabStop = false;
            this.groupBoxLog.Text = "Log";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.richTextBoxSession);
            this.panel2.Controls.Add(this.buttonClearLog);
            this.panel2.Controls.Add(this.radioButtonDebug);
            this.panel2.Controls.Add(this.radioButtonTrace);
            this.panel2.Controls.Add(this.radioButtonInfo);
            this.panel2.Location = new System.Drawing.Point(3, 16);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(816, 145);
            this.panel2.TabIndex = 3;
            // 
            // richTextBoxSession
            // 
            this.richTextBoxSession.Dock = System.Windows.Forms.DockStyle.Left;
            this.richTextBoxSession.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxSession.Name = "richTextBoxSession";
            this.richTextBoxSession.Size = new System.Drawing.Size(673, 145);
            this.richTextBoxSession.TabIndex = 10;
            this.richTextBoxSession.Text = "";
            // 
            // buttonClearLog
            // 
            this.buttonClearLog.Location = new System.Drawing.Point(720, 88);
            this.buttonClearLog.Name = "buttonClearLog";
            this.buttonClearLog.Size = new System.Drawing.Size(77, 23);
            this.buttonClearLog.TabIndex = 9;
            this.buttonClearLog.Text = "Clear log";
            this.buttonClearLog.UseVisualStyleBackColor = true;
            this.buttonClearLog.Click += new System.EventHandler(this.buttonClearLog_Click);
            // 
            // radioButtonDebug
            // 
            this.radioButtonDebug.AutoSize = true;
            this.radioButtonDebug.Checked = true;
            this.radioButtonDebug.Location = new System.Drawing.Point(720, 39);
            this.radioButtonDebug.Name = "radioButtonDebug";
            this.radioButtonDebug.Size = new System.Drawing.Size(57, 17);
            this.radioButtonDebug.TabIndex = 8;
            this.radioButtonDebug.TabStop = true;
            this.radioButtonDebug.Text = "Debug";
            this.radioButtonDebug.UseVisualStyleBackColor = true;
            this.radioButtonDebug.CheckedChanged += new System.EventHandler(this.radioButtonLog_CheckedChanged);
            // 
            // radioButtonTrace
            // 
            this.radioButtonTrace.AutoSize = true;
            this.radioButtonTrace.Location = new System.Drawing.Point(720, 65);
            this.radioButtonTrace.Name = "radioButtonTrace";
            this.radioButtonTrace.Size = new System.Drawing.Size(53, 17);
            this.radioButtonTrace.TabIndex = 7;
            this.radioButtonTrace.Text = "Trace";
            this.radioButtonTrace.UseVisualStyleBackColor = true;
            this.radioButtonTrace.CheckedChanged += new System.EventHandler(this.radioButtonLog_CheckedChanged);
            // 
            // radioButtonInfo
            // 
            this.radioButtonInfo.AutoSize = true;
            this.radioButtonInfo.Location = new System.Drawing.Point(720, 16);
            this.radioButtonInfo.Name = "radioButtonInfo";
            this.radioButtonInfo.Size = new System.Drawing.Size(43, 17);
            this.radioButtonInfo.TabIndex = 6;
            this.radioButtonInfo.Text = "Info";
            this.radioButtonInfo.UseVisualStyleBackColor = true;
            this.radioButtonInfo.CheckedChanged += new System.EventHandler(this.radioButtonLog_CheckedChanged);
            // 
            // panelCapturingProgress
            // 
            this.panelCapturingProgress.Controls.Add(this.labelElapsedTime);
            this.panelCapturingProgress.Controls.Add(this.labelRemainingTime);
            this.panelCapturingProgress.Controls.Add(this.label4);
            this.panelCapturingProgress.Controls.Add(this.label3);
            this.panelCapturingProgress.Controls.Add(this.progressBarCapturingProgress);
            this.panelCapturingProgress.Location = new System.Drawing.Point(3, 167);
            this.panelCapturingProgress.Name = "panelCapturingProgress";
            this.panelCapturingProgress.Size = new System.Drawing.Size(951, 44);
            this.panelCapturingProgress.TabIndex = 2;
            // 
            // labelElapsedTime
            // 
            this.labelElapsedTime.AutoSize = true;
            this.labelElapsedTime.Location = new System.Drawing.Point(65, 3);
            this.labelElapsedTime.Name = "labelElapsedTime";
            this.labelElapsedTime.Size = new System.Drawing.Size(0, 13);
            this.labelElapsedTime.TabIndex = 5;
            // 
            // labelRemainingTime
            // 
            this.labelRemainingTime.AutoSize = true;
            this.labelRemainingTime.Location = new System.Drawing.Point(789, 3);
            this.labelRemainingTime.Name = "labelRemainingTime";
            this.labelRemainingTime.Size = new System.Drawing.Size(0, 13);
            this.labelRemainingTime.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(707, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Remainig:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Elapsed:";
            // 
            // progressBarCapturingProgress
            // 
            this.progressBarCapturingProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBarCapturingProgress.Location = new System.Drawing.Point(0, 25);
            this.progressBarCapturingProgress.Name = "progressBarCapturingProgress";
            this.progressBarCapturingProgress.Size = new System.Drawing.Size(951, 19);
            this.progressBarCapturingProgress.Step = 1;
            this.progressBarCapturingProgress.TabIndex = 1;
            // 
            // buttonCancelSequence
            // 
            this.buttonCancelSequence.Location = new System.Drawing.Point(8, 207);
            this.buttonCancelSequence.Name = "buttonCancelSequence";
            this.buttonCancelSequence.Size = new System.Drawing.Size(118, 47);
            this.buttonCancelSequence.TabIndex = 1;
            this.buttonCancelSequence.Text = "Cancel sequence";
            this.buttonCancelSequence.UseVisualStyleBackColor = true;
            this.buttonCancelSequence.Click += new System.EventHandler(this.buttonCancelSequence_Click);
            // 
            // tabPageTests
            // 
            this.tabPageTests.Controls.Add(this.buttonTestSerialization);
            this.tabPageTests.Controls.Add(this.buttonTestChart);
            this.tabPageTests.Location = new System.Drawing.Point(4, 22);
            this.tabPageTests.Name = "tabPageTests";
            this.tabPageTests.Size = new System.Drawing.Size(888, 494);
            this.tabPageTests.TabIndex = 4;
            this.tabPageTests.Text = "Tests";
            this.tabPageTests.UseVisualStyleBackColor = true;
            // 
            // buttonTestSerialization
            // 
            this.buttonTestSerialization.Location = new System.Drawing.Point(189, 67);
            this.buttonTestSerialization.Name = "buttonTestSerialization";
            this.buttonTestSerialization.Size = new System.Drawing.Size(159, 23);
            this.buttonTestSerialization.TabIndex = 13;
            this.buttonTestSerialization.Text = "TestSerialization";
            this.buttonTestSerialization.UseVisualStyleBackColor = true;
            this.buttonTestSerialization.Click += new System.EventHandler(this.buttonTestSerialization_Click);
            // 
            // buttonTestChart
            // 
            this.buttonTestChart.Location = new System.Drawing.Point(108, 67);
            this.buttonTestChart.Name = "buttonTestChart";
            this.buttonTestChart.Size = new System.Drawing.Size(75, 23);
            this.buttonTestChart.TabIndex = 12;
            this.buttonTestChart.Text = "TestChart";
            this.buttonTestChart.UseVisualStyleBackColor = true;
            this.buttonTestChart.Click += new System.EventHandler(this.buttonTestChart_Click);
            // 
            // errorProviderEx
            // 
            this.errorProviderEx.ContainerControl = this;
            // 
            // timerEx
            // 
            this.timerEx.Interval = 1000;
            this.timerEx.Tick += new System.EventHandler(this.timerEx_Tick);
            // 
            // FormExp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 569);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStripEx);
            this.Controls.Add(this.menuStripEx);
            this.MainMenuStrip = this.menuStripEx;
            this.Name = "FormExp";
            this.Text = "TimeLapseExp - Expertomica Time-Lapse Microscopy Capture Tool";
            this.Load += new System.EventHandler(this.FormEx_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormEx_FormClosed);
            this.groupBoxCameraInfo.ResumeLayout(false);
            this.groupBoxCameraInfo.PerformLayout();
            this.groupBoxExposureSettings.ResumeLayout(false);
            this.groupBoxExposureSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxAperture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExposureTime)).EndInit();
            this.groupBoxCaptureSettings.ResumeLayout(false);
            this.groupBoxCaptureSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxContinuousNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxContinuousTime)).EndInit();
            this.groupBoxFocusSettings.ResumeLayout(false);
            this.groupBoxFocusSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxFocusPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxOpticalZoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExposureBias)).EndInit();
            this.menuStripEx.ResumeLayout(false);
            this.menuStripEx.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageCamera.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.tabPageCaptureSettings.ResumeLayout(false);
            this.tabPageCaptureSettings.PerformLayout();
            this.groupBoxSequenceCapture.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxFocusPositionStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxFocusSequenceCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxSequenceCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxTimeSpan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExposureTimeStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExposureSequenceCount)).EndInit();
            this.tabPageApplicationSettings.ResumeLayout(false);
            this.tabPageApplicationSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxChooseStoreDirectory)).EndInit();
            this.tabPageCapturing.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBoxLog.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelCapturingProgress.ResumeLayout(false);
            this.panelCapturingProgress.PerformLayout();
            this.tabPageTests.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderEx)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripEx;
        private System.ComponentModel.BackgroundWorker backgroundWorkerEx;
        private System.Windows.Forms.Timer timerStatusStripEx;
        private System.Windows.Forms.ToolTip toolTipEx;
        private System.Windows.Forms.StatusStrip statusStripEx;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogEx;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelError;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBarInicialization;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageCamera;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.ListBox listBoxCamera;
        private System.Windows.Forms.TabPage tabPageCaptureSettings;
        private System.Windows.Forms.TabPage tabPageApplicationSettings;
        private System.Windows.Forms.Button buttonSaveApplicationSettings;
        private System.Windows.Forms.Label labelStoreDirectory;
        private System.Windows.Forms.TextBox textBoxStoreDirectory;
        private System.Windows.Forms.TabPage tabPageCapturing;
        private System.Windows.Forms.Button buttonPreview;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.Button buttonRegisterCamera;
        private System.Windows.Forms.Button buttonInstallDriver;
        private System.Windows.Forms.ErrorProvider errorProviderEx;
        private System.Windows.Forms.Button buttonCancelSequence;
        private System.Windows.Forms.Button buttonClearContrastMeasurment;
        private System.Windows.Forms.Timer timerEx;
        private System.Windows.Forms.GroupBox groupBoxCameraInfo;
        private System.Windows.Forms.Label labelContrastMeasurment;
        private System.Windows.Forms.Button buttonCleanFlash;
        private System.Windows.Forms.GroupBox groupBoxExposureSettings;
        private System.Windows.Forms.NumericUpDown textBoxAperture;
        private System.Windows.Forms.NumericUpDown textBoxExposureTime;
        private System.Windows.Forms.Label labelExposureMode;
        private System.Windows.Forms.Label labelAperture;
        private System.Windows.Forms.Label labelExposureTime;
        private System.Windows.Forms.ComboBox comboBoxExposureMode;
        private System.Windows.Forms.GroupBox groupBoxCaptureSettings;
        private System.Windows.Forms.NumericUpDown textBoxContinuousNumber;
        private System.Windows.Forms.NumericUpDown textBoxContinuousTime;
        private System.Windows.Forms.Label labelContinuousNumber;
        private System.Windows.Forms.Label labelContinuousTime;
        private System.Windows.Forms.Label labelCaptureMode;
        private System.Windows.Forms.ComboBox comboBoxCaptureMode;
        private System.Windows.Forms.GroupBox groupBoxFocusSettings;
        private System.Windows.Forms.ComboBox comboBoxFocusMode;
        private System.Windows.Forms.Label labelFocusMode;
        private System.Windows.Forms.NumericUpDown textBoxFocusPosition;
        private System.Windows.Forms.Label labelFocusPosition;
        private System.Windows.Forms.Button buttonTestCapture;
        private System.Windows.Forms.Button buttonSaveToFile;
        private System.Windows.Forms.NumericUpDown textBoxOpticalZoom;
        private System.Windows.Forms.Label labelOpticalZoom;
        private System.Windows.Forms.Button buttonSendToCamera;
        private System.Windows.Forms.ComboBox comboBoxWhiteBalance;
        private System.Windows.Forms.NumericUpDown textBoxExposureBias;
        private System.Windows.Forms.ComboBox comboBoxISOSpeed;
        private System.Windows.Forms.ComboBox comboBoxResolution;
        private System.Windows.Forms.Label labelWhiteBalance;
        private System.Windows.Forms.Label labelISOSpeed;
        private System.Windows.Forms.Label labelResolution;
        private System.Windows.Forms.Label labelExposureBias;
        private System.Windows.Forms.Label labelFlashMode;
        private System.Windows.Forms.ComboBox comboBoxFlashMode;
        private TextBox textBoxShannonEntropy;
        private Label labelShannonEntropy;
        private GroupBox groupBoxLog;
        private Panel panel2;
        private Button buttonClearLog;
        private RadioButton radioButtonDebug;
        private RadioButton radioButtonTrace;
        private RadioButton radioButtonInfo;
        private Panel panelCapturingProgress;
        private Label labelElapsedTime;
        private Label labelRemainingTime;
        private Label label4;
        private Label label3;
        private ProgressBar progressBarCapturingProgress;
        private Button buttonClearShannonEntropy;
        private TextBox textBoxContrastMeasurment;
        private CheckBox checkBoxCopyImages;
        private CheckBox checkBoxlContrastMeasurment;
        private CheckBox checkBoxShannonEntropy;
        private GroupBox groupBoxSequenceCapture;
        private RadioButton radioButtonFocusPriority;
        private RadioButton radioButtonExposurePriority;
        private Label labelFocusSequenceCount;
        private NumericUpDown textBoxFocusSequenceCount;
        private Label labelFocusPositionStep;
        private NumericUpDown textBoxFocusPositionStep;
        private Label labelExposureTimeStep;
        private NumericUpDown textBoxExposureTimeStep;
        private Label labeExposureSequenceCount;
        private Button buttonCaptureSequence;
        private Label label1;
        private Label label2;
        private NumericUpDown textBoxExposureSequenceCount;
        private NumericUpDown textBoxSequenceCount;
        private NumericUpDown textBoxTimeSpan;
        private Button buttonCopyAllImages;
        private Button buttonDisconnect;
        private Button buttonConnect;
        private CheckBox checkBoxEraseAllPictures;
        private TableLayoutPanel tableLayoutPanel1;
        private RichTextBox richTextBoxShannonEntropy;
        private RichTextBox richTextBoxContrastMeasurment;
        private Label label5;
        private Label label6;
        private RichTextBox richTextBoxSession;
        private PictureBox pictureBoxChooseStoreDirectory;
        private CheckBox checkBoxPlotShannonEntropy;
        private TableLayoutPanel tableLayoutPanel2;
        private TabPage tabPageTests;
        private ToolStripMenuItem toolStripMenuItemCalculator;
        private ToolStripMenuItem toolStripMenuItemOpenCurrentStoreDirectory;
        private Button buttonTestSerialization;
        private Button buttonTestChart;
    }
}