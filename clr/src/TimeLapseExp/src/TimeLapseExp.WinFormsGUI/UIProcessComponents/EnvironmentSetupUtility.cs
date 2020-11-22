using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeLapseExp.WinFormsGUI
{
    /// <summary>
    /// Provides functionality for registering driver and camera in Windows.
    /// </summary>
    //TODO:Move to separate assembly and use any of Extensibility Framework.
    public class EnvironmentSetupUtility
    {
        /// <summary>
        /// Installs specified Olympus Rye Camera  in Windows.
        /// </summary>
        public void InstallCamera()
        {
            //TODO:Rewrite in C#.
            Process.Start(@"UseFiltject.exe",@"Cameras.ini");
        }

        /// <summary>
        /// Installs dirver for Olympys Rye cameras.
        /// </summary>
        public void SetupDriver()
        {
            //TODO:Rewrite in C#.
            Process.Start( @"AddService.exe");
        }
    }
}
