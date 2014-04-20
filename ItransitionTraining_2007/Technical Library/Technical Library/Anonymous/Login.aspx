<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="LoginPage"%>
<asp:Content ID="LoginContent" ContentPlaceHolderID="ContentPlaceHolderMain" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" Height="50px" Width="125px"  DefaultButton=<%# Login1.FindControl("LoginButton").ToString() %>>
     <asp:Login ID="Login1" Runat="server" ></asp:Login>
    </asp:Panel>
</asp:Content>

