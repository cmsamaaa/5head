<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage_Admin.Master" AutoEventWireup="true" CodeBehind="ViewAllAccounts.aspx.cs" Inherits="FiveHead.Admin.ViewAllAccounts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_PageContent" runat="server">
    <div class="container-fluid">

        <!-- Page Heading -->
        <h1 class="h3 mb-2 text-gray-800">View All User Accounts</h1>

        <div class="card shadow mb-4">
            <div class="card-header py-3">
                <h6 class="m-0 font-weight-bold text-primary">User Accounts Table</h6>
            </div>
            <div class="card-body">
                <!-- Success Alert -->
                <div class="row justify-content-center">
                    <div id="success-suspend" class="alert alert-success alert-dismissible fade show col-12 mt-4 success-alert-box" role="alert">
                        <i class="fas fa-check"></i>
                        Account has been suspended!
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div id="error-suspend" class="alert alert-danger alert-dismissible fade show col-12 mt-4 danger-alert-box" role="alert">
                        <i class="fas fa-exclamation-triangle"></i>
                        Failed to suspend user account.
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div id="success-activate" class="alert alert-success alert-dismissible fade show col-12 mt-4 success-alert-box" role="alert">
                        <i class="fas fa-check"></i>
                        Account has been re-activated!
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div id="error-activate" class="alert alert-danger alert-dismissible fade show col-12 mt-4 danger-alert-box" role="alert">
                        <i class="fas fa-exclamation-triangle"></i>
                        Failed to re-activate user account.
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                </div>
                <asp:Panel ID="Panel_Search" runat="server" DefaultButton="btn_Search">
                    <div class="form-group row">
                        <div class="col-sm-6 mb-3 mb-sm-0">
                            <input runat="server" type="text" class="form-control form-control-user" id="tb_Search" placeholder="Search by Username or Profile Name">
                        </div>
                        <div class="col-sm-4">
                            <asp:Button ID="btn_Search" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btn_Search_Click" />
                        </div>
                    </div>
                </asp:Panel>
                <div class="table-responsive">
                    <asp:GridView ID="gv_Accounts" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" OnRowCommand="gv_Accounts_RowCommand" AllowPaging="True" OnPageIndexChanging="gv_Accounts_PageIndexChanging" PageSize="10">
                        <Columns>
                            <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="col-1">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_AccountID" runat="server" Text='<%# Bind("accountID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Username" ItemStyle-CssClass="col-4">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Username" runat="server" Text='<%# Bind("username") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User Profile" ItemStyle-CssClass="col-4">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_ProfileName" runat="server" Text='<%# Bind("profileName") %>'></asp:Label>
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
                <asp:PlaceHolder ID="PlaceHolder_Empty" runat="server" Visible="false">
                    No accounts found
                </asp:PlaceHolder>
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

            const search = getParameterByName("search");
            if (search != null)
                $('#ContentPlaceHolder_PageContent_tb_Search').val(search);
        });
    </script>
</asp:Content>
