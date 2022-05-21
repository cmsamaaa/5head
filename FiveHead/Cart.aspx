<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="FiveHead.cart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="output" runat="server">
            <asp:Label ID="lbl_Debug_Output" runat="server">
                <!-- Debug Output Here -->
            </asp:Label>
        </div>

        <br />
        
        <div>
            <asp:Table id="table_Shopping_Cart" runat="server">
    <%--            <asp:TableRow runat="server">
                    <asp:TableCell ID="tc_ROW_ID" runat="server">S/N</asp:TableCell>
                    <asp:TableCell ID="tc_productID" runat="server">Product ID</asp:TableCell>
                    <asp:TableCell ID="tc_categoryID" runat="server">Category ID</asp:TableCell>
                    <asp:TableCell ID="tc_productName" runat="server">Product Name</asp:TableCell>
                    <asp:TableCell ID="tc_price" runat="server">Price</asp:TableCell>
                    <asp:TableCell ID="tc_button" runat="server">Button</asp:TableCell>
                </asp:TableRow>--%>
            </asp:Table>
        </div>

        <br />

        <!--
            Restaurant Tables
            -->
        <div id="tables">
            Tables
            <br/>
            <label id="lb_table_Number" runat="server">Table Number: </label>
            <input type="text" id="tb_table_Number" runat="server" /> 
        </div>

        <br />
        <br />

        <div id="coupon_code">
            Coupons
            <br />
            <label id="lb_coupon_Code" runat="server">Coupon Code: </label>
            <input type="text" id="tb_coupon_Code" runat="server" />
        </div>

        <br />
        <br />
        
        <div id="uInput">
            <!--
                Remove Product from Cart
            -->
<%--            <label id="lbl_prodID_to_remove" runat="server">Product ID to Remove: </label>
            <input type="text" id="tb_prodID_to_remove" runat="server" />
            <button id="btn_RemoveFromCart" onserverclick="btn_remove_from_Cart" runat="server">Remove Item from Cart</button>--%>
            <button id="btn_ClearCart" onserverclick="btn_clear_all_in_Cart" runat="server">Clear all in Cart</button>

            <br />
            <br />
                        
            <!-- Label for displaying Information -->
            <label id="lbl_Info" runat="server"></label>
        </div>

        <br />

        <div id="buttons" runat="server">
            <button id="btn_Checkout_Cart" onserverclick="btn_checkout_Cart" runat="server">Checkout Cart</button>
            <button id="btn_GoTo_Menu" onserverclick="btn_go_to_Menu" runat="server">Go To Menu</button>
        </div>
    </form>
</body>
</html>
