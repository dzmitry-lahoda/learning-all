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

public partial class ManageUsersPage : System.Web.UI.Page
{

    MembershipUserCollection users;

    protected void Page_Load(object sender, EventArgs e)
    {
        users = Membership.GetAllUsers();
        GridViewUsers.DataSource = users;
        if (!this.IsPostBack)
        {
            GridViewUsers.DataBind();
        }
    }
    protected void GridViewUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        {
            if (GridViewUsers.SelectedIndex >= 0)
            {
                PanelUser.Visible = true;
                var user = users[(string)GridViewUsers.SelectedValue];
                
                var roles = Roles.GetAllRoles();
                CheckBoxListRoles.DataSource = roles;
                CheckBoxListRoles.DataBind();
                foreach (ListItem item in CheckBoxListRoles.Items)
                {
                    item.Selected = Roles.IsUserInRole(user.UserName, item.Text);
                }

                UsernameLabel.Text = user.UserName;
                PwdQuestionLabel.Text = user.PasswordQuestion;
                LastLoginLabel.Text = user.LastLoginDate.ToShortDateString();
                EmailText.Text = user.Email;
                CommentTextBox.Text = user.Comment;
                IsApprovedCheck.Checked = user.IsApproved;
                IsLockedOutCheck.Checked = user.IsLockedOut;
            }
        }
    }
    protected void ButtonUpdateUser_Click(object sender, EventArgs e)
    {
        if (GridViewUsers.SelectedIndex < 0) return;
        
        var current = users[(string)GridViewUsers.SelectedValue];
        current.Email = EmailText.Text;
        current.Comment = CommentTextBox.Text;
        current.IsApproved = IsApprovedCheck.Checked;
        Membership.UpdateUser(current);

        ChangeUserRoles(current.UserName);

        CheckBoxListRoles.DataBind();
        GridViewUsers.DataBind();
    }

    private void ChangeUserRoles(String userName)
    {
        foreach (ListItem item in CheckBoxListRoles.Items)
        {
            if (item.Selected)
            {
                if (!Roles.IsUserInRole(userName, item.Text))
                {
                    Roles.AddUserToRole(userName, item.Text);
                }
            }
            else
            {
                if (Roles.IsUserInRole(userName, item.Text))
                {
                    Roles.RemoveUserFromRole(userName, item.Text);
                }
            }
        }
    }
}
