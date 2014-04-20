using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EPAM.Trainings.Zadanie2_2;

using Point = EPAM.Trainings.Zadanie2_2.Point;

namespace EPAM.Trainings.Zadanie2_4
{
    public partial class MainForm : Form
    {
        public PointWorker pw = new PointWorker();
        public MainForm()
        {
            pw.PointsChanged += new PointWorkerEventHandler(OnPointsChanged);
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle pointRectangle;
            foreach (Point var in pw.points)
            {
                pointRectangle = new Rectangle(var.X, var.Y, 2, 2);
                g.DrawEllipse(Pens.Black, pointRectangle);
            }

            foreach (Point var in pw.circlepoints)
            {
                pointRectangle = new Rectangle(var.X, var.Y, 2, 2);
                g.DrawEllipse(Pens.Red, pointRectangle);
            }
            Rectangle circleRectangle = new Rectangle(pw.circle.Centre.X - pw.circle.Radius,
                pw.circle.Centre.Y - pw.circle.Radius,
                2 * pw.circle.Radius, 2 * pw.circle.Radius);
            g.DrawEllipse(Pens.White, circleRectangle);
        }

        private void MainForm_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {

            Point point = new Point(e.X, e.Y);
            if (e.Button == MouseButtons.Left)
            {
                 pw.SetPoints(point,Pointer.CirclePoint);
            }
            else if (e.Button == MouseButtons.Right)
            {
                pw.SetPoints(point,Pointer.Point);
            }

        }

        public void OnPointsChanged(Object sender,PointWorkerEventArgs e)
        {
            if (e.pointer == Pointer.CirclePoint)
            {
                pw.AddCirclePoints(e.point);
            }
            else if (e.pointer ==  Pointer.Point)
            {
                pw.AddPoints(e.point);
            }
            List<Circle> list1 = pw.GetCircles();
            List<Circle> list2 = new List<Circle>();
            foreach (Circle var in list1)
            {
                if (var.Contains(pw.points.ToArray()))
                {
                    list2.Add(var);
                }
            }
            pw.circle = new Circle(Point.Zero, 10000);
            foreach (Circle var in list2)
            {
                if (var.Area < pw.circle.Area)
                {
                    pw.circle = var;
                }
            }
            Invalidate();
        }
    }
}