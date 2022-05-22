<% @ Page Title="" Language="C#" MasterPageFile="~/MasterPage_Login.Master" AutoEventWireup="true" CodeBehind="Menu_OLD.aspx.cs" Inherits="FiveHead.Menu_OLD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="border-width: 5px;" runat="server">
        <div id="output" runat="server">
            <asp:Label ID="lbl_Debug_Output" runat="server">
                <!-- Debug Output Here -->
            </asp:Label>
        </div>

        <asp:Table id="table_MenuItems" runat="server">
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

    <div id="filter">
        <!-- DropDownList for Filter Type -->
        <label id="lb_filter_Type" runat="server">Filter Type: </label>
        <asp:DropDownList id="ddl_filter_Type" runat="server">
            <asp:ListItem Selected="True" Value="ProductName">Product Name</asp:ListItem>
            <asp:ListItem Value="ProductID">Product ID</asp:ListItem>
        </asp:DropDownList>

        <!-- Filter Text -->
        <label id="lb_filter_Text">Filter Text: </label>
        <input type="text" id="tb_filter_Text" runat="server" />
        
        <!-- Start Filter -->
        <button id="btn_FilterMenuItems" onserverclick="btn_filter_menu_Items" runat="server">Filter Menu Items</button>
    </div>

    <br />

    <div id="button" runat="server">
        <button id="btn_ViewAll" onserverclick="btn_view_all_menuItems" runat="server">View All Menu Items</button>
        <button id="btn_GoToCart" onserverclick="btn_go_to_Cart" runat="server">Go to Cart</button>
    </div>
</asp:Content>

