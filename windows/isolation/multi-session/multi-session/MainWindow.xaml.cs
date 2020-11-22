using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Description;
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
using System.Diagnostics;
using System.Configuration;
using System.Security;
using System.Threading;
using System.IO.MemoryMappedFiles;
using System.IO;
using child;





namespace multi_session
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string machine = @"Global\";
        private string session = @"Local\";

        private MemoryMappedFile _mFile;
        private int _size = 0xffff;
        private MemoryMappedFile _sFile;
        private Mutex _mWait;
        private Mutex _sWait;
        private ServiceHost _mServer;
        private string _m;
        private string _s;
        private TestClient _sclient;
        private TestClient _mclient;

        public MainWindow()
        {
            

            InitializeComponent();
            _s = "net.pipe://localhost/session/" + Process.GetCurrentProcess().SessionId + "/" + typeof(ITestService);
            _m = "net.pipe://localhost/machine/" + typeof(ITestService);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var w = new Window { Topmost = true, ShowInTaskbar = true, ShowActivated = true };
            w.Show();
            writeLineOk(display(sender));
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string[] names = System.Configuration.ConfigurationManager.AppSettings["processes"].Split(';');

            string data = (from name in names from p in Process.GetProcessesByName(name) select p.ProcessName + " " + p.Id + " " + p.SessionId).Aggregate((x, y) => x + Environment.NewLine + y);
            writeLine(display(sender) + ":" + newLine + data);
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _mFile = MemoryMappedFile.CreateOrOpen(machine + "shared.memory" + suffix, _size);
            using (var x = _mFile.CreateViewStream())
            {
                x.WriteByte((byte)DateTime.Now.Second);
            }
        }



        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var x = MemoryMappedFile.OpenExisting(machine + "shared.memory" + suffix).CreateViewStream())
                {
                    var value = x.ReadByte();
                    writeLine(display(sender) + ": " + value);
                }

            }
            catch (Exception ex)
            {
                writeError(display(sender), ex);
            }
        }

        private void writeError(string message, Exception error)
        {
            logTextBox.Text += message + ":" + error.Message + newLine;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            _sFile = MemoryMappedFile.CreateOrOpen(session + "shared.memory" + suffix, _size);
            using (var x = _sFile.CreateViewStream())
            {
                x.WriteByte((byte)DateTime.Now.Second);
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var x = MemoryMappedFile.OpenExisting(session + "shared.memory" + suffix).CreateViewStream())
                {
                    var value = x.ReadByte();
                    writeLine(display(sender) + ": " + value);
                }
            }
            catch (Exception ex)
            {
                writeError(display(sender), ex);
            }
        }



        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            bool dummy;
            _mWait = new Mutex(true, machine + suffix);
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            try
            {
                var m = Mutex.OpenExisting(machine + suffix,MutexRights.Synchronize);
                writeLineOk(display(sender) + ":" + m.SafeWaitHandle);
            }
            catch (Exception ex)
            {
                writeError(display(sender), ex);
            }

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            _sWait = new Mutex(true, session + suffix);
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            try
            {
                var m = Mutex.OpenExisting(session + suffix);
                writeLineOk(display(sender) + ":" + m.SafeWaitHandle);
            }
            catch (Exception ex)
            {
                writeError(display(sender), ex);
            }
        }


        private string suffix
        {
            get
            {
                return this.GetType().FullName;
            }
        }
        private string newLine { get { return Environment.NewLine; } }




        private static string display(object sender)
        {
            return (sender as Button).Content.ToString();
        }

        private void writeLine(string message)
        {
            logTextBox.Text += message + newLine;
        }


        private void writeLineOk(string message)
        {
            logTextBox.Text += message + ": Succeed" + newLine;
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            try
            {
                var current = System.Security.Principal.WindowsIdentity.GetCurrent();
                StringBuilder sb = new StringBuilder();
                sb.Append("USER=" + current.Name + " ").Append(current.User.Value).AppendLine();
                sb.Append("REMOTESESSION=" + System.Windows.Forms.SystemInformation.TerminalServerSession).AppendLine();
                var local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var roaming = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                sb.Append(local).AppendLine().Append(roaming).AppendLine();
                writeLine(display(sender) + ":" + newLine + sb);
            }
            catch (Exception ex)
            {
                writeError(display(sender), ex);
            }
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {

            start(sender, _s);
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            Process.Start("child.exe");
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            
            start(sender, _m);
        }

        private void start(object sender,string address)
        {
            ThreadPool.QueueUserWorkItem((x) =>
                                             {
                                                 try
                                                 {
                                                     ServiceHost serviceHost = new ServiceHost(typeof (TestService));
                                                     NetNamedPipeBinding binding = new NetNamedPipeBinding();
                                                     serviceHost.AddServiceEndpoint(typeof(ITestService), binding, address);
                                                     serviceHost.Open();
                                                     Dispatcher.Invoke(new Action(() => writeLine(display(sender) + " : " + address)));
                                                 }
                                                 catch (Exception ex)
                                                 {
                                                     Dispatcher.Invoke(new Action(() => writeError(display(sender), ex)));
                                                 }
                                             });
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_mclient == null) _mclient = new TestClient(_m);
                writeLine(display(sender) + " : " + _mclient.GetTestData());
            }
            catch (Exception ex)
            {
                writeError(display(sender), ex);
            }

        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_sclient==null) _sclient = new TestClient(_s);
                writeLine(display(sender) + " : " + _sclient.GetTestData());
            }
            catch (Exception ex)
            {
                writeError(display(sender), ex);
            }
        }
    }
}
