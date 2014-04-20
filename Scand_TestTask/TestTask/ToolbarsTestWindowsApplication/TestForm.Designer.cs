using Scand.Toolbars;

namespace Scand.TestTask
{
    partial class TestForm
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
            this.buttonSetButtonImage = new System.Windows.Forms.Button();
            this.buttonLoadXml = new System.Windows.Forms.Button();
            this.comboBoxId = new System.Windows.Forms.ComboBox();
            this.comboBoxUri = new System.Windows.Forms.ComboBox();
            this.comboBoxLoadXml = new System.Windows.Forms.ComboBox();
            this.buttonLoadSet = new System.Windows.Forms.Button();
            this.toolbarTest = new Scand.Toolbars.Toolbar();
            this.SuspendLayout();
            // 
            // buttonSetButtonImage
            // 
            this.buttonSetButtonImage.Location = new System.Drawing.Point(12, 60);
            this.buttonSetButtonImage.Name = "buttonSetButtonImage";
            this.buttonSetButtonImage.Size = new System.Drawing.Size(257, 23);
            this.buttonSetButtonImage.TabIndex = 1;
            this.buttonSetButtonImage.Text = "Назначить картинку для заданной кнопки";
            this.buttonSetButtonImage.UseVisualStyleBackColor = true;
            this.buttonSetButtonImage.Click += new System.EventHandler(this.buttonSetButtonImage_Click);
            // 
            // buttonLoadXml
            // 
            this.buttonLoadXml.Location = new System.Drawing.Point(12, 12);
            this.buttonLoadXml.Name = "buttonLoadXml";
            this.buttonLoadXml.Size = new System.Drawing.Size(121, 31);
            this.buttonLoadXml.TabIndex = 2;
            this.buttonLoadXml.Text = "Загрузить xml";
            this.buttonLoadXml.UseVisualStyleBackColor = true;
            this.buttonLoadXml.Click += new System.EventHandler(this.buttonLoadXml_Click);
            // 
            // comboBoxId
            // 
            this.comboBoxId.FormattingEnabled = true;
            this.comboBoxId.Items.AddRange(new object[] {
            "btn1",
            "btn2",
            "btn3",
            "btn4",
            "noSuchButton"});
            this.comboBoxId.Location = new System.Drawing.Point(12, 89);
            this.comboBoxId.Name = "comboBoxId";
            this.comboBoxId.Size = new System.Drawing.Size(121, 21);
            this.comboBoxId.TabIndex = 4;
            // 
            // comboBoxUri
            // 
            this.comboBoxUri.FormattingEnabled = true;
            this.comboBoxUri.Items.AddRange(new object[] {
            "http://localhost/mysite/img/img.gif",
            "http://localhost/mysite/img/img1.gif",
            "http://localhost/mysite/img/img2.gif",
            "http://localhost/mysite/img/img3.gif",
            "http://localhost/mysite/img/img4.gif",
            "http://localhost/mysite/img/imgN.gif",
            "http://localhost/mysite/img/imgBig.bmp",
            "http://localhost/mysite/img/notimg.txt"});
            this.comboBoxUri.Location = new System.Drawing.Point(139, 89);
            this.comboBoxUri.Name = "comboBoxUri";
            this.comboBoxUri.Size = new System.Drawing.Size(491, 21);
            this.comboBoxUri.TabIndex = 5;
            // 
            // comboBoxLoadXml
            // 
            this.comboBoxLoadXml.AutoCompleteCustomSource.AddRange(new string[] {
            "            <option>http://localhost/mysite/res/correctAll.xml</option>",
            "http://localhost/mysite/res/correctExternalUris.xml",
            "http://localhost/mysite/res/correctManyImages.xml",
            "http://localhost/mysite/res/correctNoImage.xml",
            "http://localhost/mysite/res/correctNoText.xml",
            "http://localhost/mysite/res/notcorrectEmptyId.xml",
            "http://localhost/mysite/res/notcorrectImageIsNotImage.xml",
            "http://localhost/mysite/res/notcorrectNoId.xml</option>",
            "http://localhost/mysite/res/notcorrectNoImageNoText.xml",
            "http://localhost/mysite/res/notcorrectUnableToGetImage.xml",
            "http://localhost/mysite/res/notcorrectWrongImgUri.xml",
            "http://localhost/mysite/res/notcorrectXml1.xml.txt",
            "http://localhost/mysite/res/notcorrectXml2.xml.txt",
            "http://localhost/mysite/res/nosuchxmlfile.xml",
            "Bad formed Uri."});
            this.comboBoxLoadXml.FormattingEnabled = true;
            this.comboBoxLoadXml.Items.AddRange(new object[] {
            "http://localhost/mysite/res/correctAll.xml",
            "http://localhost/mysite/res/correctExternalUris.xml",
            "http://localhost/mysite/res/correctManyImages.xml",
            "http://localhost/mysite/res/correctNoImage.xml",
            "http://localhost/mysite/res/correctNoText.xml",
            "http://localhost/mysite/res/correctWithBigImage.xml",
            "http://localhost/mysite/res/notcorrectEmptyId.xml",
            "http://localhost/mysite/res/notcorrectImageIsNotImage.xml",
            "http://localhost/mysite/res/notcorrectNoId.xml",
            "http://localhost/mysite/res/notcorrectNoImageNoText.xml",
            "http://localhost/mysite/res/notcorrectUnableToGetImage.xml",
            "http://localhost/mysite/res/notcorrectWrongImgUri.xml",
            "http://localhost/mysite/res/notcorrectXml1.xml.txt",
            "http://localhost/mysite/res/notcorrectXml2.xml.txt",
            "http://localhost/mysite/res/nosuchxmlfile.xml",
            "Bad formed Uri."});
            this.comboBoxLoadXml.Location = new System.Drawing.Point(139, 18);
            this.comboBoxLoadXml.Name = "comboBoxLoadXml";
            this.comboBoxLoadXml.Size = new System.Drawing.Size(460, 21);
            this.comboBoxLoadXml.TabIndex = 6;
            // 
            // buttonLoadSet
            // 
            this.buttonLoadSet.Location = new System.Drawing.Point(12, 153);
            this.buttonLoadSet.Name = "buttonLoadSet";
            this.buttonLoadSet.Size = new System.Drawing.Size(319, 23);
            this.buttonLoadSet.TabIndex = 7;
            this.buttonLoadSet.Text = "Загрузить xml и назначить картинку для заданной кнопки";
            this.buttonLoadSet.UseVisualStyleBackColor = true;
            this.buttonLoadSet.Click += new System.EventHandler(this.buttonLoadSet_Click);
            // 
            // toolbarTest
            // 
            this.toolbarTest.AutoScroll = true;
            this.toolbarTest.AutoSize = true;
            this.toolbarTest.Location = new System.Drawing.Point(12, 116);
            this.toolbarTest.Name = "toolbarTest";
            this.toolbarTest.Size = new System.Drawing.Size(844, 31);
            this.toolbarTest.TabIndex = 0;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 225);
            this.Controls.Add(this.buttonLoadSet);
            this.Controls.Add(this.comboBoxLoadXml);
            this.Controls.Add(this.comboBoxUri);
            this.Controls.Add(this.comboBoxId);
            this.Controls.Add(this.buttonLoadXml);
            this.Controls.Add(this.buttonSetButtonImage);
            this.Controls.Add(this.toolbarTest);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Toolbar toolbarTest;
        private System.Windows.Forms.Button buttonSetButtonImage;
        private System.Windows.Forms.Button buttonLoadXml;
        private System.Windows.Forms.ComboBox comboBoxId;
        private System.Windows.Forms.ComboBox comboBoxUri;
        private System.Windows.Forms.ComboBox comboBoxLoadXml;
        private System.Windows.Forms.Button buttonLoadSet;
    }
}

