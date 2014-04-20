<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Register.aspx.cs" Inherits="RegisterPage" %>

<asp:Content ID="RegistrationContent" ContentPlaceHolderID="ContentPlaceHolderMain"
    runat="Server">
    <asp:Label ID="LabelRegistration" runat="server" Text="Registration"></asp:Label>
    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" OnCreatedUser="CreateUserWizard1_CreatedUser"
        OnCreatingUser="CreateUserWizard1_CreatingUser" OnFinishButtonClick="CreateUserWizard1_FinishButtonClick">
        <WizardSteps>
            <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
            </asp:CreateUserWizardStep>
            <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
            </asp:CompleteWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
</asp:Content>
