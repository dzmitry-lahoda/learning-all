<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="LoginPage"%>
<asp:Content ID="LoginContent" ContentPlaceHolderID="ContentPlaceHolderMain" Runat="Server">
    <asp:Login ID="Login1" runat="server" PasswordRecoveryUrl="Pages/Anonymous/RecoverPassword.aspx">
    </asp:Login>
</asp:Content>

