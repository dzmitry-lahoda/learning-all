//KMeans.cs

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace codeding.KMeans.DataStructures
{
    public class KMeans
    {
        public static List<PointCollection> DoKMeans(PointCollection points, int clusterCount)
        {
            //divide points into equal clusters
            List<PointCollection> allClusters = new List<PointCollection>();
            List<List<Point>> allGroups = ListUtility.SplitList<Point>(points, clusterCount);
            foreach (List<Point> group in allGroups)
            {
                PointCollection cluster = new PointCollection();
                cluster.AddRange(group);
                allClusters.Add(cluster);
            }

            //start k-means clustering
            int movements = 1;
            while (movements > 0)
            {
                movements = 0;

                foreach (PointCollection cluster in allClusters) //for all clusters
                {
                    for (int pointIndex = 0; pointIndex < cluster.Count; pointIndex++) //for all points in each cluster
                    {
                        Point point = cluster[pointIndex];

                        int nearestCluster = FindNearestCluster(allClusters, point);
                        if (nearestCluster != allClusters.IndexOf(cluster)) //if point has moved
                        {
                            if (cluster.Count > 1) //cluster shall have minimum one point
                            {
                                Point removedPoint = cluster.RemovePoint(point);
                                allClusters[nearestCluster].AddPoint(removedPoint);
                                movements += 1;
                            }
                        }
                    }
                }
            }

            return (allClusters);
        }

        public static int FindNearestCluster(List<PointCollection> allClusters, Point point)
        {
            double minimumDistance = 0.0;
            int nearestClusterIndex = -1;

            for (int k = 0; k < allClusters.Count; k++) //find nearest cluster
            {
                double distance = Point.FindDistance(point, allClusters[k].Centroid);
                if (k == 0)
                {
                    minimumDistance = distance;
                    nearestClusterIndex = 0;
                }
                else if (minimumDistance > distance)
                {
                    minimumDistance = distance;
                    nearestClusterIndex = k;
                }
            }

            return (nearestClusterIndex);
        }
    }
}
