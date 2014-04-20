<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Article.aspx.cs" Inherits="ArticlePage" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" Runat="Server">

<!--tinyMCE-->
<script language="javascript" type="text/javascript" src="App_Scripts/tinymce/jscripts/tiny_mce/tiny_mce.js"></script>
<script language="javascript" type="text/javascript">
tinyMCE.init({
    mode : "textareas",
    theme : "simple"
});
</script>
<!--tinyMCE-->

       
   <asp:Label ID="LabelTitle" runat="server" Text="Title"></asp:Label>
    <asp:TextBox ID="TextBoxTitle" runat="server"></asp:TextBox><br />
               
                <asp:Label ID="LabelAthour" runat="server" Text="Athour"></asp:Label>

    <asp:TextBox ID="TextBoxAthour" runat="server" Enabled="false"></asp:TextBox><br />
   
                <asp:Label ID="LabelBody" runat="server" Text="Body" ></asp:Label>
    <asp:TextBox ID="TextBoxBody" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox><br />
    
                <asp:Label ID="LabelTags" runat="server" Text="Tags"></asp:Label>
    <asp:TextBox ID="TextBoxTags" runat="server"></asp:TextBox><br />
  
    <asp:LoginView ID="LoginView1" runat="server">
    <AnonymousTemplate>
    </AnonymousTemplate>
    <RoleGroups>
    <asp:RoleGroup Roles="Administrator">
    <ContentTemplate>
        <asp:Button id="ButtonDeleteArticle" runat="server" Text="Delete article" OnClick="DeleteArticle" />
    </ContentTemplate>
    </asp:RoleGroup>
    </RoleGroups>
    </asp:LoginView>
</asp:Content>

