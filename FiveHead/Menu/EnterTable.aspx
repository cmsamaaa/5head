<%@ Page Title="" Language="C#" MasterPageFile="~/Menu/MasterPage_Menu.Master" AutoEventWireup="true" CodeBehind="EnterTable.aspx.cs" Inherits="FiveHead.Menu.EnterTable" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="css/forms.css">
    <link rel="stylesheet" href="css/product.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_PageContent" runat="server">
    <div class="centered">
        <h2>Enter Table Number</h2>
        <input runat="server" type="text" name="tb_TableNo" id="tb_TableNo" class="tableNo__input">
        <asp:Button ID="btn_Submit" runat="server" Text="Start Dining!" CssClass="btn-custom" OnClick="btn_Submit_Click" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_Scripts" runat="server">
</asp:Content>
