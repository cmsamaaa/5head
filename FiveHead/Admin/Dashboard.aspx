<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage_Admin.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="FiveHead.Admin.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #lbl_Today {
            font-weight: 800;
        }

        .message-body {
            font-size: 18px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_PageContent" runat="server">
    <div class="container-fluid">

        <!-- Page Heading -->
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0 text-gray-800">Dashboard</h1>
        </div>

        <div class="row">

            <div class="col-lg-12">

                <!-- Basic Card Example -->
                <div class="card shadow mb-4">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">Hello again,
                            <asp:Label ID="lbl_Username" runat="server" Text="Username"></asp:Label>!</h6>
                    </div>
                    <div class="card-body p-0">
                        <div class="text-center">
                            <img class="img-fluid px-3 px-sm-4 mt-3 mb-4" style="width: 50rem;" src="img/undraw_add_user.svg" alt="...">
                        </div>
                        <!-- Nested Row within Card Body -->
                        <div class="row justify-content-center">
                            <div class="col-11">
                                <div class="p-5">
                                    <span class="message-body">
                                        This is the Administrator dashboard. You may create, update, suspend, or re-activate user accounts and profiles here. 
                                        To begin, please select one of the options on the navigation panel located on the left at all times.
                                    </span>
                                    <br />
                                    <br />
                                    <span class="float-right" id="lbl_Today"></span>
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
            updateTime();
            setInterval(updateTime, 100);
        });

        function updateTime() {
            $('#lbl_Today').html(getTime);
        }
    </script>
</asp:Content>
