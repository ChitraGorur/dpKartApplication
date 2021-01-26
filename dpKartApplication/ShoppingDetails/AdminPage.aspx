<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="ShoppingCartApplication.ShoppingDetails.AdminPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:RadioButtonList ID="rblSelectDataSource" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblSelectDataSource_SelectedIndexChanged">
        <asp:ListItem Text="Data from Excel" Value="0" Selected="True" />
        <asp:ListItem Text="Data from File" Value="1" />
    </asp:RadioButtonList>
    </div>
    <hr />
    <asp:Button ID="btnSelectDataSource" runat="server" Text="Set Data Source and Redirect to product catalog" OnClick="btnSelectDataSource_Click"/>
</asp:Content>
