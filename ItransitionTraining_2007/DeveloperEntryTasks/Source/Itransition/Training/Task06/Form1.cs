using System;
using System.Windows.Forms;
using System.Threading;
using System.Configuration;
using System.Globalization;

namespace Itransition.Training.Task06
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void language1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                /*CultureInfo ci = new CultureInfo("ru-RU");
                // set culture for formatting
                Thread.CurrentThread.CurrentCulture = ci;
                // set culture for resources
                Thread.CurrentThread.CurrentUICulture = ci;*/
                 Language.Deserialize("language2", this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void language2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                /*CultureInfo ci = new CultureInfo("en-US");
                // set culture for formatting
                Thread.CurrentThread.CurrentCulture = ci;
                // set culture for resources
                Thread.CurrentThread.CurrentUICulture = ci;*/
                Language.Deserialize("language1", this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
   }
}