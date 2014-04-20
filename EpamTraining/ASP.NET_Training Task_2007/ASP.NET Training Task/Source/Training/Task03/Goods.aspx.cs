using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Web.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;

public partial class Task03_Goods : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {

    }


    private void Page_PreRender(object sender, System.EventArgs e)
    {

        String category = Request.QueryString["category"];
        if ((category == null) || (category == ""))
        {
            Response.Redirect("Category.aspx");
        }
        /*GoodsDB gdb = new GoodsDB();
        DataTable dt = gdb.GetAllNamesInCategory(category);
        dt.DefaultView.RowFilter = "";// "asd=''";
        GridViewGoods.DataSource=dt;
        GridViewGoods.DataBind();*/
    }

    protected void Buy_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < GridViewGoods.Rows.Count; i++)
        {
            Response.Write( GridViewGoods.Rows[i].Cells[0].Text+"<br/>") ;
            Response.Write(GridViewGoods.Rows[i].Cells[3].Text + "<br/>");
        }
    }
    protected void SqlDataSourceDefault_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }

    protected void CheckBoxBuy_CheckedChanged(object sender, EventArgs e)
    {
        Response.Write( e.GetType().ToString());
        Response.Write("ASD");
        Response.Write(SqlDataSourceDefault.FilterExpression);
    }
    protected void ButtonFiltr_Click(object sender, EventArgs e)
    {
        decimal from = 0;
        decimal to = decimal.MaxValue;
        bool filtr = (decimal.TryParse(TextBoxFrom.Text, out from) && decimal.TryParse(TextBoxTo.Text, out to) && from >= 0);
        if (filtr)
        {
            SqlDataSourceDefault.FilterExpression = String.Format("Price>={0} AND Price<={1}", from, to);
        }
    }



    protected void ButtonReset_Click(object sender, EventArgs e)
    {
        TextBoxFrom.Text = String.Empty;
        TextBoxTo.Text = String.Empty;
        SqlDataSourceDefault.FilterExpression = String.Format("Price>={0} AND Price<={1}", 0.0 ,decimal.MaxValue);
    }
}
