<%@ Page Title="" Language="C#" MasterPageFile="~/Restaurant/MasterPage_Restaurant.Master" AutoEventWireup="true" CodeBehind="ViewAllProducts.aspx.cs" Inherits="FiveHead.Restaurant.ViewAllProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_PageContent" runat="server">
    <div class="content-wrapper">
        <div class="row">
            <div class="col-lg-12 grid-margin stretch-card">
                <div class="card">
                    <div class="card-body">
                        <h4 class="card-title">Menu Items</h4>
                        <p class="card-description">
                            List of all menu items
                        </p>
                        <div class="form-group row">
                            <div class="input-group col-sm-6">
                                <div class="input-group-prepend bg-transparent">
                                    <span class="input-group-text bg-transparent border-right-0">
                                        <i class="mdi mdi-magnify text-primary"></i>
                                    </span>
                                </div>
                                <input runat="server" type="text" class="form-control form-control-lg border-left-0 login-field" id="tb_Search" placeholder="Search by Product Name" />
                            </div>
                            <div class="input-group col-sm-6">
                                <button runat="server" id="btn_Search" class="btn btn-primary" onserverclick="btn_Search_Click">Search</button>
                            </div>
                        </div>
                        <!-- Success Alert -->
                        <div class="row justify-content-center">
                            <div id="success-suspend" class="alert alert-success alert-dismissible fade show col-12 mt-4 success-alert-box" role="alert">
                                <i class="fas fa-check"></i>
                                Menu item has been <strong>suspended</strong>!
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                            </div>
                            <div id="error-suspend" class="alert alert-danger alert-dismissible fade show col-12 mt-4 danger-alert-box" role="alert">
                                <i class="fas fa-exclamation-triangle"></i>
                                Failed to suspend menu item.
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                            </div>
                            <div id="success-activate" class="alert alert-success alert-dismissible fade show col-12 mt-4 success-alert-box" role="alert">
                                <i class="fas fa-check"></i>
                                Menu item has been <strong>re-activated</strong>!
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                            </div>
                            <div id="error-activate" class="alert alert-danger alert-dismissible fade show col-12 mt-4 danger-alert-box" role="alert">
                                <i class="fas fa-exclamation-triangle"></i>
                                Failed to re-activate menu item.
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                            </div>
                        </div>
                        <div class="table-responsive pt-3">
                            <asp:GridView ID="gv_Products" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" OnRowCommand="gv_Products_RowCommand" AllowPaging="True" OnPageIndexChanging="gv_Products_PageIndexChanging" PageSize="10">
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="col-1">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_ProductID" runat="server" Text='<%# Bind("productID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_ProductName" runat="server" Text='<%# Bind("productName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price" ItemStyle-CssClass="col-2">
                                        <ItemTemplate>
                                            $
                                            <asp:Label ID="lbl_Price" runat="server" Text='<%# Eval("price", "{0:0.00}") %>'></asp:Label>
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
                        <asp:PlaceHolder ID="PlaceHolder_NoProduct" runat="server" Visible="false">
                            No products found
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
