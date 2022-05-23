<%@ Page Title="" Language="C#" MasterPageFile="~/Menu/MasterPage_Menu.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="FiveHead.Menu.Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="css/cart.css">
    <link rel="stylesheet" href="css/forms.css">
    <link rel="stylesheet" href="css/product.css">
    <link rel="stylesheet" href="/Admin/vendor/fontawesome-free/css/all.min.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_PageContent" runat="server">
    <ul class='cart__item-list' runat="server" id="list_Cart"></ul>
    <asp:PlaceHolder ID="PlaceHolder_Content" runat="server">
        <div class='centered coupon__group-control'>
            <div class="form-control coupon__input">
                <input runat="server" class="input-box" type="text" name="tb_Coupon" id="tb_Coupon" placeholder="Enter coupon code"/>
                <button id="btn_Coupon" runat="server" class="btn-custom" onserverclick="btn_Coupon_Click">Use Coupon</button>
            </div>
            <div class="form-control coupon__input">
                <input runat="server" class="input-box" type="text" name="tb_Contact" id="tb_Contact" placeholder="Enter contact information"/>
            </div>
            <div class="form-control coupon__input">
                <h4>Total Bill: <asp:Label ID="lbl_TotalBill" runat="server"></asp:Label></h4>
                <asp:Button ID="btn_Clear" CssClass="btn-custom danger" runat="server" Text="Clear Cart" OnClick="btn_Clear_Click" />
                <asp:Button ID="btn_Order" CssClass="btn-custom" runat="server" Text="Place Order" OnClick="btn_Order_Click" />
            </div>
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="PlaceHolder_Empty" runat="server" Visible="false">
        <div class="centered">
            <h1>No Products in Cart!</h1>
        </div>
    </asp:PlaceHolder>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_Scripts" runat="server">
    <script>
        const paymentStr = getParameterByName("payment");
        if (paymentStr == "true")
            alert("Payment successful!");
    </script>
</asp:Content>
