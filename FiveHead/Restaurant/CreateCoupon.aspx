<%@ Page Title="" Language="C#" MasterPageFile="~/Restaurant/MasterPage_Restaurant.Master" AutoEventWireup="true" CodeBehind="CreateCoupon.aspx.cs" Inherits="FiveHead.Restaurant.CreateCoupon" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_PageContent" runat="server">
    <div class="content-wrapper">
        <div class="row">
            <div class="col-12 grid-margin stretch-card">
                <div class="card">
                    <div class="card-body">
                        <h4 class="card-title">Create Coupon</h4>
                        <p class="card-description">
                            Create a coupon for restaurant promotions
                        </p>
                        <!-- Alert Message -->
                        <div class="row justify-content-center">
                            <div class="alert alert-success alert-dismissible fade show col-12 success-alert-box" role="alert">
                                <i class="fas fa-check"></i>
                                New coupon has been created!
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="alert alert-danger alert-dismissible fade show col-12 danger-alert-box" role="alert">
                                <i class="fas fa-exclamation-triangle"></i>
                                Coupon code already exist! Please enter a different code.
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="alert alert-danger alert-dismissible fade show col-12 empty-alert-box" role="alert">
                                <i class="fas fa-exclamation-triangle"></i>
                                Please fill up all fields.
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="alert alert-danger alert-dismissible fade show col-12 invalid-alert-box" role="alert">
                                <i class="fas fa-exclamation-triangle"></i>
                                Please enter a valid discount.
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="ContentPlaceHolder_PageContent_tb_CouponCode" class="col-sm-3 col-form-label">Enter Coupon Code</label>
                            <div class="col-sm-3">
                                <input runat="server" type="text" class="form-control" id="tb_CouponCode" placeholder="Coupon Code">
                            </div>
                            <label for="ContentPlaceHolder_PageContent_tb_Discount" class="col-sm-3 col-form-label">Enter Discount (%)</label>
                            <div class="col-sm-3">
                                <input runat="server" type="text" class="form-control" id="tb_Discount" placeholder="10">
                            </div>
                        </div>
                        <button runat="server" id="btn_Create" class="btn btn-primary mr-2" onserverclick="btn_Create_Click">Create</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_Scripts" runat="server">
    <script>
        $(document).ready(function () {
            const errorStr = getParameterByName("error");
            const queryStr = getParameterByName("create");
            const couponCode = getParameterByName("code");
            const discount = getParameterByName("discount");
            if (errorStr == "empty")
                $('.empty-alert-box').show();
            if (queryStr == "true")
                $('.success-alert-box').show();
            if (queryStr == "false")
                $('.danger-alert-box').show();
            if (couponCode != null)
                $('#ContentPlaceHolder_PageContent_tb_CouponCode').val(couponCode);
            if (discount == "invalid")
                $('.invalid-alert-box').show();
            else if (discount != null)
                $('#ContentPlaceHolder_PageContent_tb_Discount').val(discount);
        });
    </script>
</asp:Content>
