using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using NLocalIpc;
using System.Threading;

namespace App1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LocalInterProcessEvent<SEA> _interProcessEvent;
        private int _counter;

        public MainWindow()
        {
            InitializeComponent();

           
        }


        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            if (_interProcessEvent != null)
                _interProcessEvent.Dispose();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_interProcessEvent == null)
            {
                _interProcessEvent = new LocalInterProcessEvent<SEA>("UniqueTheEventName");
            }
            _interProcessEvent.Subsribe(Handle);
        }

        private void Handle(object sender, SEA e)
        {            
            var current = Interlocked.Add(ref _counter,1);
            //if (_counter % 10 == 0 &&  _counter !=0)
            //{
            //    _interProcessEvent.Raise(new SEA(Guid.NewGuid()));            
            //}
            Action Log = () => log.Text += current + " " + DateTime.Now + " " + e.id + Environment.NewLine;
            Dispatcher.Invoke(Log);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_interProcessEvent == null)
            {
                _interProcessEvent = new LocalInterProcessEvent<SEA>("UniqueTheEventName");
            }
            _interProcessEvent.Raise(new SEA(Guid.NewGuid()));
            _interProcessEvent.Raise(new SEA(Guid.NewGuid()));
            _interProcessEvent.Raise(new SEA(Guid.NewGuid()));
            _interProcessEvent.Raise(new SEA(Guid.NewGuid()));
            _interProcessEvent.Raise(new SEA(Guid.NewGuid()));
        }
    }

    [Serializable]
    public class SEA : EventArgs
    {
        public SEA()
        {
        }

        public SEA(Guid id)
        {
            this.id = id;
        }

        public Guid id { get; set; }
    }
}
