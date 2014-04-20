<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ManageItems.aspx.cs" Inherits="ManageItemsPage" ValidateRequest="true" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanelItemTypes" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridViewItemTypes" runat="server" DataSourceID="ObjectDataSourceItemTypes"
                            OnSelectedIndexChanged="GridViewItemTypes_SelectedIndexChanged" AutoGenerateColumns="False"
                            AutoGenerateSelectButton="True" Caption="Item types:" DataKeyNames="Id">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True"
                                    SortExpression="Id" Visible="False" />
                                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
                <hr />
                <asp:UpdatePanel ID="UpdatePanelEdit" runat="server">
                    <ContentTemplate>
                        <asp:Panel runat="server" ID="PanelEdit" Visible="false">
                            <asp:Label ID="LabelName" runat="server" Text="Name of item:"></asp:Label>
                            <asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>
                            <br />
                            <asp:PlaceHolder ID="PlaceHolderFields" runat="server"></asp:PlaceHolder>
                            <br />
                            <asp:Button ID="ButtonAdd" runat="server" Text="Add new item" 
                                OnClick="ButtonAdd_Click" />
                        </asp:Panel>
                   </ContentTemplate>
                   <Triggers>
                   <asp:PostBackTrigger  ControlID="ButtonAdd" />
                   </Triggers>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:PlaceHolder ID="PlaceHolderItems" runat="server"></asp:PlaceHolder>
                <br />
            </td>
        </tr>
    </table>
    <!--Data sources-->
    <asp:ObjectDataSource ID="ObjectDataSourceItemTypes" runat="server" TypeName="ItemTypeDAO"
        DataObjectTypeName="ItemType" SelectMethod="GetAllItemTypes"></asp:ObjectDataSource>
</asp:Content>
