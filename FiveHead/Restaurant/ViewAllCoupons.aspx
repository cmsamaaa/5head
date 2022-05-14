<%@ Page Title="" Language="C#" MasterPageFile="~/Restaurant/MasterPage_Restaurant.Master" AutoEventWireup="true" CodeBehind="ViewAllCoupons.aspx.cs" Inherits="FiveHead.Restaurant.ViewAllCoupons" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_PageContent" runat="server">
    <div class="content-wrapper">
        <div class="row">
            <div class="col-lg-12 grid-margin stretch-card">
                <div class="card">
                    <div class="card-body">
                        <h4 class="card-title">Coupons</h4>
                        <p class="card-description">
                            List of all discount coupons
                        </p>
                        <!-- Success Alert -->
                        <div class="row justify-content-center">
                            <div id="success-suspend" class="alert alert-success alert-dismissible fade show col-12 mt-4 success-alert-box" role="alert">
                                <i class="fas fa-check"></i>
                                Coupon has been <strong>suspended</strong>!
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                            </div>
                            <div id="error-suspend" class="alert alert-danger alert-dismissible fade show col-12 mt-4 danger-alert-box" role="alert">
                                <i class="fas fa-exclamation-triangle"></i>
                                Failed to suspend coupon.
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                            </div>
                            <div id="success-activate" class="alert alert-success alert-dismissible fade show col-12 mt-4 success-alert-box" role="alert">
                                <i class="fas fa-check"></i>
                                Coupon has been <strong>re-activated</strong>!
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                            </div>
                            <div id="error-activate" class="alert alert-danger alert-dismissible fade show col-12 mt-4 danger-alert-box" role="alert">
                                <i class="fas fa-exclamation-triangle"></i>
                                Failed to re-activate coupon.
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                            </div>
                        </div>
                        <div class="table-responsive pt-3">
                            <asp:GridView ID="gv_Coupons" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" OnRowCommand="gv_Coupons_RowCommand" AllowPaging="True" OnPageIndexChanging="gv_Coupons_PageIndexChanging" PageSize="10">
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="col-1">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_CouponID" runat="server" Text='<%# Bind("couponID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Coupon">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Code" runat="server" Text='<%# Bind("code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Discount" ItemStyle-CssClass="col-1">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Discount" runat="server" Text='<%# Eval("discount", @"{0:#\%}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Suspended" ItemStyle-CssClass="col-1">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Deactivated" runat="server" Text='<%# Bind("deactivated") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="col-2">
                                        <ItemTemplate>
                                            <asp:Button ID="btn_Edit" runat="server" Text="Edit" CssClass="btn btn-primary mr-4" CommandName="Edit" CommandArgument='<%#((GridViewRow)Container).RowIndex%>' Visible='<%# Bind("editVisible") %>' />
                                            <asp:Button ID="btn_Suspend" runat="server" Text='<%# Bind("suspend") %>' CssClass='<%# Bind("css") %>' CommandName='<%# Bind("suspend") %>' CommandArgument='<%#((GridViewRow)Container).RowIndex%>' OnClientClick='<%# Bind("message") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                            </asp:GridView>
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

            const queryStr_Activate = getParameterByName("activate");
            if (queryStr_Activate == "true")
                $('#success-activate').show();
            if (queryStr_Activate == "false")
                $('#error-activate').show();
        });
    </script>
</asp:Content>
