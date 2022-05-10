<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_Login.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="FiveHead.Menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Literal id="table_MenuItems" runat="server">

        </asp:Literal>
     </div>

    <div id="button">
        <button id="btn_add_to_Cart" serveronclick="btn_add_to_Cart">
            Add to Cart
        </button>
    </div>
</asp:Content>
