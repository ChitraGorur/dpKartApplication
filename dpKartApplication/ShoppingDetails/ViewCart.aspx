<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewCart.aspx.cs" Inherits="ShoppingCartApplication.ShoppingDetails.ViewCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin: 10px;">
        <asp:GridView ID="grdCart" runat="server" AutoGenerateColumns="false" AlternatingRowStyle-BackColor="LightGray" HeaderStyle-BackColor="#b2b2b2" HeaderStyle-Width="50px" OnRowCommand="grdCart_RowCommand">
            <Columns>
                <asp:BoundField DataField="ProductCode" HeaderText="Product Code" />
                <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                <asp:BoundField DataField="QuantitySelected" HeaderText="Quantity Selected" />
                <asp:BoundField DataField="UnitPrice" HeaderText="Unit Price" />
                <asp:BoundField DataField="TotalCalculatedPrice" HeaderText="Total Price" />
                <asp:TemplateField HeaderText="Cart">
                    <ItemTemplate>
                        <asp:Button ID="btnRemoveCart" runat="server" Text="Remove From Cart" CommandName="RemoveFromCart" CommandArgument="<%#((GridViewRow)Container).RowIndex %>" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div>
        <asp:Label ID="lblEmptyCart" runat="server" Text="your cart is Empty!" Font-Bold="True" Visible="false" />
        <asp:Label ID="lblAmountLabel" runat="server" Text="Total Amount: " Font-Bold="True" />
        &nbsp;
        <asp:Label ID="lblOfferAmount" runat="server" Font-Bold="True" Font-Underline="True" />
        &nbsp;
        <asp:Label ID="lblTotalAmount" runat="server" Font-Bold="True" Font-Underline="True" Visible="false" />
    </div>
    <hr />
    <div id="EmptyCart" style="padding-left: 450px">
        <asp:Button ID="btnEmptyCart" runat="server" Text="Empty Cart" OnClick="btnEmptyCart_Click" Width="175px" />
    </div>
    <hr />
    <div>
        <asp:RadioButtonList ID="rblOffers" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblOffers_SelectedIndexChanged">
            <asp:ListItem Text="10% Cash Back" Value="10" Selected="True" />
            <asp:ListItem Text="5% Discount" Value="5" />
        </asp:RadioButtonList>
        <asp:Button ID="btnApply" runat="server" Text="Apply Offer" OnClick="btnApply_Click" />
        &nbsp;&nbsp;
        <asp:Button ID="btnRemove" runat="server" Text="Remove Offer" OnClick="btnRemove_Click" Enable="false" />
    </div>
    <hr />
    <div>
        <strong>Select Region to apply Tax :</strong>
        &nbsp;&nbsp;
        <asp:DropDownList ID="drpStateCode" runat="server">
            <asp:ListItem Text="India" Value="IND" Selected="True" />
            <asp:ListItem Text="Outside India" Value="OutsideIndia" />
        </asp:DropDownList>
    </div>
    <hr />
    <div>
        <asp:Label ID="lblTaxLabel" runat="server" Text="Total Amount Payable after Tax: " Font-Bold="True" Font-Underline="True" Visible="false" />
        <asp:Label ID="lblTotalAmountAfterTax" runat="server" Font-Bold="True" Font-Underline="True" Visible="false" />
    </div>
    <hr />
    <div>
        <asp:Button ID="btnCheckout" runat="server" Text="CheckOut" OnClick="btnCheckout_Click" />
    </div>
</asp:Content>
