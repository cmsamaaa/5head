<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage_Admin.Master" AutoEventWireup="true" CodeBehind="CreateProfile.aspx.cs" Inherits="FiveHead.Admin.CreateProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_PageContent" runat="server">
    <div class="container-fluid">

        <!-- Page Heading -->
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0 text-gray-800">Create User Profile</h1>
        </div>

        <div class="row">

            <div class="col-lg-12">

                <!-- Basic Card Example -->
                <div class="card shadow mb-4 border-bottom-primary">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">User Profile Details</h6>
                    </div>
                    <div class="card-body p-0">
                        <!-- Success Alert -->
                        <div class="row justify-content-center">
                            <div class="alert alert-success alert-dismissible fade show col-7 mt-4 success-alert-box" role="alert">
                                <i class="fas fa-check"></i>
                                New user profile has been created!
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="alert alert-danger alert-dismissible fade show col-7 mt-4 danger-alert-box" role="alert">
                                <i class="fas fa-exclamation-triangle"></i>
                                Failed to create user profile.
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                        </div>
                        <!-- Nested Row within Card Body -->
                        <div class="row justify-content-center">
                            <div class="col-lg-8">
                                <div class="p-5">
                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <input runat="server" type="text" class="form-control form-control-user" id="tb_ProfileName" placeholder="Profile Name" required>
                                        </div>
                                    </div>
                                    <button runat="server" id="btn_Create" class="btn btn-primary btn-user btn-block" onserverclick="btn_Create_Click">Create</button>
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
        });
    </script>
</asp:Content>
