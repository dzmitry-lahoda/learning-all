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
using System.Collections.Generic;
using Itransition.Training.Data;

public partial class ArticlePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String articleTitle = Request.QueryString["title"];
        String articleAthour = Request.QueryString["athour"];
        Article article=null;
        if (articleTitle == null || articleAthour == null)
        {
             Response.Redirect("~/Default.aspx");
        }
        else
        {
            article = ArticleEditor.GetArticleWithAthourAndTitle(articleAthour,articleTitle);
        }
        if (article == null)
        {
            Response.Redirect("~/Default.aspx");
        }
        else
        {
            TextBoxAthour.Text = article.Athour;
            TextBoxTitle.Text = article.Title;
            TextBoxBody.Text = article.Body;
            foreach (Tag tag in article.Tags)
            {
                TextBoxTags.Text += tag.Title;
            }

        }

    }

    protected void DeleteArticle(object sender, EventArgs e)
    {
        ArticleEditor.DeleteArticleByAthourAndTitle(TextBoxAthour.Text,TextBoxTitle.Text);
        Response.Redirect("~/Default.aspx");
    }
}
