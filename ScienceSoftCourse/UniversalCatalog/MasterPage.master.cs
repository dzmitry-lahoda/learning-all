using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LinkButtonRegister_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Pages/Anonymous/Register.aspx");
    }
    protected void LinkButtonRecoverPassword_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Pages/Anonymous/RecoverPassword.aspx");
    }
    protected void LinkButtonManageTypes_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Pages/Administrator/ManageTypes.aspx");
    }

    protected void LinkButtonManageUsers_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Pages/Administrator/ManageUsers.aspx");
    }
    protected void LinkButtonManageItems_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Pages/Editor/ManageItems.aspx");
    }
    protected void LinkButtonManageProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Pages/User/ManageProfile.aspx");
    }
    protected void LinkButtonView_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/View.aspx");
    }

    protected void ItemTypeCreated(object sender, EventArgs e)
    {
        Response.Write("XZCXZCZXCZXC");
    }
}
