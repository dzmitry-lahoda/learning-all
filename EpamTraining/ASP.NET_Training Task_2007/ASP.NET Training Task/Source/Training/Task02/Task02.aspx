<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Task02.aspx.cs" Inherits="Task02_Task02" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Страница с комментариями</title>
</head>
<body>
    <form id="Review" runat="server">
    <div>
        <asp:Panel ID="ReviewPanel" runat="server" Height="247px" Width="100%" BorderColor="Red" BorderWidth="2px">
            &nbsp;<asp:Image ID="Poster" runat="server" style="vertical-align: middle; text-align: center" AlternateText="Poster" GenerateEmptyAlternateText="True" Height="75px" ImageAlign="Middle" Width="84px" />
            <asp:Label ID="FilmLabel" runat="server" Text="Film" style="vertical-align: middle; text-align: center;"></asp:Label><br />
            &nbsp;<asp:Label ID="ReviewLabel" runat="server" Text="На странице помещен некий материал (например, рецензия на фильм), к которому пользователи могут оставлять комментарии. Все оставленные комментарии отображаются. Затем идет форма для комментария, включающая поля – имя, текст комментария, оценка материала. После заполнения полей комментарий записывается в БД, страница пересылается пользователю. Страница отображает среднюю оценку материала." Height="125px" Width="80%"></asp:Label><br />
            <br />
            &nbsp;<br />
            <asp:Label ID="ReviewMarkLabel" runat="server" Text="Оценка материала:"></asp:Label>
            <asp:Label ID="AverageMark" runat="server" Text="-"></asp:Label></asp:Panel>
        &nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;
        <br />
        <asp:GridView ID="GridViewComments" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" ForeColor="#333333" GridLines="None">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BorderStyle="None" />
            <Columns>
                <asp:TemplateField HeaderText="Comments">
                    <ItemTemplate>
                        <b>
                            <%# Eval("Name") %>
                        </b>
                        <hr />
                        <%# Eval("Comment") %>
                        <hr />
                        <br />    
                     </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <br />
    </div>
        <asp:Panel ID="AddCommentPanel" runat="server" Height="50px" Width="100%" BorderColor="Red" BorderWidth="2px">
            <br />
            <asp:Label ID="AddCommentLabel" runat="server" Text="Ваш комментарий" Font-Bold="True" ForeColor="Red"></asp:Label><br />
            <br />
            <asp:Label ID="NameLabel" runat="server" Text="Имя:" Width="21%" Height="22px"></asp:Label>
            <asp:TextBox ID="NameTextBox" runat="server" Height="22px" Width="262px" MaxLength="32" EnableViewState="False"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator
                ID="NameRequiredFieldValidator" runat="server" ControlToValidate="NameTextBox"
                ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator><br />
            <br />
            <asp:Label ID="CommentLabel" runat="server" Text="Текст комментария:" Width="21%"></asp:Label>&nbsp;
            <asp:TextBox ID="CommentTextBox" runat="server" Height="94px" TextMode="MultiLine" Width="434px" MaxLength="400" EnableViewState="False"></asp:TextBox>
            <asp:RequiredFieldValidator ID="CommentRequiredFieldValidator" runat="server" ControlToValidate="CommentTextBox"
                ErrorMessage="*" SetFocusOnError="True" Height="9px" Width="4px"></asp:RequiredFieldValidator><br />
            <br />
            <asp:Label ID="MarkLabel" runat="server" Text="Оценка" Width="21%"></asp:Label>
            <asp:DropDownList ID="MarkDropDownList" runat="server" Height="22px" Width="105px">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem Selected="True" Value="5"></asp:ListItem>
                <asp:ListItem Value="6"></asp:ListItem>
                <asp:ListItem Value="7"></asp:ListItem>
                <asp:ListItem Value="8"></asp:ListItem>
                <asp:ListItem Value="9"></asp:ListItem>
                <asp:ListItem Value="10"></asp:ListItem>
            </asp:DropDownList><br />
            <br />
            <asp:Button ID="AddCommentButton" runat="server" OnClick="AddButton_Click" Text="Добавить комментарий" /><br />
            <br />
            </asp:Panel>
    </form>
</body>
</html>
