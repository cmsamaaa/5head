<%@ Page Title="" Language="C#" MasterPageFile="~/Restaurant/MasterPage_Restaurant.Master" AutoEventWireup="true" CodeBehind="CreateProduct.aspx.cs" Inherits="FiveHead.Restaurant.CreateProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_PageContent" runat="server">
    <div class="content-wrapper">
        <div class="row">
            <div class="col-12 grid-margin stretch-card">
                <div class="card">
                    <div class="card-body">
                        <h4 class="card-title">Create Menu Item</h4>
                        <p class="card-description">
                            Add a new product into the menu
                        </p>
                        <!-- Alert Message -->
                        <div class="row justify-content-center">
                            <div class="alert alert-success alert-dismissible fade show col-12 success-alert-box" role="alert">
                                <i class="fas fa-check"></i>
                                New menu item has been created!
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="alert alert-danger alert-dismissible fade show col-12 danger-alert-box" role="alert">
                                <i class="fas fa-exclamation-triangle"></i>
                                Failed to create menu item.
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
                                Please enter a valid price.
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="ContentPlaceHolder_PageContent_tb_ProductName" class="col-sm-3 col-form-label">Product Name</label>
                            <div class="col-sm-9">
                                <input runat="server" type="text" class="form-control" id="tb_ProductName" placeholder="Product Name">
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="ContentPlaceHolder_PageContent_tb_Price" class="col-sm-3 col-form-label">Price</label>
                            <div class="col-sm-9">
                                <input runat="server" type="text" class="form-control" id="tb_Price" placeholder="Price">
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="ContentPlaceHolder_PageContent_ddl_Category" class="col-sm-3 col-form-label">Category</label>
                            <div class="col-sm-9">
                                <select runat="server" class="form-control" id="ddl_Category">
                                    <option selected disabled>Category</option>
                                </select>
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
            const productName = getParameterByName("prod");
            const productPrice = getParameterByName("price");
            const productCategory = getParameterByName("cat");
            if (errorStr == "empty")
                $('.empty-alert-box').show();
            if (queryStr == "true")
                $('.success-alert-box').show();
            if (queryStr == "false")
                $('.danger-alert-box').show();
            if (productName != null)
                $('#ContentPlaceHolder_PageContent_tb_ProductName').val(productName);

            if (productPrice == "invalid")
                $('.invalid-alert-box').show();
            else if (productPrice != null)
                $('#ContentPlaceHolder_PageContent_tb_Price').val(productPrice);

            if (productCategory != null) {
                $('#ContentPlaceHolder_PageContent_ddl_Category').val(productCategory);
                $('#ContentPlaceHolder_PageContent_ddl_Category').css("color", "black");
            }

            $('#ContentPlaceHolder_PageContent_ddl_Category').on("change", function () {
                $('#ContentPlaceHolder_PageContent_ddl_Category').css("color", "black");
            });
        });
    </script>
</asp:Content>
