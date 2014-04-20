using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EPAM.Trainings.Zadanie2_2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Point[] points1 ={ new Point(20, 10), new Point(40, 10), new Point(35, 30), new Point(30, 30), new Point(30, 30) };
            Point[] points2 ={ new Point(20, 10), new Point(40, 10), new Point(35, 30), new Point(30, 30), new Point(30, 30) };
            List<Circle> list = Circle.GetCircles(points);
            foreach (Circle var in list)
            {
                Console.WriteLine(var.ToString());
            }

            Console.WriteLine();
            Circle c = new Circle(new Point(20, 10), new Point(40, 10), new Point(30, 30));
            Console.WriteLine(c.ToString());

            Console.WriteLine();
            Point a = new Point(1, 2);
            Point b = new Point(1, 2);
            Console.WriteLine(Point.Equals(a, b).ToString());
            Console.WriteLine(a.Equals(b).ToString());
            MessageBox.Show(c.Contains(new Point(20,25)).ToString());
        }
    }
}