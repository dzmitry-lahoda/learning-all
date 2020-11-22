using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using MS.Internal.Interop;

namespace show_dialog
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
            var windowsUser = System.Security.Principal.WindowsIdentity.GetCurrent();
            var viewOfUser = ObjectDumper.ObjectDumperExtensions.DumpToString(windowsUser, nameof(windowsUser));
            log.Text = Environment.Version + Environment.NewLine + viewOfUser;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var saveDialog = new SaveFileDialog();
            saveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveDialog.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow();
            window.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var action = new ThreadStart(() =>
            {
                var window = new MainWindow();
                window.ShowDialog();
            });
            var t = new Thread(action);
            t.ApartmentState = ApartmentState.STA;
            t.Start();
        }

        [DllImport("Shell32.dll")]
        [SecurityCritical, SuppressUnmanagedCodeSecurity]
        internal static extern HRESULT SHCreateItemFromParsingName([MarshalAs(UnmanagedType.LPWStr)] string pszPath, IBindCtx pbc, [In] ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppv);


        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var path = @"c:\Users\dzmitry_lahoda\Desktop\";
            const string ShellItem2 = "7e9fb0d3-919f-4307-ab2e-9b1860310c93";
            Guid iidShellItem2 = new Guid(ShellItem2);
            object unk;
            HRESULT hr = SHCreateItemFromParsingName(path, null, ref iidShellItem2, out unk);
            hr.ThrowIfFailed();
        }
    }
}
