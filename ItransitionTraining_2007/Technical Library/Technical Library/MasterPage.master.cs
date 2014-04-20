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

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    protected void AddArticle(object sender, EventArgs e)
    {
        Response.Redirect("~/User/ArticleAdding.aspx");
    }

    protected void ManageYourProfile(object sender, EventArgs e)
    {
        Response.Redirect("~/User/Profile.aspx");
    }

    protected void ManageYourArticles(object sender, EventArgs e)
    {
        Response.Redirect("~/User/Articles.aspx");
    }

    protected void Search(object sender, EventArgs e)
    {
        String url = String.Format("~/ArticleList.aspx?tag={0}", TextBoxSearch.Text);
        Response.Redirect(url);
    }

    protected void GetArticles(object sender, EventArgs e)
    {
        Response.Redirect("~/ArticleList.aspx");
    }
    protected void GetAllArticles(object sender, EventArgs e)
    {
        Response.Redirect("~/ArticleList.aspx");
    }


    protected void Register(object sender, EventArgs e)
    {
        Response.Redirect("~/Anonymous/Registration.aspx");
    }

    protected void RecoverPassword(object sender, EventArgs e)
    {
        Response.Redirect("~/Anonymous/PasswordRecovery.aspx");
    }
    
}