<%@ Page Title="" Language="C#" MasterPageFile="~/Menu/MasterPage_Menu.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="FiveHead.Menu.Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="css/product.css">
    <link rel="stylesheet" href="css/forms.css">
    <link rel="stylesheet" href="css/product.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_PageContent" runat="server">
    <div class="form-control coupon__input">
        <input runat="server" class="input-box" type="text" name="tb_Search" id="tb_Search" placeholder="Product Name"/>
        <button id="btn_Search" runat="server" class="btn-custom" onserverclick="btn_Search_Click">Search</button>
        <button id="btn_Clear" runat="server" class="btn-custom" onserverclick="btn_Clear_Click">Clear</button>
    </div>
    <div class="form-control coupon__input">
        <select runat="server" class="input-box" id="ddl_Category">
            <option selected disabled>Category</option>
        </select>
        <button id="btn_Filter" runat="server" class="btn-custom" onserverclick="btn_Filter_Click">Filter</button>
    </div>
    <div class="grid" runat="server" id="list_Products"></div>
    <asp:PlaceHolder ID="PlaceHolder_Empty" runat="server" Visible="false">
        <div class="centered">
            <h1>No Products Found!</h1>
        </div>
    </asp:PlaceHolder>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_Scripts" runat="server">
</asp:Content>
