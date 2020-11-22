using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Application = System.Windows.Application;

namespace child
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Mutex _mutex;
        private MainWindow _w;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                Mutex mutex = Mutex.OpenExisting(this.GetType().FullName);
            }
            catch (Exception)
            {
                _w = new MainWindow { Topmost = true, ShowInTaskbar = true, ShowActivated = true };
                _w.Show();

                System.Windows.Forms.Form tray = new Form();
                var trayMenu = new System.Windows.Forms.ContextMenu();
                trayMenu.MenuItems.Add("Exit");
                var trayIcon = new NotifyIcon();
                trayIcon.Text = "Test tray";
                trayIcon.Icon = new Icon(SystemIcons.Application, 40, 40);
                trayIcon.ContextMenu = trayMenu;
                trayIcon.Visible = true;
                tray.ContextMenu = trayMenu;
                tray.ShowInTaskbar = false;
                tray.Visible = false;
                tray.Show();
                tray.Hide();

                _mutex = new Mutex(true, this.GetType().FullName);
                return;
            }
            Environment.Exit(0);
        }



        protected override void OnLoadCompleted(System.Windows.Navigation.NavigationEventArgs e)
        {
            Debugger.Launch();


        }
    }
}
