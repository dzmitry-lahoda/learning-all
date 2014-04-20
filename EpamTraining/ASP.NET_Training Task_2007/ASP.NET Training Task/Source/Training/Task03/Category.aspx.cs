using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;

public partial class Task03_Category : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private void Page_PreRender(object sender, System.EventArgs e)
    {
        GoodsDB gdb = new GoodsDB();
        DataTable dt = gdb.GetAllCategories();
        GridViewDefault.DataSource = dt;
        GridViewDefault.DataBind();
    }

}
