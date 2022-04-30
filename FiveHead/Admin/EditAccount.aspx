<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage_Admin.Master" AutoEventWireup="true" CodeBehind="EditAccount.aspx.cs" Inherits="FiveHead.Admin.EditAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_PageContent" runat="server">
    <div class="container-fluid">

        <!-- Page Heading -->
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0 text-gray-800">Update User Account</h1>
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
                        <div class="row justify-content-center mt-4">

                            <!-- PlaceHolder - Success Username -->
                            <asp:PlaceHolder ID="PlaceHolder_Success_Username" runat="server" Visible="False">
                                <!-- Success Alert -->
                                <div class="alert alert-success alert-dismissible fade show col-7" role="alert">
                                    <i class="fas fa-exclamation-triangle"></i>
                                    Successfully updated username!
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                </div>
                            </asp:PlaceHolder>

                            <!-- PlaceHolder - Error Username -->
                            <asp:PlaceHolder ID="PlaceHolder_Error_Username" runat="server" Visible="False">
                                <!-- Success Alert -->
                                <div class="alert alert-danger alert-dismissible fade show col-7" role="alert">
                                    <i class="fas fa-exclamation-triangle"></i>
                                    Failed to update username.
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                </div>
                            </asp:PlaceHolder>

                            <!-- PlaceHolder - Success Password -->
                            <asp:PlaceHolder ID="PlaceHolder_Success_Password" runat="server" Visible="False">
                                <!-- Success Alert -->
                                <div class="alert alert-success alert-dismissible fade show col-7" role="alert">
                                    <i class="fas fa-exclamation-triangle"></i>
                                    Successfully updated password!
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                </div>
                            </asp:PlaceHolder>

                            <!-- PlaceHolder - Error Password -->
                            <asp:PlaceHolder ID="PlaceHolder_Error_Password" runat="server" Visible="False">
                                <!-- Success Alert -->
                                <div class="alert alert-danger alert-dismissible fade show col-7" role="alert">
                                    <i class="fas fa-exclamation-triangle"></i>
                                    Failed to update password.
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                </div>
                            </asp:PlaceHolder>

                            <!-- PlaceHolder - Mismatch Password -->
                            <asp:PlaceHolder ID="PlaceHolder_Mismatch_Password" runat="server" Visible="False">
                                <!-- Success Alert -->
                                <div class="alert alert-danger alert-dismissible fade show col-7" role="alert">
                                    <i class="fas fa-exclamation-triangle"></i>
                                    Password mismatch, please fill them up again!
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                </div>
                            </asp:PlaceHolder>

                            <!-- PlaceHolder - Success Staff -->
                            <asp:PlaceHolder ID="PlaceHolder_Success_Staff" runat="server" Visible="False">
                                <!-- Success Alert -->
                                <div class="alert alert-success alert-dismissible fade show col-7" role="alert">
                                    <i class="fas fa-exclamation-triangle"></i>
                                    Successfully updated staff record!
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                </div>
                            </asp:PlaceHolder>

                            <!-- PlaceHolder - Error Staff -->
                            <asp:PlaceHolder ID="PlaceHolder_Error_Staff" runat="server" Visible="False">
                                <!-- Success Alert -->
                                <div class="alert alert-danger alert-dismissible fade show col-7" role="alert">
                                    <i class="fas fa-exclamation-triangle"></i>
                                    Failed to update staff record.
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                </div>
                            </asp:PlaceHolder>
                        </div>
                        <!-- End of Alert Boxes -->

                        <!-- Nested Row within Card Body -->
                        <div class="row justify-content-center">
                            <div class="col-lg-7">
                                <div class="p-5">
                                    <div class="form-group row">
                                        <div class="col-sm-6 mb-3 mb-sm-0">
                                            <input runat="server" type="text" class="form-control form-control-user" id="tb_AccountType" placeholder="Account Type" disabled>
                                        </div>
                                        <div class="col-sm-6">
                                            <input runat="server" type="text" class="form-control form-control-user" id="tb_UserProfile" placeholder="User Profile" disabled>
                                        </div>
                                    </div>
                                    <div runat="server" id="div_Name" class="form-group row row-name">
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
                                    <asp:Label ID="lbl_AccountID" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lbl_Username" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lbl_StaffID" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lbl_FirstName" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lbl_LastName" runat="server" Text="" Visible="false"></asp:Label>
                                    <button runat="server" id="btn_Update" class="btn btn-primary btn-user btn-block" onserverclick="btn_Update_Click" type="button">Update</button>
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
