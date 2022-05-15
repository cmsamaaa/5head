<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentSummary.aspx.cs" Inherits="FiveHead.PaymentSummary" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="user_info">
            <div id="uinfo_general">
                [General] <br />
                <label id="lbl_tableNumber" runat="server">Table Number: </label>
                <label id="lbl_tableNumber_Value" runat="server" /> 
                <br />
            </div>
            
            <br />

            <div id="uinfo_payment"> 
                [Payment] <br />
                <label id="lbl_Total" runat="server">Total: </label>
                <label id="lbl_total_Price_Value" runat="server" /> 
                <br />
                <label id="lbl_Discount" runat="server">Discount: </label>
                <label id="lbl_discount_Value" runat="server" /> 
                <br />
                <label id="lbl_final_Price" runat="server">Final Price: </label>
                <label id="lbl_final_Price_Value" runat="server" /> 
            </div>
        </div>

        <br />

        <div>
            <asp:Table id="table_payment_Summary" runat="server"></asp:Table>
        </div>

        <br />
        
        <div id="div_user_Input" runat="server">
            <label id="lb_uInput_ContactDetails">Contacts: </label>
            <input type="text" id="tb_uInput_ContactDetails" runat="server" />
        </div>

        <div>
            <label id="lbl_Info" runat="server"></label>
        </div>

        <br />

        <div id="buttons" runat="server">
            <button id="btn_Make_Payment" onserverclick="btn_make_Payment" runat="server">Make Payment</button>
            <button id="btn_GoTo_Cart" onserverclick="btn_go_to_Cart" runat="server">Go To Cart</button>
        </div>
    </form>
</body>
</html>
