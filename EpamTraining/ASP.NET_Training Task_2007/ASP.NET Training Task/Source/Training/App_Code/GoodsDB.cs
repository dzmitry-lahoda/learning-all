using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;


public class GoodDetails
{
    private String name;
    public String Name
    {
        get { return name; }
        set { name = value; }
    }
    private String category;
    public String Category
    {
        get { return category; }
        set { category = value; }
    }
    private int amount;
    public int Amount
    {
        get { return amount; }
        set { amount = value; }
    }


    public GoodDetails(String name, String comment, int amount)
    {
        this.name = name;
        this.category = category;
        this.amount = amount;
    }
}

/// <summary>
/// Summary description for GoodsDB
/// </summary>
public class GoodsDB
{

    private String connectionString;

    public GoodsDB()
    {
        // Get default connection string.
        connectionString = WebConfigurationManager.ConnectionStrings["eshop"].ConnectionString;
    }

    public DataTable GetAllNamesInCategory(String category)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("GetAllNamesInCategory", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@Category", SqlDbType.VarChar, 50));
        cmd.Parameters["@Category"].Value = category;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        //fill the DataSet.
        try
        {
            da.Fill(ds, "goods");
            return ds.Tables["goods"];
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
        }
    }

    public DataTable GetAllCategories()
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("GetAllCategories", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet(); ;
        //fill the DataSet.
        try
        {
            da.Fill(ds, "goods");
            return ds.Tables["goods"];
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
        }
    }
}

