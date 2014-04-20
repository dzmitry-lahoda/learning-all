<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ManageUsers.aspx.cs" Inherits="ManageUsersPage" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView ID="GridViewUsers" runat="server" AutoGenerateColumns="False" DataKeyNames="UserName"
                OnSelectedIndexChanged="GridViewUsers_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="UserName" HeaderText="Username" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="CreationDate" HeaderText="Creation Date" />
                    <asp:CommandField ShowSelectButton="True" />
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanelUser" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelUser" runat="server" Visible="False">
                Selected User:<br />
                <table border="1">
                    <tr>
                        <td>
                            User Name:
                        </td>
                        <td>
                            <asp:Label ID="UsernameLabel" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Email:
                        </td>
                        <td>
                            <asp:TextBox ID="EmailText" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Password Question:
                        </td>
                        <td>
                            <asp:Label ID="PwdQuestionLabel" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Last Login Date:
                        </td>
                        <td>
                            <asp:Label ID="LastLoginLabel" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Comment:
                        </td>
                        <td>
                            <asp:TextBox ID="CommentTextBox" runat="server" TextMode="multiline" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="IsApprovedCheck" runat="server" Text="Approved" />
                        </td>
                        <td>
                            <asp:CheckBox ID="IsLockedOutCheck" runat="Server" Text="Locked Out" />
                        </td>
                    </tr>
                </table>
                <!--<asp:DetailsView ID="DetailsViewUser" runat="server" Height="50px" Width="125px"
                AutoGenerateEditButton="True" AutoGenerateRows="False" DefaultMode="Edit">
                <fields>
                        <asp:BoundField DataField="UserName" HeaderText="UserName" ReadOnly="True" />
                        <asp:BoundField DataField="Email" HeaderText="Email" ReadOnly="True" />
                        <asp:BoundField DataField="Comment" HeaderText="Comment" />
                        <asp:CheckBoxField DataField="IsApproved" HeaderText="IsApproved" />
                        <asp:CheckBoxField DataField="IsLockedOut" HeaderText="IsLockedOut" />
                    </fields>
                </asp:DetailsView>-->
                <asp:Label ID="LabelRoles" runat="server" Text="Is in Roles:"></asp:Label>
                <asp:CheckBoxList ID="CheckBoxListRoles" runat="server">
                </asp:CheckBoxList>
                <br />
                <asp:Button ID="ButtonUpdateUser" runat="server" OnClick="ButtonUpdateUser_Click"
                    Text="Update user" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
