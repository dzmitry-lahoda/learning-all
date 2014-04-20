<%@ Page Language="C#" AutoEventWireup="true" Trace="false" 
CodeFile="Goods.aspx.cs" Inherits="Task03_Goods" EnableViewStateMac="true"
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Интернет-магазин</title>
</head>
<body>
    <form id="Goods" runat="server">
    <div style="height: 116px; width: 100%">
        <asp:Label runat="server" ID="LabelPriceFilter" Text="Цена"></asp:Label>
        <asp:Label ID="Label0" runat="server" Text="от"></asp:Label>
        <asp:TextBox ID="TextBoxFrom" runat="server"></asp:TextBox>
        <asp:Label ID="Label1" runat="server" Text="до"></asp:Label>
        <asp:TextBox ID="TextBoxTo" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="ButtonFiltr" runat="server" Text="Фильтровать" 
            onclick="ButtonFiltr_Click" />
        <br />
        <asp:Button ID="ButtonReset" runat="server" Text="Сбросить фильтры" 
            onclick="ButtonReset_Click" />
    </div>
    <div>
        <asp:GridView ID="GridViewGoods" runat="server" BackColor="White" 
            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
            AllowSorting="True" DataSourceID="SqlDataSourceDefault" >
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <RowStyle ForeColor="#000066" />
            <Columns>
                <asp:TemplateField HeaderText="Buy">
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="CheckBoxBuy" Checked="False" OnCheckedChanged="CheckBoxBuy_CheckedChanged"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSourceDefault" runat="server" ConnectionString="<%$ ConnectionStrings:eshop %>"
            SelectCommand="GetAllNamesInCategory" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="" Name="Category" QueryStringField="category"
                    Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        &nbsp; &nbsp;&nbsp;
        <asp:Button ID="Buy" runat="server" Text="Купить" onclick="Buy_Click" 
            style="height: 26px" />&nbsp;&nbsp;</div>
    </form>
</body>
</html>
