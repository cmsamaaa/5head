<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="FiveHead.Admin.cart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Literal runat="server" id="table_Shopping_Cart">
                Hello World
            </asp:Literal>
        </div>

        <div id="buttons">
            <button id="btn_checkout_Cart" onserverclick="btn_checkout_Cart">Checkout Cart</button>
            <button id="btn_make_Payment" onserverclick="btn_make_Payment">Make Payment</button>
        </div>
    </form>
</body>
</html>
