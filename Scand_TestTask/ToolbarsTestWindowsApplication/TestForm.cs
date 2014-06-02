using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Scand.TestTask
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        private void buttonLoadXml_Click(object sender, EventArgs e)
        {

            toolbarTest.LoadXml(comboBoxLoadXml.Text);
        }

        private void buttonSetButtonImage_Click(object sender, EventArgs e)
        {
            toolbarTest.SetButtonImage(comboBoxId.Text, comboBoxUri.Text);
        }

        private void buttonLoadSet_Click(object sender, EventArgs e)
        {
            buttonLoadXml_Click(sender, e);
            buttonSetButtonImage_Click(sender, e);
        }
    }
}