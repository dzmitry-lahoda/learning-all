//MainPage.xaml.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Data;
using System.Windows.Controls.DataVisualization;
using System.Windows.Controls.DataVisualization.Charting;

using codeding.KMeans.DataStructures;

namespace codeding.KMeansDemo
{
    public partial class KMeansDemoPage : UserControl
    {
        #region Properties

        public PointCollection AllPoints { get; set; }

        #endregion

        #region Constructors

        public KMeansDemoPage()
        {
            InitializeComponent();
            this.DataContext = this;

            ResetData();
            this.ClusterCountNumericUpDown.ValueChanged += ClusterCountNumericUpDown_ValueChanged;
        }

        #endregion

        #region Internal-Methods

        private void ResetData()
        {
            XTextBox.Text = "1";
            YTextBox.Text = "1";

            this.AllPoints = new PointCollection();
            this.AllPoints.Add(new KMeans.DataStructures.Point(0, 1, 1.5));
            this.AllPoints.Add(new KMeans.DataStructures.Point(1, 5, 4));
            this.AllPoints.Add(new KMeans.DataStructures.Point(2, 2, 9));
            this.AllPoints.Add(new KMeans.DataStructures.Point(3, 4, 3));
            this.AllPoints.Add(new KMeans.DataStructures.Point(4, 4, 4));
            this.AllPoints.Add(new KMeans.DataStructures.Point(5, 6, 1.8));
            this.AllPoints.Add(new KMeans.DataStructures.Point(6, 7, 13));
            ResetPointsBinding();
            DoKMeans();
        }

        private void DoKMeans()
        {
            //do kmeans clustering
            List<PointCollection> allClusters = KMeans.DataStructures.KMeans.DoKMeans(this.AllPoints, (int)ClusterCountNumericUpDown.Value);

            //render kmeans-graph
            PointsChart.Series.Clear();
            PointsChart.LegendTitle = allClusters.Count + " Clusters";
            foreach (PointCollection cluster in allClusters)
            {
                Dictionary<double, double> chartSeriesData = new Dictionary<double, double>();
                foreach (KMeans.DataStructures.Point point in cluster)
                {
                    while (chartSeriesData.ContainsKey(point.X)) point.X += 0.01; //avoid duplicate points in same cluster
                    chartSeriesData.Add(point.X, point.Y);
                }

                PointsChart.Series.Add(CreateScatterSeries("Cluster" + (allClusters.IndexOf(cluster) + 1), chartSeriesData));
            }

            //render clusters-tree
            ClustersTree.Items.Clear();
            foreach (PointCollection cluster in allClusters)
            {
                TreeViewItem clusterNode = new TreeViewItem();
                clusterNode.Header = "Cluster" + (allClusters.IndexOf(cluster) + 1);

                foreach (KMeans.DataStructures.Point point in cluster)
                {
                    TreeViewItem pointNode = new TreeViewItem();
                    pointNode.Header = point;
                    clusterNode.Items.Add(pointNode);
                }

                ClustersTree.Items.Add(clusterNode);
            }
        }

        private void ResetPointsBinding()
        {
            PointsListBox.ClearValue(ItemsControl.ItemsSourceProperty);
            PointsListBox.SetBinding(ItemsControl.ItemsSourceProperty, new Binding("AllPoints"));
        }

        private ScatterSeries CreateScatterSeries(string title, Dictionary<double, double> data)
        {
            ScatterSeries scatterSeries = new ScatterSeries();
            scatterSeries.Title = title;
            scatterSeries.ItemsSource = data;
            scatterSeries.IndependentValueBinding = new Binding("Key");
            scatterSeries.DependentValueBinding = new Binding("Value");
            return (scatterSeries);
        }

        #endregion

        #region Control-Events

        private void AddPointButton_Click(object sender, RoutedEventArgs e)
        {
            double newX, newY;
            if (double.TryParse(XTextBox.Text, out newX) && double.TryParse(YTextBox.Text, out newY))
            {
                codeding.KMeans.DataStructures.Point newPoint = new codeding.KMeans.DataStructures.Point(this.AllPoints.Count, newX, newY);
                if (this.AllPoints.Contains(newPoint))
                {
                    MessageBox.Show("Point " + newPoint + " is already added.");
                    return;
                }

                this.AllPoints.Add(newPoint);
                ResetPointsBinding();
                DoKMeans();
            }
            else
            {
                MessageBox.Show("Please enter valid X and Y for new point.");
            }
        }

        private void RemovePointHyperlink_Click(object sender, RoutedEventArgs e)
        {
            //check cluster-count
            if (this.AllPoints.Count == ClusterCountNumericUpDown.Value)
            {
                MessageBox.Show("There should be minimum " + this.AllPoints.Count + " points to build " + this.AllPoints.Count + " clusters.");
                return;
            }

            //remove current-point
            HyperlinkButton senderHyperlink = (HyperlinkButton)sender;
            KMeans.DataStructures.Point currentPoint = (KMeans.DataStructures.Point)senderHyperlink.DataContext;
            this.AllPoints.RemovePoint(currentPoint);
            ResetPointsBinding();
            DoKMeans();
        }

        private void ClusterCountNumericUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.AllPoints.Count < ClusterCountNumericUpDown.Value)
            {
                MessageBox.Show(ClusterCountNumericUpDown.Value + " clusters cannot be formed using " + this.AllPoints.Count + " points");
                ClusterCountNumericUpDown.Value = this.AllPoints.Count;
                return;
            }

            DoKMeans();
        }

        #endregion
    }
}
