using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

///LIST DIRCTORY
///zapisat' w dervep v xml s razmerami failov
///pos4itat' razmer XML R W
///ls asd.xml
///DOM XML DOCUMENT 

namespace Zadanie2_8
{
    public partial class Form1 : Form
    {
        MD5 md5 = new MD5CryptoServiceProvider();

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                File.Delete(textBox1.Text);
            }
            catch
            {
                MessageBox.Show("Операция завершилась неудачей:(");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                File.Copy(textBox1.Text, textBox2.Text);
            }
            catch
            {
                MessageBox.Show("Операция завершилась неудачей:(");
            }
        }

        private void textBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dlg.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                File.Move(textBox1.Text, textBox2.Text);
            }
            catch
            {
                MessageBox.Show("Операция завершилась неудачей:(");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(textBox1.Text);
                label1.Text=
                    String.Format("{0}\n{1}\n{2}\n{3}\n{4}\n",
                    fileInfo.Name, fileInfo.CreationTime, fileInfo.Extension,fileInfo.IsReadOnly,fileInfo.Length);
            }
            catch
            {
                MessageBox.Show("Операция завершилась неудачей:(");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Directory.CreateDirectory(textBox3.Text);
            }
            catch
            {
                MessageBox.Show("Операция завершилась неудачей:(");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                Directory.Delete(textBox3.Text);
            }
            catch
            {
                MessageBox.Show("Операция завершилась неудачей:(");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                Directory.Move(textBox3.Text, textBox4.Text);
            }
            catch
            {
                MessageBox.Show("Операция завершилась неудачей:(");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(textBox3.Text);
                label2.Text = 
                    String.Format("{0}\n{1}\n{2}\n{3}\n{4}\n",
                    directoryInfo.Name, 
                    directoryInfo.CreationTime, 
                    directoryInfo.Extension, 
                    directoryInfo.Parent, 
                    directoryInfo.Root);
                
            }
            catch
            {
                MessageBox.Show("Операция завершилась неудачей:(");
            }
        }

        private void textBox3_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = fbd.SelectedPath;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                try
                {
                    using (StreamReader textReader = File.OpenText(textBox1.Text))
                    {
                        label1.Text = textReader.ReadToEnd();
                    }
                }
                catch
                {
                    MessageBox.Show("Операция завершилась неудачей:(");
                }
            }
            else
            {
                try
                {
                    String text;
                    using (StreamReader textReader = File.OpenText(textBox1.Text))
                    {
                        label1.Text = textReader.ReadToEnd().Replace("123345", "a");
                    }
                }
                catch
                {
                    MessageBox.Show("Операция завершилась неудачей:(");
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(new FileStream(textBox1.Text,FileMode.Append)))
                {
                    streamWriter.WriteLine(textBox2.Text);
                }
                using (StreamReader textReader = File.OpenText(textBox1.Text))
                {
                    label1.Text = textReader.ReadToEnd();
                }
            }
            catch
            {
                MessageBox.Show("Операция завершилась неудачей:(");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                String text;
                using (StreamReader textReader = File.OpenText(textBox1.Text))
                {
                    text = textReader.ReadToEnd().Replace("a", "123345");
                }

                using (StreamWriter streamWriter = new StreamWriter(new FileStream(textBox1.Text, FileMode.Create)))
                {
                    streamWriter.WriteLine(text);
                }

            }
            catch
            {
                MessageBox.Show("Операция завершилась неудачей:(");
            }
        }
    }
}