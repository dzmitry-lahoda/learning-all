using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace child
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var text = GetUser() + "; Thread " + Thread.CurrentPrincipal.Identity.Name + "; Session " + Process.GetCurrentProcess().SessionId + Environment.NewLine;

                using (new Impersonator(config("user.name"), config("user.domain"), config("user.password")))
                {
                    text += GetUser() + "; Thread " + Thread.CurrentPrincipal.Identity.Name + "; Session " + Process.GetCurrentProcess().SessionId + Environment.NewLine;
                }
                logTextBox.Text = text;
            }
            catch (Exception ex)
            {
                logTextBox.Text = ex.Message;
            }

        }

        private string config(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        private string GetUser()
        {
            var c = System.Security.Principal.WindowsIdentity.GetCurrent();
            var repr = c.Name + " " + c.User.Value;
            return repr;
        }
    }
}
