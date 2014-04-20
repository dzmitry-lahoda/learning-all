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
using Itransition.Training.Data;

public partial class ArticleAddingPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TextBoxAthour.Text = Context.User.Identity.Name;
    }
    protected void AddArticle(object sender, EventArgs e)
    {
        List<Tag> tags=new List<Tag>();
        tags.Add(new Tag(TextBoxTags.Text));
        Article article = new Article(TextBoxAthour.Text, TextBoxTitle.Text,TextBoxBody.Text, tags, DateTime.Now);
        if (ArticleEditor.AddArticle(article))
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}
