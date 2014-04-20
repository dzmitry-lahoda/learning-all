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

public partial class ArticleListPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IList<Article> articles;
        String tagTitle=Request.QueryString["tag"];
        if (tagTitle != null)
        {
            articles = ArticleEditor.GetArticlesWithTag(tagTitle);
        }
        else
        {
            articles = ArticleEditor.GetAllArticles();
        }
        GridViewArticles.DataSource = articles;
        GridViewArticles.DataBind();
    }
}
