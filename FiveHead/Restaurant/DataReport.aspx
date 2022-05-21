﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Restaurant/MasterPage_Restaurant.Master" AutoEventWireup="true" CodeBehind="DataReport.aspx.cs" Inherits="FiveHead.Restaurant.DataReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_PageContent" runat="server">
    <div class="content-wrapper">
        <div class="row">
            <div class="col-md-12 grid-margin stretch-card">
                <div class="card">
                    <div class="card-body dashboard-tabs p-0">
                        <ul class="nav nav-tabs px-4" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" id="overview-tab" data-toggle="tab" href="#overview" role="tab" aria-controls="overview" aria-selected="true">Overview</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" id="sales-tab" data-toggle="tab" href="#sales" role="tab" aria-controls="sales" aria-selected="false">Sales</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" id="purchases-tab" data-toggle="tab" href="#purchases" role="tab" aria-controls="purchases" aria-selected="false">Purchases</a>
                            </li>
                        </ul>
                        <div class="tab-content py-0 px-0">
                            <div class="tab-pane fade show active" id="overview" role="tabpanel" aria-labelledby="overview-tab">
                                <div class="d-flex flex-wrap justify-content-xl-between">
                                    <div class="d-none d-xl-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                                        <i class="mdi mdi-calendar-heart icon-lg mr-3 text-primary"></i>
                                        <div class="d-flex flex-column justify-content-around">
                                            <small class="mb-1 text-muted">Start date</small>
                                            <div class="dropdown">
                                                <a class="btn btn-secondary dropdown-toggle p-0 bg-transparent border-0 text-dark shadow-none font-weight-medium" href="#" role="button" id="dropdownMenuLinkA" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    <h5 class="mb-0 d-inline-block">26 Jul 2018</h5>
                                                </a>
                                                <div class="dropdown-menu" aria-labelledby="dropdownMenuLinkA">
                                                    <a class="dropdown-item" href="#">12 Aug 2018</a>
                                                    <a class="dropdown-item" href="#">22 Sep 2018</a>
                                                    <a class="dropdown-item" href="#">21 Oct 2018</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="d-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                                        <i class="mdi mdi-currency-usd mr-3 icon-lg text-danger"></i>
                                        <div class="d-flex flex-column justify-content-around">
                                            <small class="mb-1 text-muted">Revenue</small>
                                            <h5 class="mr-2 mb-0">$577545</h5>
                                        </div>
                                    </div>
                                    <div class="d-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                                        <i class="mdi mdi-eye mr-3 icon-lg text-success"></i>
                                        <div class="d-flex flex-column justify-content-around">
                                            <small class="mb-1 text-muted">Total views</small>
                                            <h5 class="mr-2 mb-0">9833550</h5>
                                        </div>
                                    </div>
                                    <div class="d-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                                        <i class="mdi mdi-download mr-3 icon-lg text-warning"></i>
                                        <div class="d-flex flex-column justify-content-around">
                                            <small class="mb-1 text-muted">Downloads</small>
                                            <h5 class="mr-2 mb-0">2233783</h5>
                                        </div>
                                    </div>
                                    <div class="d-flex py-3 border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                                        <i class="mdi mdi-flag mr-3 icon-lg text-danger"></i>
                                        <div class="d-flex flex-column justify-content-around">
                                            <small class="mb-1 text-muted">Flagged</small>
                                            <h5 class="mr-2 mb-0">3497843</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane fade" id="sales" role="tabpanel" aria-labelledby="sales-tab">
                                <div class="d-flex flex-wrap justify-content-xl-between">
                                    <div class="d-none d-xl-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                                        <i class="mdi mdi-calendar-heart icon-lg mr-3 text-primary"></i>
                                        <div class="d-flex flex-column justify-content-around">
                                            <small class="mb-1 text-muted">Start date</small>
                                            <div class="dropdown">
                                                <a class="btn btn-secondary dropdown-toggle p-0 bg-transparent border-0 text-dark shadow-none font-weight-medium" href="#" role="button" id="dropdownMenuLinkA" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    <h5 class="mb-0 d-inline-block">26 Jul 2018</h5>
                                                </a>
                                                <div class="dropdown-menu" aria-labelledby="dropdownMenuLinkA">
                                                    <a class="dropdown-item" href="#">12 Aug 2018</a>
                                                    <a class="dropdown-item" href="#">22 Sep 2018</a>
                                                    <a class="dropdown-item" href="#">21 Oct 2018</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="d-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                                        <i class="mdi mdi-download mr-3 icon-lg text-warning"></i>
                                        <div class="d-flex flex-column justify-content-around">
                                            <small class="mb-1 text-muted">Downloads</small>
                                            <h5 class="mr-2 mb-0">2233783</h5>
                                        </div>
                                    </div>
                                    <div class="d-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                                        <i class="mdi mdi-eye mr-3 icon-lg text-success"></i>
                                        <div class="d-flex flex-column justify-content-around">
                                            <small class="mb-1 text-muted">Total views</small>
                                            <h5 class="mr-2 mb-0">9833550</h5>
                                        </div>
                                    </div>
                                    <div class="d-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                                        <i class="mdi mdi-currency-usd mr-3 icon-lg text-danger"></i>
                                        <div class="d-flex flex-column justify-content-around">
                                            <small class="mb-1 text-muted">Revenue</small>
                                            <h5 class="mr-2 mb-0">$577545</h5>
                                        </div>
                                    </div>
                                    <div class="d-flex py-3 border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                                        <i class="mdi mdi-flag mr-3 icon-lg text-danger"></i>
                                        <div class="d-flex flex-column justify-content-around">
                                            <small class="mb-1 text-muted">Flagged</small>
                                            <h5 class="mr-2 mb-0">3497843</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane fade" id="purchases" role="tabpanel" aria-labelledby="purchases-tab">
                                <div class="d-flex flex-wrap justify-content-xl-between">
                                    <div class="d-none d-xl-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                                        <i class="mdi mdi-calendar-heart icon-lg mr-3 text-primary"></i>
                                        <div class="d-flex flex-column justify-content-around">
                                            <small class="mb-1 text-muted">Start date</small>
                                            <div class="dropdown">
                                                <a class="btn btn-secondary dropdown-toggle p-0 bg-transparent border-0 text-dark shadow-none font-weight-medium" href="#" role="button" id="dropdownMenuLinkA" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    <h5 class="mb-0 d-inline-block">26 Jul 2018</h5>
                                                </a>
                                                <div class="dropdown-menu" aria-labelledby="dropdownMenuLinkA">
                                                    <a class="dropdown-item" href="#">12 Aug 2018</a>
                                                    <a class="dropdown-item" href="#">22 Sep 2018</a>
                                                    <a class="dropdown-item" href="#">21 Oct 2018</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="d-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                                        <i class="mdi mdi-currency-usd mr-3 icon-lg text-danger"></i>
                                        <div class="d-flex flex-column justify-content-around">
                                            <small class="mb-1 text-muted">Revenue</small>
                                            <h5 class="mr-2 mb-0">$577545</h5>
                                        </div>
                                    </div>
                                    <div class="d-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                                        <i class="mdi mdi-eye mr-3 icon-lg text-success"></i>
                                        <div class="d-flex flex-column justify-content-around">
                                            <small class="mb-1 text-muted">Total views</small>
                                            <h5 class="mr-2 mb-0">9833550</h5>
                                        </div>
                                    </div>
                                    <div class="d-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                                        <i class="mdi mdi-download mr-3 icon-lg text-warning"></i>
                                        <div class="d-flex flex-column justify-content-around">
                                            <small class="mb-1 text-muted">Downloads</small>
                                            <h5 class="mr-2 mb-0">2233783</h5>
                                        </div>
                                    </div>
                                    <div class="d-flex py-3 border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                                        <i class="mdi mdi-flag mr-3 icon-lg text-danger"></i>
                                        <div class="d-flex flex-column justify-content-around">
                                            <small class="mb-1 text-muted">Flagged</small>
                                            <h5 class="mr-2 mb-0">3497843</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 stretch-card">
                <div class="card">
                    <div class="card-body">
                        <p class="card-title">Data Report Table</p>
                        <div class="table-responsive">
                            <table id="recent-purchases-listing" class="table">
                                <thead>
                                    <tr>
                                        <th>Name</th>
                                        <th>Status report</th>
                                        <th>Office</th>
                                        <th>Price</th>
                                        <th>Date</th>
                                        <th>Gross amount</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>Jeremy Ortega</td>
                                        <td>Levelled up</td>
                                        <td>Catalinaborough</td>
                                        <td>$790</td>
                                        <td>06 Jan 2018</td>
                                        <td>$2274253</td>
                                    </tr>
                                    <tr>
                                        <td>Alvin Fisher</td>
                                        <td>Ui design completed</td>
                                        <td>East Mayra</td>
                                        <td>$23230</td>
                                        <td>18 Jul 2018</td>
                                        <td>$83127</td>
                                    </tr>
                                    <tr>
                                        <td>Emily Cunningham</td>
                                        <td>support</td>
                                        <td>Makennaton</td>
                                        <td>$939</td>
                                        <td>16 Jul 2018</td>
                                        <td>$29177</td>
                                    </tr>
                                    <tr>
                                        <td>Minnie Farmer</td>
                                        <td>support</td>
                                        <td>Agustinaborough</td>
                                        <td>$30</td>
                                        <td>30 Apr 2018</td>
                                        <td>$44617</td>
                                    </tr>
                                    <tr>
                                        <td>Betty Hunt</td>
                                        <td>Ui design not completed</td>
                                        <td>Lake Sandrafort</td>
                                        <td>$571</td>
                                        <td>25 Jun 2018</td>
                                        <td>$78952</td>
                                    </tr>
                                    <tr>
                                        <td>Myrtie Lambert</td>
                                        <td>Ui design completed</td>
                                        <td>Cassinbury</td>
                                        <td>$36</td>
                                        <td>05 Nov 2018</td>
                                        <td>$36422</td>
                                    </tr>
                                    <tr>
                                        <td>Jacob Kennedy</td>
                                        <td>New project</td>
                                        <td>Cletaborough</td>
                                        <td>$314</td>
                                        <td>12 Jul 2018</td>
                                        <td>$34167</td>
                                    </tr>
                                    <tr>
                                        <td>Ernest Wade</td>
                                        <td>Levelled up</td>
                                        <td>West Fidelmouth</td>
                                        <td>$484</td>
                                        <td>08 Sep 2018</td>
                                        <td>$50862</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_Scripts" runat="server">
</asp:Content>
