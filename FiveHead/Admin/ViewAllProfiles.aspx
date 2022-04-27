<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage_Admin.Master" AutoEventWireup="true" CodeBehind="ViewAllProfiles.aspx.cs" Inherits="FiveHead.Admin.ViewAllProfiles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_PageContent" runat="server">
    <div class="container-fluid">

        <!-- Page Heading -->
        <h1 class="h3 mb-2 text-gray-800">View All User Profiles</h1>

        <div class="card shadow mb-4">
            <div class="card-header py-3">
                <h6 class="m-0 font-weight-bold text-primary">User Profiles Table</h6>
            </div>
            <div class="card-body">
                <!-- Success Alert -->
                <div class="row justify-content-center">
                    <div id="success-suspend" class="alert alert-success alert-dismissible fade show col-12 mt-4 success-alert-box" role="alert">
                        <i class="fas fa-check"></i>
                        User profile has been suspended!
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div id="error-suspend" class="alert alert-danger alert-dismissible fade show col-12 mt-4 danger-alert-box" role="alert">
                        <i class="fas fa-exclamation-triangle"></i>
                        Failed to suspend user profile.
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div id="success-activate" class="alert alert-success alert-dismissible fade show col-12 mt-4 success-alert-box" role="alert">
                        <i class="fas fa-check"></i>
                        User profile has been re-activated!
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div id="error-activate" class="alert alert-danger alert-dismissible fade show col-12 mt-4 danger-alert-box" role="alert">
                        <i class="fas fa-exclamation-triangle"></i>
                        Failed to re-activate user profile.
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                </div>

                <div class="table-responsive">
                    <asp:GridView ID="gv_Profiles" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" AllowPaging="True" OnPageIndexChanging="gv_Accounts_PageIndexChanging" PageSize="10">
                        <Columns>
                            <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="col-1">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_ProfileID" runat="server" Text='<%# Bind("profileID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Profile Name" ItemStyle-CssClass="col-4">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_ProfileName" runat="server" Text='<%# Bind("profileName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                    </asp:GridView>
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
