<%@ Page Title="" Language="C#" MasterPageFile="~/Restaurant/MasterPage_Restaurant.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="FiveHead.Restaurant.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #lbl_Today {
            color: #4d83ff;
            font-weight: 600;
        }

        .message-body {
            font-size: 18px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_PageContent" runat="server">
    <div class="content-wrapper">
        <div class="row">
            <div class="col-md-12 grid-margin">
                <div class="d-flex justify-content-between flex-wrap">
                    <div class="d-flex align-items-end flex-wrap">
                        <div class="mr-md-3 mr-xl-5">
                            <h2>Welcome back</h2>
                            <p class="mb-md-0">to 5Head Dashboard for <strong><asp:Label ID="lbl_Profile" runat="server" Text="staff"></asp:Label></strong>.</p>
                        </div>
                        <div class="d-flex">
                            <i class="mdi mdi-home text-muted hover-cursor"></i>
                            <p class="text-muted mb-0 hover-cursor">&nbsp;/&nbsp;Dashboard&nbsp;</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 stretch-card">
                <div class="card">
                    <div class="card-body">
                        <p class="card-title"></p>
                        <div class="text-center">
                            <img class="img-fluid px-3 px-sm-4 mt-3 mb-4" style="width: 50rem;" src="images/undraw_special_event.svg" alt="...">
                        </div>
                        <div class="row justify-content-center">
                            <div class="col-11 message-body">
                                <asp:PlaceHolder ID="PlaceHolder_Staff" runat="server" Visible="false">
                                    This is the dashboard for <strong>Restaurant Staffs</strong> to facilitate the managing and fulfilling of orders.
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="PlaceHolder_Manager" runat="server" Visible="false">
                                    This is the dashboard for <strong>Restaurant Managers</strong> to create/modify menu, prices and coupons.
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="PlaceHolder_Owner" runat="server" Visible="false">
                                    This is the dashboard for <strong>Restaurant Owners</strong> to view and organise customers' data to improve and grow the business.
                                </asp:PlaceHolder>
                            </div>
                        </div>
                        <br />
                        <br />
                        <span class="float-right" id="lbl_Today"></span>
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
