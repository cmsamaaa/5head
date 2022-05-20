<%@ Page Title="" Language="C#" MasterPageFile="~/Restaurant/MasterPage_Restaurant.Master" AutoEventWireup="true" CodeBehind="ViewOrder.aspx.cs" Inherits="FiveHead.Restaurant.ViewOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_PageContent" runat="server">
    <div class="content-wrapper">
        <div class="row">
            <div class="col-lg-12 grid-margin stretch-card">
                <div class="card">
                    <div class="card-body">
                        <h4 class="card-title">Order Details</h4>
                        <p class="card-description">
                            List of all items for <strong>Table #<asp:Label ID="lbl_TableNo" runat="server" Text="0"></asp:Label></strong> at <asp:Label ID="lbl_StartDatetime" runat="server" Text="20/05/2022"></asp:Label>.
                        </p>
                        <!-- Success Alert -->
                        <div class="row justify-content-center">
                            <div id="success-suspend" class="alert alert-success alert-dismissible fade show col-12 mt-4 success-alert-box" role="alert">
                                <i class="fas fa-check"></i>
                                Order has been <strong>suspended</strong>!
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div id="error-suspend" class="alert alert-danger alert-dismissible fade show col-12 mt-4 danger-alert-box" role="alert">
                                <i class="fas fa-exclamation-triangle"></i>
                                Failed to suspend order.
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                        </div>
                        <div class="table-responsive pt-3">
                            <asp:GridView ID="gv_Orders" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" AllowPaging="True" OnPageIndexChanging="gv_Orders_PageIndexChanging" PageSize="10">
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="col-1">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_OrderID" runat="server" Text='<%# Bind("orderID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_ProductName" runat="server" Text='<%# Bind("productName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-CssClass="col-1">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Quantity" runat="server" Text='<%# Bind("productQty") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price (per unit)" ItemStyle-CssClass="col-2">
                                        <ItemTemplate>
                                            $ <asp:Label ID="lbl_Price" runat="server" Text='<%# Eval("price", "{0:0.00}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment Status" ItemStyle-CssClass="col-2">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_PaymentStatus" runat="server" Text='<%# Bind("paymentStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Status" ItemStyle-CssClass="col-1">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_OrderStatus" runat="server" Text='<%# Bind("orderStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                            </asp:GridView>
                        </div>
                        <div class="row pt-3 align-items-center">
                            <div class="col-6 text-left">
                                <asp:Button ID="btn_Back" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btn_Back_Click" />
                            </div>
                            <div class="col-6 text-right">
                                <span><strong>Total Bill: $ <asp:Label ID="lbl_TotalBill" runat="server" Text="0.00"></asp:Label></strong></span>
                            </div>
                        </div>
                        <div class="row pt-5">
                            <div class="col-12 text-right">
                                <asp:Button ID="btn_Complete" runat="server" Text="Complete Order" CssClass="btn btn-success" OnClick="btn_Complete_Click" OnClientClick="return confirm('Are you sure you want to mark the order as completed? This action cannot be reverted.')" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_Scripts" runat="server">
    <script>
        $(document).ready(function () {
            const queryStr_Suspend = getParameterByName("suspend");
            if (queryStr_Suspend == "true")
                $('#success-suspend').show();
            if (queryStr_Suspend == "false")
                $('#error-suspend').show();
        });
    </script>
</asp:Content>
