<%@ Page Title="" Language="C#" MasterPageFile="~/Restaurant/MasterPage_Restaurant.Master" AutoEventWireup="true" CodeBehind="ViewAllOrders.aspx.cs" Inherits="FiveHead.Restaurant.ViewAllOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_PageContent" runat="server">
    <div class="content-wrapper">
        <div class="row">
            <div class="col-lg-12 grid-margin stretch-card">
                <div class="card">
                    <div class="card-body">
                        <h4 class="card-title">Order History</h4>
                        <p class="card-description">
                            List of all existing orders in the database.
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
                            <asp:GridView ID="gv_Orders" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" OnRowCommand="gv_Orders_RowCommand" AllowPaging="True" OnPageIndexChanging="gv_Orders_PageIndexChanging" PageSize="10">
                                <Columns>
                                    <asp:TemplateField HeaderText="Table No." ItemStyle-CssClass="col-1">
                                        <ItemTemplate>
                                            #<asp:Label ID="lbl_TableNumber" runat="server" Text='<%# Bind("tableNumber") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Start Date Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_StartDateTime" runat="server" Text='<%# Bind("start_datetime") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="End Date Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_EndDateTime" runat="server" Text='<%# Bind("end_datetime") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Bill">
                                        <ItemTemplate>
                                            $ <asp:Label ID="lbl_FinalPrice" runat="server" Text='<%# Eval("totalBill", "{0:0.00}") %>'></asp:Label>
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
                                    <asp:TemplateField ItemStyle-CssClass="col-3">
                                        <ItemTemplate>
                                            <asp:Button ID="btn_View" runat="server" Text='View' CssClass="btn btn-success mr-4" CommandName='View' CommandArgument='<%#((GridViewRow)Container).RowIndex%>' />
                                            <asp:Button ID="btn_Suspend" runat="server" Text='Suspend' CssClass='btn btn-danger' CommandName='Suspend' CommandArgument='<%#((GridViewRow)Container).RowIndex%>' OnClientClick='<%# Bind("message") %>' Visible='<%# Bind("suspendVisible") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                            </asp:GridView>
                        </div>
                        <asp:PlaceHolder ID="PlaceHolder_NoOrders" runat="server" Visible="false">No orders found
                        </asp:PlaceHolder>
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
