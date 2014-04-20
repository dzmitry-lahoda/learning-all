<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Category.aspx.cs" Inherits="Task03_Category" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Интернет-магазин</title>
</head>
<body>
    <form id="Category" runat="server">
        <asp:GridView ID="GridViewDefault" runat="server" AutoGenerateColumns="false">
        <Columns>
        <asp:HyperLinkField DataNavigateUrlFormatString="goods.aspx?category={0}"  HeaderText="Выберите категорию" DataNavigateUrlFields="category" DataTextField="category" />
        </Columns>
            <RowStyle BorderStyle="Solid" />
        </asp:GridView>
    </form>
</body>
</html>
