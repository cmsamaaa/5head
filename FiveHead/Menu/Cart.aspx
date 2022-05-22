<%@ Page Title="" Language="C#" MasterPageFile="~/Menu/MasterPage_Menu.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="FiveHead.Menu.Cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="css/cart.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_PageContent" runat="server">
    <ul class='cart__item-list' runat="server" id="list_Cart"></ul>
    <div class='centered'>
        <asp:Button ID="btn_Order" CssClass="btn-custom" runat="server" Text="Place Order" />
    </div>
    <asp:PlaceHolder ID="PlaceHolder_Empty" runat="server" Visible="false">
        <h1>No Products in Cart!</h1>
    </asp:PlaceHolder>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_Scripts" runat="server">
</asp:Content>
