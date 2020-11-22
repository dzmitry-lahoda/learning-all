using System;
using System.Diagnostics;

namespace TimeLapseExp.WinFormsGUI
{
    internal partial class FormExp
    {
        private void toolStripMenuItemCalculator_Click(object sender, EventArgs e)
        {
            Process.Start("calc.exe");
        }

        private void toolStripMenuItemOpenCurrentFolder_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Model.StoreDirectory);
        }
    }
}
