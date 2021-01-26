<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductCatalog.aspx.cs" Inherits="ShoppingCartApplication.ProductCatalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin: 10px;">
        <asp:GridView ID="grdProductCatalog" runat="server" AutoGenerateColumns="false" AlternatingRowStyle-BackColor="LightGray" HeaderStyle-BackColor="#b2b2b2" HeaderStyle-Width="50px" OnRowCommand="grdProductCatalog_RowCommand">
            <Columns>
                 <asp:BoundField DataField="ProductCode" HeaderText="Product Code" />
                <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                <asp:BoundField DataField="ProductDescription" HeaderText="Product Description" />
                <asp:BoundField DataField="UnitPrice" HeaderText="Unit Price" />
                 <asp:BoundField DataField="VendorCode" HeaderText="Vendor Code" />
                <asp:TemplateField HeaderText="Product Quantity">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlQuantity" runat="server">
                            <asp:ListItem Text="1" Value="1" Selected="True" />
                            <asp:ListItem Text="2" Value="2" />
                            <asp:ListItem Text="3" Value="3" />
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cart">
                    <ItemTemplate>
                        <asp:Button ID="btnAddCart" runat="server" Text="Add to Cart" CommandName="AddToCart" CommandArgument="<%#((GridViewRow)Container).RowIndex %>" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>
    </div>
</asp:Content>
