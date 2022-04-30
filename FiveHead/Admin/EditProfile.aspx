<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage_Admin.Master" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="FiveHead.Admin.EditProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_PageContent" runat="server">
    <div class="container-fluid">

        <!-- Page Heading -->
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0 text-gray-800">Update User Profile</h1>
        </div>

        <div class="row">

            <div class="col-lg-12">

                <!-- Basic Card Example -->
                <div class="card shadow mb-4 border-bottom-primary">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">User Profile Details</h6>
                    </div>
                    <div class="card-body p-0">
                        <!-- Start of Alert Boxes -->
                        <div class="row justify-content-center mt-4">

                            <!-- PlaceHolder - Success Profile Name -->
                            <asp:PlaceHolder ID="PlaceHolder_Success_ProfileName" runat="server" Visible="False">
                                <!-- Success Alert -->
                                <div class="alert alert-success alert-dismissible fade show col-7" role="alert">
                                    <i class="fas fa-exclamation-triangle"></i>
                                    Successfully updated profile name!
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                </div>
                            </asp:PlaceHolder>

                            <!-- PlaceHolder - Error Profile Name -->
                            <asp:PlaceHolder ID="PlaceHolder_Error_ProfileName" runat="server" Visible="False">
                                <!-- Success Alert -->
                                <div class="alert alert-danger alert-dismissible fade show col-7" role="alert">
                                    <i class="fas fa-exclamation-triangle"></i>
                                    Failed to update profile name.
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                </div>
                            </asp:PlaceHolder>
                        </div>
                        <!-- End of Alert Boxes -->

                        <!-- Nested Row within Card Body -->
                        <div class="row justify-content-center">
                            <div class="col-lg-8">
                                <div class="p-5">
                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <input runat="server" type="text" class="form-control form-control-user" id="tb_ProfileName" placeholder="Profile Name" required>
                                        </div>
                                    </div>
                                    <button runat="server" id="btn_Update" class="btn btn-primary btn-user btn-block" onserverclick="btn_Update_Click">Update</button>
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
</asp:Content>
