<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="FiveHead.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table class="auto-style1">
        <tr>
            <td>Username*: </td>
            <td>
                <asp:TextBox ID="tb_Username" runat="server" CssClass="form-control" required="true"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Password*: </td>
            <td>
                <asp:TextBox ID="tb_Password" runat="server" TextMode="Password" CssClass="form-control" required="true"></asp:TextBox>
            </td>
            <td>&nbsp;&nbsp; Confirm Password*: </td>
            <td>
                <asp:TextBox ID="tb_ConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" required="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style3" colspan="4">*Mandatory fields</td>
        </tr>
        <tr>
            <td class="auto-style3" colspan="4">
                <div style="display: inline-block; padding-left: 20px;"></div>
                <div style="display: inline-block;">
                    <asp:CheckBox ID="cb_Agree" runat="server" Style="color: #000000" CssClass="checkbox" required="true" />
                    <span class="auto-style4">I agree to the </span><a href="#" class="auto-style4">terms & conditions</a>.</div>
            </td>
        </tr>
        <tr>
            <td class="auto-style2" colspan="4">
                <br />
                <asp:Button ID="btn_Register" runat="server" Text="Register" OnClick="btn_Register_Click" CssClass="btn btn-default" />
                <asp:Button ID="btn_Clear" runat="server" Text="Clear" CssClass="btn btn-default" CausesValidation="False" OnClick="btn_Clear_Click" UseSubmitBehavior="False" />
            </td>
        </tr>
    </table>
        </form>
</body>
</html>
