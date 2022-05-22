<%@ Page Title="" Language="C#" MasterPageFile="~/Menu/MasterPage_Menu.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="FiveHead.Menu.Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="css/product.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_PageContent" runat="server">
    <div class="grid" runat="server" id="list_Products"></div>
    <asp:PlaceHolder ID="PlaceHolder_Empty" runat="server" Visible="false">
        <h1>No Products Found!</h1>
    </asp:PlaceHolder>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_Scripts" runat="server">
</asp:Content>
