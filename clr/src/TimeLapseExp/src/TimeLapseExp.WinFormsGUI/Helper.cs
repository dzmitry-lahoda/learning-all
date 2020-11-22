using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Visifire.Charts;

namespace TimeLapseExp.WinFormsGUI
{
    /// <summary>
    /// Put here different utilities untill moving them to separate class.
    /// </summary>
    internal static class Helper
    {
        private static Chart chart = new Chart();
        private static Title title = new Title();
        static DataSeries dataSeries = new DataSeries();
        static ChartGrid chartGrid = new ChartGrid();

        static Window window = new Window();
        static Grid grid = new Grid();

        private static Axis yAxis = new Axis();
        private static Axis xAxis = new Axis();

        internal static void Ahahah()
        {
            
        }

        /// <summary>
        /// Drows chart for analyze of best Shannon entropy.
        /// </summary>
        /// <param name="titleText"></param>
        /// <param name="picturesInfo"></param>
        /// <returns></returns>
        internal static Window ShowChart(String titleText,Dictionary<string, FormExp.FocusExposureContrastShannon> picturesInfo)
        {
            title.Dispatcher.Invoke(DispatcherPriority.Normal,
        (ThreadStart)delegate 
            {
    title.Text = titleText;

            
            
            dataSeries.RenderAs = RenderAs.Line;

            DataPoint dataPoint;
                var min = Double.MaxValue;
                var max = Double.MinValue;
            foreach (var info in picturesInfo)
            {
                dataPoint = new DataPoint();
                if (max<info.Value.Shannon)
                {
                    max = info.Value.Shannon;
                }
                if (min > info.Value.Shannon)
                {
                    min = info.Value.Shannon;
                }
                dataPoint.YValue = info.Value.Shannon;
                dataPoint.XValue = info.Value.Exposure;
                dataSeries.DataPoints.Add(dataPoint);
            }
            //TODO: Get appropriate minimum and maximum.
            yAxis.Title = "Shannon entropy";
            yAxis.Interval = 0.5;
                var dataMargin = (min + max)/4;
                yAxis.AxisMaximum = max + dataMargin;
                var axisMinimum = min - dataMargin;
                axisMinimum = axisMinimum > 0 ? axisMinimum : 0;
                yAxis.AxisMinimum = axisMinimum;
            
            xAxis.Title = "Exposure time, 10E-6 s";
            xAxis.Interval = 10000;

            chart.Titles.Add(title);
            chart.AxesX.Add(xAxis);
            chart.AxesY.Add(yAxis);

            chartGrid.Enabled = true;
            chartGrid.Chart = chart;
            chart.Series.Add(dataSeries);

            grid.Children.Add(chart);
            window.Content = grid;
            window.Show();
            }
            );
            return window;
        }

    }
}