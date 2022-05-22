<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RestaurantOwner.aspx.cs" Inherits="FiveHead.Restaurant.RestaurantOwner" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Restaurant Owner Report</title>
    <style>
        #page-title {
            text-align: center;
            font-weight: bold;
        }

        .tables {
            border: 5px solid blue;
            margin: 2% 2% 2% 2%;
            background-position: center;
            text-align:center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Page Header -->
        <div id="body-header">
            <h1 id="page-title">Restaurant Owner's Report Page</h1>
        </div>

        <br />

        <div id="actions">
            <!-- Buttons Here -->
            <div id="actions_filter" runat="server">
                <p>
                    Filter By:
                    <input type="button" id="btn_FilterFrequency" value="Frequency" onserverclick="btn_filter_Frequency" runat="server" />
                    <input type="button" id="btn_FilterPreferences" value="Preferences" onserverclick="btn_filter_Preferences" runat="server" />
                    <input type="button" id="btn_FilterBehaviour" value="Behaviour" onserverclick="btn_filter_Behaviour" runat="server" />
                    <input type="button" id="btn_ResetFilter" value="Show All" onserverclick="btn_reset_Filter" runat="server" />
                </p>
                
            </div>
        </div>
        
        <br />

        <!-- Report Page -->
        <div id="restaurant_owner_report" runat="server">
            <div id="div_avg_spending" runat="server"> 
                <h2>General Average Spending Per Visit:</h2>
                <asp:Table id="table_average_Spending" class="tables" GridLines="Both" runat="server"></asp:Table>
            </div>

            <br />

            <div id="div_freq_orders" runat="server">
                <h2>Frequency of Orders:</h2>
                <br />

                <h3 style="margin: 2% 2% 2% 2%;">Daily:</h3>
                <asp:Table ID="table_frequency_Orders_Daily" class="tables" GridLines="Both" runat="server"></asp:Table>

                <br />

                <h3 style="margin: 2% 2% 2% 2%;">Weekly:</h3>
                <asp:Table ID="table_frequency_Orders_Weekly" class="tables" GridLines="Both" runat="server"></asp:Table>

                <br />

                <h3 style="margin: 2% 2% 2% 2%;">Monthly:</h3>
                <asp:Table ID="table_frequency_Orders_Monthly" class="tables" GridLines="Both" runat="server"></asp:Table>
            </div>

            <div id="div_preferences" runat="server">
                <h2>Preferences:</h2>
                <asp:Table ID="table_Preferences" class="tables" GridLines="Both" runat="server"></asp:Table>
            </div>

            <br />
        </div>

        <!-- Page Footer -->
        <div id="messages">
            <label id="lbl_Message"></label>
        </div>
    </form>
</body>
</html>
