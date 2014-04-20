<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Task01.aspx.cs" Inherits="Task01_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Приложение-анкета</title>
</head>
<body>
    <form id="formA" runat="server" defaultfocus="Button1"  defaultbutton="Button1">
    <div>
        <p class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: justify">
            Необходимо создать страницу, содержащую метки, поля ввода и проверочные компоненты,
            представляющую анкету для ввода произвольных данных пользователя. Добавить код,
            сохраняющий введенные данные в файл</p>
        <p>
            <asp:Label ID="LoginLabel" runat="server" Text="Login:"></asp:Label>
            <asp:TextBox ID="LoginTextBox" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="LoginRequiredFieldValidator" runat="server" ControlToValidate="LoginTextBox"
                SetFocusOnError="True">*</asp:RequiredFieldValidator>
        </p>
        <p>
            &nbsp;</p>
        <p>
            <asp:Label ID="EmailLabel1" runat="server" Text="Email:"></asp:Label>
            <asp:TextBox ID="EmailTextBox1" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="EmailRequiredFieldValidator1" runat="server" ControlToValidate="EmailTextBox1"
                SetFocusOnError="True">*</asp:RequiredFieldValidator>&nbsp;
            <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server"
                ControlToValidate="EmailTextBox1" ErrorMessage="Wrong email." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></p>
        <p>
            <asp:Label ID="EmailLabel2" runat="server" Text="Repeated email:"></asp:Label>
            <asp:TextBox ID="EmailTextBox2" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="EmailRequiredFieldValidator2" runat="server" ControlToValidate="EmailTextBox2">*</asp:RequiredFieldValidator>
            <asp:CompareValidator ID="EmailCompareValidator" runat="server" ControlToCompare="EmailTextBox1"
                ControlToValidate="EmailTextBox2" ErrorMessage="Different emails."></asp:CompareValidator></p>
        <p>
            &nbsp;</p>
        <p>
            <asp:Label ID="PasswordLabel1" runat="server" Text="Password:"></asp:Label>
            <asp:TextBox ID="PasswordTextBox1" runat="server" EnableViewState="False"></asp:TextBox>
            <asp:RequiredFieldValidator ID="PasswordRequiredFieldValidator1" runat="server" ControlToValidate="PasswordTextBox1"
                SetFocusOnError="True">*</asp:RequiredFieldValidator>&nbsp;
        </p>
        <p>
            <asp:Label ID="PasswordLabel2" runat="server" Text="Repeat password:"></asp:Label>
            <asp:TextBox ID="PasswordTextBox2" runat="server" EnableViewState="False"></asp:TextBox>
            <asp:RequiredFieldValidator ID="PasswordRequiredFieldValidator2" runat="server" ControlToValidate="PasswordTextBox2"
                SetFocusOnError="True">*</asp:RequiredFieldValidator>
            <asp:CompareValidator ID="PasswordCompareValidator" runat="server" ControlToCompare="PasswordTextBox1"
                ControlToValidate="PasswordTextBox2" ErrorMessage="Different passwords."></asp:CompareValidator></p>
        <p>
            <asp:Label ID="TextLabel" runat="server" Text="Text:"></asp:Label><asp:TextBox ID="TextTextBox"
                runat="server" Height="116px" Width="372px"></asp:TextBox>&nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
        <asp:Button ID="ButtonSaveToFile" runat="server" Text="Save to file" OnClick="ButtonSaveToFile_Click" />&nbsp;</p>
    </div>
    </form>
</body>
</html>
