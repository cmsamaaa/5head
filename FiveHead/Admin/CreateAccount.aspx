<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage_Admin.Master" AutoEventWireup="true" CodeBehind="CreateAccount.aspx.cs" Inherits="FiveHead.Admin.CreateAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src='http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.5/jquery-ui.min.js'></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_PageContent" runat="server">
    <div class="container-fluid">

        <!-- Page Heading -->
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0 text-gray-800">Create User Account</h1>
        </div>

        <div class="row">

            <div class="col-lg-12">

                <!-- Basic Card Example -->
                <div class="card shadow mb-4 border-bottom-primary">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">User Account Details</h6>
                    </div>
                    <div class="card-body p-0">
                        <!-- Start of Alert Boxes -->
                        <!-- PlaceHolder - Temp Message -->
                        <asp:PlaceHolder ID="PlaceHolder_Selection_Msg" runat="server" Visible="False">
                            <!-- Success Alert -->
                            <div class="row justify-content-center">
                                <div class="alert alert-danger alert-dismissible fade show col-7 mt-4" role="alert">
                                    <i class="fas fa-exclamation-triangle"></i>
                                    Please select an user account type.
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                </div>
                            </div>
                        </asp:PlaceHolder>

                        <!-- PlaceHolder - Empty Field -->
                        <asp:PlaceHolder ID="PlaceHolder_EmptyFields" runat="server" Visible="False">
                            <!-- Success Alert -->
                            <div class="row justify-content-center">
                                <div class="alert alert-danger alert-dismissible fade show col-7 mt-4" role="alert">
                                    <i class="fas fa-exclamation-triangle"></i>
                                    Some fields are empty! Please kindly fill them up to proceed with the creation.
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                </div>
                            </div>
                        </asp:PlaceHolder>

                        <!-- PlaceHolder - Error Account -->
                        <asp:PlaceHolder ID="PlaceHolder_Error_Account" runat="server" Visible="False">
                            <!-- Success Alert -->
                            <div class="row justify-content-center">
                                <div class="alert alert-danger alert-dismissible fade show col-7 mt-4" role="alert">
                                    <i class="fas fa-exclamation-triangle"></i>
                                    Failed to create user account.
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                </div>
                            </div>
                        </asp:PlaceHolder>

                        <!-- PlaceHolder - Error Staff -->
                        <asp:PlaceHolder ID="PlaceHolder_Error_Staff" runat="server" Visible="False">
                            <!-- Success Alert -->
                            <div class="row justify-content-center">
                                <div class="alert alert-danger alert-dismissible fade show col-7 mt-4" role="alert">
                                    <i class="fas fa-exclamation-triangle"></i>
                                    Failed to create staff record.
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                </div>
                            </div>
                        </asp:PlaceHolder>

                        <!-- Success Alert -->
                        <div class="row justify-content-center">
                            <div class="alert alert-success alert-dismissible fade show col-7 mt-4 success-alert-box" role="alert">
                                <i class="fas fa-check"></i>
                                New user account has been created!
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="alert alert-danger alert-dismissible fade show col-7 mt-4 danger-alert-box" role="alert">
                                <i class="fas fa-exclamation-triangle"></i>
                                Failed to create user account.
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                        </div>
                        <!-- End of Alert Boxes -->

                        <!-- Nested Row within Card Body -->
                        <div class="row justify-content-center">
                            <div class="col-lg-7">
                                <div class="p-5">
                                    <div class="form-group row">
                                        <div class="col-sm-6 mb-3 mb-sm-0">
                                            <select runat="server" id="ddl_AccountType" class="custom-select" required>
                                                <option selected disabled>Account Type</option>
                                                <option value="staff">Staff</option>
                                                <option value="administrator">Administrator</option>
                                            </select>
                                        </div>
                                        <div class="col-sm-6">
                                            <select runat="server" id="ddl_UserProfile" class="custom-select">
                                                <option selected disabled>User Profile</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="form-group row row-name">
                                        <div class="col-sm-6 mb-3 mb-sm-0">
                                            <input runat="server" type="text" class="form-control form-control-user" id="tb_FirstName" placeholder="First Name">
                                        </div>
                                        <div class="col-sm-6">
                                            <input runat="server" type="text" class="form-control form-control-user" id="tb_LastName" placeholder="Last Name">
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <input runat="server" type="text" class="form-control form-control-user" id="tb_Username" placeholder="Username" required>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-6 mb-3 mb-sm-0">
                                            <input runat="server" type="password" class="form-control form-control-user" id="tb_Password" placeholder="Password" required>
                                        </div>
                                        <div class="col-sm-6">
                                            <input runat="server" type="password" class="form-control form-control-user" id="tb_RepeatPassword" placeholder="Repeat Password" required>
                                        </div>
                                    </div>
                                    <button runat="server" id="btn_Create" class="btn btn-primary btn-user btn-block" onserverclick="btn_Create_Click" type="button">Create</button>
                                </div>
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
            const queryStr = getParameterByName("create");
            if (queryStr == "true")
                $('.success-alert-box').show();
            if (queryStr == "false")
                $('.danger-alert-box').show();

            $('#ContentPlaceHolder_PageContent_ddl_AccountType').on('change', function () {
                if (this.value === "administrator") {
                    $('.row-name').fadeOut();
                    $('#ContentPlaceHolder_PageContent_ddl_UserProfile').fadeOut();
                }
                else {
                    $('.row-name').fadeIn();
                    $('#ContentPlaceHolder_PageContent_ddl_UserProfile').fadeIn();
                }
            });
        });
    </script>
</asp:Content>
