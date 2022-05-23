<%@ Page Title="" Language="C#" MasterPageFile="~/Menu/MasterPage_Menu.Master" AutoEventWireup="true" CodeBehind="Billing.aspx.cs" Inherits="FiveHead.Menu.Billing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="css/cart.css">
    <link rel="stylesheet" href="css/forms.css">
    <link rel="stylesheet" href="css/product.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_PageContent" runat="server">
    <ul class='cart__item-list' runat="server" id="list_Orders"></ul>
    <div class='centered coupon__group-control'>
        <div class="form-control coupon__input">
            <h4>Total Bill: <asp:Label ID="lbl_TotalBill" runat="server"></asp:Label></h4>
            <asp:Button ID="btn_Payment" CssClass="btn-custom" runat="server" Text="Make Payment" OnClick="btn_Payment_Click" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_Scripts" runat="server">
</asp:Content>
