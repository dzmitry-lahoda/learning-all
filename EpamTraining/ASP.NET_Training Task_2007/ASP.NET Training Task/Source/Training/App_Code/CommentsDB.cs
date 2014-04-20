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


public class CommentDetails
{
    private String name;
    public String Name
    {
        get { return name; }
        set { name = value; }
    }
    private String comment;
    public String Comment
    {
        get { return comment; }
        set { comment = value; }
    }
    private Double mark;
    public Double Mark
    {
        get { return mark; }
        set { mark = value; }
    }
    public CommentDetails(String name, String comment, Double mark)
    {
        this.name = name;
        this.comment = comment;
        this.mark = mark;
    }
}

/// <summary>
/// Summary description for CommentsDB
/// </summary>
public class CommentsDB
{
 
    private String connectionString;

    public CommentsDB()
    {
        // Get default connection string.
        connectionString = WebConfigurationManager.ConnectionStrings["reviews"].ConnectionString;
    }
    public void InsertComment(CommentDetails comm)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("InsertComment", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.Char, 32));
        cmd.Parameters["@Name"].Value = comm.Name;
        cmd.Parameters.Add(new SqlParameter("@Comment", SqlDbType.VarChar, 1000));
        cmd.Parameters["@Comment"].Value = comm.Comment;
        cmd.Parameters.Add(new SqlParameter("@Mark", SqlDbType.SmallInt));
        cmd.Parameters["@Mark"].Value = comm.Mark;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            //exception can be logged
            throw new ApplicationException("Data error.");
        }
        finally
        {
            con.Close();
        }
    }

    public void DeletAllComments()
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("DeleteAllComments", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            //exception can be logged
            throw new ApplicationException("Data error.");
        }
        finally
        {
            con.Close();
        }
    }
    public List<CommentDetails> SelectAllComments()
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("SelectAllComments", con);
        cmd.CommandType = CommandType.StoredProcedure;
        // Create a collection for all the employee records.
        List<CommentDetails> comments = new List<CommentDetails>();
        try
        {
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CommentDetails comm = new CommentDetails(
                (String)reader["Name"], (String)reader["Comment"],
                (Double)reader["Mark"]);
                comments.Add(comm);
            }
            reader.Close();
            return comments;
        }
        catch (SqlException ex)
        {
            throw new ApplicationException("Data error.");
        }
        finally
        {
            con.Close();
        }
    }
    public String GetAverageMark()
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("GetAverageMark", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            con.Open();
            String averageMark = cmd.ExecuteScalar().ToString();
            if (averageMark.Length >= 4)
            {
                averageMark=averageMark.Substring(0, 4);
            }
            return averageMark;
        }
        catch (SqlException ex)
        {
            throw new ApplicationException("Data error.");
        }
        finally
        {
            con.Close();
        }
    }
}
