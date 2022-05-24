<%@ Page Title="" Language="C#" MasterPageFile="~/Restaurant/MasterPage_Restaurant.Master" AutoEventWireup="true" CodeBehind="DataReport.aspx.cs" Inherits="FiveHead.Restaurant.DataReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        table tr:first-child {
            font-weight: 500;
            font-size: 1rem;
        }
        table tr:nth-child(even) {
            background: rgba(238, 238, 238, 0.57);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_PageContent" runat="server">
    <div class="content-wrapper">
        <div class="row">
            <div class="col-md-12 grid-margin">
                <div class="d-flex justify-content-between flex-wrap">
                    <div class="d-flex align-items-end flex-wrap">
                        <h2>Data Report</h2>
                    </div>
                    <div class="d-flex justify-content-between align-items-end flex-wrap">
                        <p class="mr-4">Filter By: </p>
                        <button runat="server" type="button" id="btn_FilterBehaviour" class="btn btn-light bg-white mr-3 d-flex align-items-center" onserverclick="btn_filter_Behaviour">
                            <i class="mdi mdi-gesture-tap text-muted mr-1"></i>Behaviour
                        </button>
                        <button runat="server" type="button" id="btn_FilterFrequency" class="btn btn-light bg-white mr-3 d-flex align-items-center" onserverclick="btn_filter_Frequency">
                            <i class="mdi mdi-clock-outline text-muted mr-1"></i><span>Frequency</span>
                        </button>
                        <button runat="server" type="button" id="btn_FilterPreferences" class="btn btn-light bg-white mr-3 d-flex align-items-center" onserverclick="btn_filter_Preferences">
                            <i class="mdi mdi-account-heart text-muted mr-1"></i>Preference
                        </button>
                        <button runat="server" type="button" id="btn_ResetFilter" class="btn btn-primary mr-3 d-flex align-items-center" onserverclick="btn_reset_Filter">
                            <i class="mdi mdi-refresh mr-1"></i>Show All
                        </button>
                    </div>
                </div>
            </div>
        </div>
        <div id="div_avg_spending" runat="server" class="row mb-2">
            <div class="col-md-12 stretch-card">
                <div class="card">
                    <div class="card-body">
                        <p class="card-title">General Average Spending Per Visit</p>
                        <div class="table-responsive">
                            <asp:Table ID="table_average_Spending" runat="server" CssClass="table table-bordered"></asp:Table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="div_freq_orders" runat="server" class="row mb-2">
            <div class="col-md-12 stretch-card">
                <div class="card">
                    <div class="card-body">
                        <p class="card-title">Frequency of Orders</p>
                        <div class="table-responsive">
                            <asp:Table ID="table_total_Orders" class="table table-bordered" runat="server"></asp:Table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="div_preferences" runat="server" class="row mb-2">
            <div class="col-md-12 stretch-card">
                <div class="card">
                    <div class="card-body">
                        <p class="card-title">Preferences</p>
                        <div class="table-responsive">
                            <asp:Table ID="table_Preferences" class="table table-bordered" runat="server"></asp:Table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_Scripts" runat="server">
</asp:Content>

