using System;
using System.Collections.Generic;
using System.IO;
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


using dnAnalytics.LinearAlgebra;
using dnAnalytics.LinearAlgebra.IO;

using HTMImageLayer;
using Matrix = dnAnalytics.LinearAlgebra.Matrix;


namespace TestApplication
{

    public class ASDASDASD : IComparer<double>
    {

        private double _Epsilon = 1E-5;

        public int Compare(double x, double y)
        {
            var nearEqual = Math.Abs(x - y) < _Epsilon;
            if (nearEqual)
            {
                return 0;
            }
            if (x < y)
                return 1;

            return -1;
        }

    }

    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void image1_MouseDown(object sender, MouseButtonEventArgs e)
        {

            //var frame = decoder.Preview;
            //var pixels = Array.CreateInstance(typeof(double), 1);
            //frame.CopyPixels(pixels, 1, 1);
        }



        private void image1_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void image1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var toCompare = 2;
            var list = new List<double>(new double[] { 1, 2, 3, 7, 5, -1 });
            var index = list.FindIndex(x => Math.Abs(x - toCompare) < 1E-5);

            var imageMatrix =
                ImageLayer.LoadImageMatrix(
                    @"D:\WORK\HTM\Neocortex_1.4.2c\Neocortex\Training Objects\Some\Helicopter\helicopter.bmp");
            var frames = ImageLayer.FrameImageMatrix(imageMatrix, 4);


            var level3 = new Level(3);
            var node1 = new Node(1);
            level3.AddNodes(node1);

            var level2 = new Level(2);
            for (var i = 2; i < 18; i++)
            {
                var newNode = new Node(i);
                node1.AddChildren(newNode);
                level2.AddNodes(newNode);
            }

            var level1 = new Level(1);

            foreach (var node in level2.Nodes)
            {
                for (var i = 0; i < 4; i++)
                {
                    var newNode = new Node(i);
                    node.AddChildren(newNode);
                    level1.Nodes.Add(newNode);
                }
            }

            for (var i = 0; i < frames.Count; i++)
            {
                level1.Nodes[i].Memory.AddPattern(frames[i]);
            }

            for (var i = 0; i < frames.Count; i++)
            {
                var asd = frames[i].Clone();
                asd.Multiply(23);
                level1.Nodes[i].Memory.AddPattern(asd);
            }

            var ggg = 1;
            //            var imageName = @"D:\WORK\HTM\Neocortex_1.4.2c\Neocortex\Test Objects\some\Cell phone\cell_phone.bmp";
            //            var imageStream = new FileStream(imageName, FileMode.Open, FileAccess.Read, FileShare.Read);
            //            var decoder = BitmapDecoder.Create(imageStream, BitmapCreateOptions.None, BitmapCacheOption.Default);
            //            image1.Source = decoder.Frames[0];
            //            var frame = decoder.Frames[0];
            //            var h = frame.PixelHeight;
            //            var w = frame.PixelWidth;
            //            double[] imageArray = new double[h*w];
            //            frame.CopyPixels(imageArray, 64, 0);
            //            var imageVector = new DenseVector(imageArray);
            //            var imageMatrix = imageVector.CreateMatrix(w, h);
            //            for (var j = 0; j < h; j++)
            //            {
            //                for (var k = 0; k < w; k++)
            //                {
            //
            //                    imageMatrix[j, k] = imageArray[j * h + k];
            //
            //                }
            //                
            //            }
            //            var asd = new DelimitedMatrixWriter();
            //            asd.WriteMatrix(imageMatrix,"asdasdsa.txt");
            //            
            //            //imageMatrix = imageMatrix.Shift(7);
            //            var changedImage = BitmapSource.Create(w, h, 1, 1, frame.Format, frame.Palette, imageMatrix.ToRowWiseArray(), 64);
            //
            //            //var changedImage = BitmapSource.Create(w, h, frame.DpiX, frame.DpiY, frame.Format, frame.Palette, imageMatrix.ToRowWiseArray(), 64);
            //            image1.Source = changedImage;
        }

    }
}