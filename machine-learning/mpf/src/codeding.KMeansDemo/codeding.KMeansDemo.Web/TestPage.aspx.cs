//TestPage.aspx.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using codeding.KMeans.DataStructures;

namespace codeding.KMeansDemo.Web
{
    public partial class TestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PointCollection points = new PointCollection();
            points.Add(new Point(0, 1, 1));
            points.Add(new Point(1, 5, 4));
            points.Add(new Point(2, 2, 1));
            points.Add(new Point(3, 4, 3));
            points.Add(new Point(4, 3, 2));

            List<PointCollection> allClusters = KMeans.DataStructures.KMeans.DoKMeans(points, 2);
            Response.Write(allClusters.Count + " clusters<br/>");

            int clusterIndex = 0;
            foreach (PointCollection cluster in allClusters)
            {
                Response.Write(cluster.Count + " points in cluster" + clusterIndex + "<br/>");
                foreach (Point p in cluster)
                {
                    Response.Write("&nbsp;&nbsp;&nbsp;&nbsp;" + p.ToString() + "<br/>");
                }

                clusterIndex += 1;
            }
        }
    }
}
