using System;
using System.Collections.Generic;
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

using AIbyExample.Common;

namespace Chapter2Test
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
//            var agent = new CreatureAgent();
//            agent.ChangeState(new RunAwayCreatureState());
//            agent.Update();
//            Thread.Sleep(1000);
//            agent.IsSafe = true;
//            agent.Update();
//            agent.Update();

            var miner = new HumanoidAgent();

            for (var i = 0; i < 20; ++i)
            {
                miner.Update();

                Thread.Sleep(800);
            }
        }

    }
}
