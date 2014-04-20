namespace Itransition.Training.Task06
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.language1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.language2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Font = null;
            this.label1.Name = "label1";
            // 
            // button1
            // 
            this.button1.AccessibleDescription = null;
            this.button1.AccessibleName = null;
            resources.ApplyResources(this.button1, "button1");
            this.button1.BackgroundImage = null;
            this.button1.Font = null;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.AccessibleDescription = null;
            this.menuStrip1.AccessibleName = null;
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.BackgroundImage = null;
            this.menuStrip1.Font = null;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.language1ToolStripMenuItem,
            this.language2ToolStripMenuItem});
            this.menuStrip1.Name = "menuStrip1";
            // 
            // language1ToolStripMenuItem
            // 
            this.language1ToolStripMenuItem.AccessibleDescription = null;
            this.language1ToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.language1ToolStripMenuItem, "language1ToolStripMenuItem");
            this.language1ToolStripMenuItem.BackgroundImage = null;
            this.language1ToolStripMenuItem.Name = "language1ToolStripMenuItem";
            this.language1ToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.language1ToolStripMenuItem.Click += new System.EventHandler(this.language1ToolStripMenuItem_Click);
            // 
            // language2ToolStripMenuItem
            // 
            this.language2ToolStripMenuItem.AccessibleDescription = null;
            this.language2ToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.language2ToolStripMenuItem, "language2ToolStripMenuItem");
            this.language2ToolStripMenuItem.BackgroundImage = null;
            this.language2ToolStripMenuItem.Name = "language2ToolStripMenuItem";
            this.language2ToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.language2ToolStripMenuItem.Click += new System.EventHandler(this.language2ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Font = null;
            this.Icon = null;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem language1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem language2ToolStripMenuItem;
    }
}

