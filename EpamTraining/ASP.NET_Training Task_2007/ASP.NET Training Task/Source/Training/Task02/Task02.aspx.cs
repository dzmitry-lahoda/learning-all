using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Text;

public partial class Task02_Task02 : System.Web.UI.Page
{
    private CommentsDB db = new CommentsDB();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void AddButton_Click(object sender, EventArgs e)
    {
        db.InsertComment(new CommentDetails(NameTextBox.Text, CommentTextBox.Text, Int16.Parse(MarkDropDownList.Text)));
        //db.DeletAllComments();
    }

    private void Page_PreRender(object sender, System.EventArgs e)
    {
        List<CommentDetails> comments = db.SelectAllComments();
        GridViewComments.DataSource = comments;
        GridViewComments.DataBind();
        AverageMark.Text = db.GetAverageMark().ToString();
    }

}
