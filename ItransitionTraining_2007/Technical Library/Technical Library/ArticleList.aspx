<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ArticleList.aspx.cs" Inherits="ArticleListPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" Runat="Server">
    <asp:GridView ID="GridViewArticles" runat="server" AutoGenerateColumns="False">
        <columns>
            <asp:HyperLinkField DataTextField="Title" HeaderText="Title" 
            DataNavigateUrlFormatString="~/Article.aspx?title={0}&athour={1}"
             DataNavigateUrlFields="title,athour"
            >
            </asp:HyperLinkField>
            
            <asp:HyperLinkField DataTextField="Athour" HeaderText="Athour" >
            </asp:HyperLinkField>
        </columns>
        <RowStyle BorderColor="DarkRed" BorderWidth="2px" />
    </asp:GridView>
</asp:Content>

