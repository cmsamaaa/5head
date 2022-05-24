using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FiveHead.Controller;
using System.Text;

namespace FiveHead.Restaurant
{
    public partial class DataReport : System.Web.UI.Page
    {
        // Global Variables
        OrdersController ordersController = new OrdersController();         // For handling Orders (Customer)
        int state;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Check if page is first time loading
                // Will only execute the first time when the page loads
            }

            DisplayReport();
        }
        public DataReport()
        {
            /* Page state
             * 0 : View all
             * 1 : Filter Frequency
             * 2 : Filter Preferences
             * 3 : Filter Behaviour
             */
            state = 0;
        }

        /*
         * Event Handlers
         */
        protected void btn_reset_Filter(Object sender, EventArgs e)
        {
            /*
             * Show All
             */
            state = 0;
            DisplayReport();
        }
        protected void btn_filter_Frequency(Object sender, EventArgs e)
        {
            /*
             * Display Frequency
             */
            // DisplayFrequencyOfOrders();
            state = 1;
            DisplayReport();
        }
        protected void btn_filter_Preferences(Object sender, EventArgs e)
        {
            /*
             * Display Preferences
             */
            // DisplayPreferences();
            state = 2;
            DisplayReport();
        }
        protected void btn_filter_Behaviour(Object sender, EventArgs e)
        {
            /*
             * Display Behaviour
             */
            // DisplayAvgSpending();
            state = 3;
            DisplayReport();
        }

        /*
         * Helper Functions
         */
        public void DisplayAvgSpending()
        {
            /*
             * Display Report for Average Spendings per Visit 
             */
            // Initialize Variables
            List<List<String>> avg_spending = new List<List<String>>(); // Average Spending

            // Tables
            Table tbl_avgSpending = table_average_Spending;     // Table for average spending

            // Get all contacts
            List<String> all_contacts = ordersController.GetAllContacts();

            /*
             * Reset and Clear
             */
            tbl_avgSpending.Rows.Clear();

            /*
             * Average Spending
             */
            // Get all contact's average spending
            for (int i = 0; i < all_contacts.Count; i++)
            {
                // Local loop list
                List<String> contact_map = new List<string>();

                // Get Current Contact
                string curr_contact = all_contacts[i];

                // Calculate current contact's total spending
                double totalSpending = ordersController.CalculateTotalSpend(curr_contact);

                // Calculate current contact's average spending
                double avgSpending = ordersController.CalculateAverageSpend(curr_contact);

                // Get Number of Visits
                int number_of_visits = ordersController.GetNumberOfVisitsByContacts(curr_contact);

                // Map Average spending to current contact
                contact_map.Add((i + 1).ToString());                          // Row number
                contact_map.Add(curr_contact);                              // Contact
                contact_map.Add(number_of_visits.ToString());               // Total Visits
                contact_map.Add(Math.Round(totalSpending, 2).ToString());   // Total Spending
                contact_map.Add(Math.Round(avgSpending, 2).ToString());     // Average Spending

                // Add Local list to Average Spending 
                avg_spending.Add(contact_map);
            }

            // Display Average Spending
            /*
             * Headers 
             */
            // Create Table Header
            List<String> HeaderText = new List<String>() { "S/N", "Contact", "Total Visits", "Total Spending", "Average Overall Spending" };

            // Create new Table Header Row
            TableHeaderRow header = new TableHeaderRow();
            for (int i = 0; i < HeaderText.Count; i++)
            {
                // Create new cell
                TableCell curr_header = new TableCell();

                // Populate Cell
                curr_header.Text = HeaderText[i];

                // Add Cell to Header Row
                header.Cells.Add(curr_header);
            }

            // Add Header Row to Table
            tbl_avgSpending.Rows.Add(header);

            for (int i = 0; i < avg_spending.Count; i++)
            {
                TableRow tr = new TableRow();
                List<String> curr_row = avg_spending[i];

                for (int j = 0; j < curr_row.Count; j++)
                {
                    /*
                     * Get Cells in current row
                     */
                    TableCell new_cell = new TableCell();

                    //table_MenuItems.Text += " | " + curr_row[j].ToString().Replace(Environment.NewLine, "<br/>");
                    new_cell.Text = curr_row[j];

                    // Add Cell to row
                    tr.Cells.Add(new_cell);
                }

                // Append Rows to table
                tbl_avgSpending.Rows.Add(tr);
            }
        }
        public void DisplayTotalOrders()
        {
            /*
             * Display Daily Orders
             */

            // Initialize Variables
            List<List<String>> frequency_of_Orders_Total = new List<List<String>>(); // Average Spending (Daily)

            // Create Table Header
            List<String> HeaderText = new List<String>();

            // Tables
            Table tbl_orderFreq_Total = table_total_Orders;       // Table for Frequency of Orders (Daily)

            // Get all contacts
            List<String> all_contacts = ordersController.GetAllContacts();

            /*
             * Reset and Clear
             */
            tbl_orderFreq_Total.Rows.Clear();

            /*
             * Frequency of Orders - Total
             */
            // Get all contact's frequency of orders
            for (int i = 0; i < all_contacts.Count; i++)
            {
                // Local loop list
                List<String> contact_map = new List<string>();
                double daily_avg_spendings = 0.0;
                double weekly_avg_spendings = 0.0;
                double monthly_avg_spendings = 0.0;

                // Get Current Contact
                string curr_contact = all_contacts[i];

                // Get Number of Visits
                int number_of_visits = ordersController.GetNumberOfVisitsByContacts(curr_contact);

                // Calculate Daily Average Spending
                List<List<String>> daily_avg_spendings_report = ordersController.GetDailyAverageSpend(curr_contact);
                for (int j = 0; j < daily_avg_spendings_report.Count; j++)
                {
                    List<String> curr_row = daily_avg_spendings_report[j];

                    String curr_start_Day = curr_row[1];
                    String curr_end_Day = curr_row[2];
                    String curr_avg_Spending = curr_row[3];

                    daily_avg_spendings = double.Parse(curr_avg_Spending);
                }

                // Calculate Weekly Average Spending
                List<List<String>> weekly_avg_spendings_report = ordersController.GetWeeklyAverageSpend(curr_contact);
                for (int j = 0; j < weekly_avg_spendings_report.Count; j++)
                {
                    List<String> curr_row = weekly_avg_spendings_report[j];

                    String curr_start_Day = curr_row[1];
                    String curr_end_Day = curr_row[2];
                    String curr_avg_Spending = curr_row[3];

                    weekly_avg_spendings = double.Parse(curr_avg_Spending);
                }

                // Calculate Monthly Average Spending
                List<List<String>> monthly_avg_spendings_report = ordersController.GetMonthlyAverageSpend(curr_contact);
                for (int j = 0; j < monthly_avg_spendings_report.Count; j++)
                {
                    List<String> curr_row = monthly_avg_spendings_report[j];

                    String curr_start_Day = curr_row[1];
                    String curr_end_Day = curr_row[2];
                    String curr_avg_Spending = curr_row[3];

                    monthly_avg_spendings = double.Parse(curr_avg_Spending);
                }

                // Calculate Last Visit
                int days_since_last_visit = ordersController.CalculateDaysSinceLastVisit(curr_contact);

                // Map Average spending to current contact
                contact_map.Add((i + 1).ToString());            // Row number
                contact_map.Add(curr_contact);                  // Contact
                contact_map.Add(number_of_visits.ToString());           // Number of Total Visits
                contact_map.Add(days_since_last_visit.ToString());       // Days since last visit
                contact_map.Add(Math.Round(daily_avg_spendings, 2).ToString());    // Daily Average Spending
                contact_map.Add(Math.Round(weekly_avg_spendings, 2).ToString());    // Weekly Average Spending
                contact_map.Add(Math.Round(monthly_avg_spendings, 2).ToString());   // Monthly Average Spending

                // Add Local list to Average Spending 
                frequency_of_Orders_Total.Add(contact_map);
            }


            /*
             * Headers 
             */

            // Define Header Rows
            HeaderText = new List<String>() { "S/N", "Contact", "Total Visits", "Number of Days since last visit", "Daily Average Spendings", "Weekly Average Spendings", "Monthly Average Spendings" };

            // Create new Table Header Row
            TableHeaderRow header_Daily = new TableHeaderRow();
            for (int i = 0; i < HeaderText.Count; i++)
            {
                // Create new cell
                TableCell curr_header = new TableCell();

                // Populate Cell
                curr_header.Text = HeaderText[i];

                // Add Cell to Header Row
                header_Daily.Cells.Add(curr_header);
            }

            // Add Header Row to Table
            tbl_orderFreq_Total.Rows.Add(header_Daily);

            for (int i = 0; i < frequency_of_Orders_Total.Count; i++)
            {
                TableRow tr = new TableRow();
                List<String> curr_row = frequency_of_Orders_Total[i];

                for (int j = 0; j < curr_row.Count; j++)
                {
                    /*
                     * Get Cells in current row
                     */
                    TableCell new_cell = new TableCell();

                    //table_MenuItems.Text += " | " + curr_row[j].ToString().Replace(Environment.NewLine, "<br/>");
                    new_cell.Text = curr_row[j];

                    // Add Cell to row
                    tr.Cells.Add(new_cell);
                }

                // Append Rows to table
                tbl_orderFreq_Total.Rows.Add(tr);
            }
        }
        public void DisplayDailyOrders()
        {
            /*
             * Display Daily Orders
             */

            // Initialize Variables
            List<List<String>> frequency_of_Orders_Daily = new List<List<String>>(); // Average Spending (Daily)

            // Create Table Header
            List<String> HeaderText = new List<String>();

            // Tables
            Table tbl_orderFreq_Daily = new Table();       // Table for Frequency of Orders (Daily)

            // Get all contacts
            List<String> all_contacts = ordersController.GetAllContacts();

            /*
             * Reset and Clear
             */
            tbl_orderFreq_Daily.Rows.Clear();

            /*
             * Frequency of Orders - Daily
             */
            // Get all contact's frequency of orders
            for (int i = 0; i < all_contacts.Count; i++)
            {
                // Local loop list
                List<String> contact_map = new List<string>();
                double daily_avg_spendings = 0.0;

                // Get Current Contact
                string curr_contact = all_contacts[i];

                // Get Number of Visits
                int number_of_visits = ordersController.GetNumberOfVisitsByContacts(curr_contact);

                // Calculate Daily Visits
                List<List<String>> daily_avg_spendings_report = ordersController.GetDailyAverageSpend(curr_contact);
                for (int j = 0; j < daily_avg_spendings_report.Count; j++)
                {
                    List<String> curr_row = daily_avg_spendings_report[j];

                    String curr_start_Day = curr_row[1];
                    String curr_end_Day = curr_row[2];
                    String curr_avg_Spending = curr_row[3];

                    daily_avg_spendings = double.Parse(curr_avg_Spending);
                }

                // Calculate Last Visit
                int days_since_last_visit = ordersController.CalculateDaysSinceLastVisit(curr_contact);


                // Map Average spending to current contact
                contact_map.Add((i + 1).ToString());            // Row number
                contact_map.Add(curr_contact);                  // Contact
                contact_map.Add(number_of_visits.ToString());   // Number of Total Visits
                contact_map.Add(days_since_last_visit.ToString());       // Number of Daily Visits
                contact_map.Add(Math.Round(daily_avg_spendings, 2).ToString());    // Daily Average Spending

                // Add Local list to Average Spending 
                frequency_of_Orders_Daily.Add(contact_map);
            }


            /*
             * Headers 
             */

            // Define Header Rows
            HeaderText = new List<String>() { "S/N", "Contact", "Total Visits", "Number of Days since last visit", "Daily Average Spendings" };

            // Create new Table Header Row
            TableHeaderRow header_Daily = new TableHeaderRow();
            for (int i = 0; i < HeaderText.Count; i++)
            {
                // Create new cell
                TableCell curr_header = new TableCell();

                // Populate Cell
                curr_header.Text = HeaderText[i];

                // Add Cell to Header Row
                header_Daily.Cells.Add(curr_header);
            }

            // Add Header Row to Table
            tbl_orderFreq_Daily.Rows.Add(header_Daily);

            for (int i = 0; i < frequency_of_Orders_Daily.Count; i++)
            {
                TableRow tr = new TableRow();
                List<String> curr_row = frequency_of_Orders_Daily[i];

                for (int j = 0; j < curr_row.Count; j++)
                {
                    /*
                     * Get Cells in current row
                     */
                    TableCell new_cell = new TableCell();

                    //table_MenuItems.Text += " | " + curr_row[j].ToString().Replace(Environment.NewLine, "<br/>");
                    new_cell.Text = curr_row[j];

                    // Add Cell to row
                    tr.Cells.Add(new_cell);
                }

                // Append Rows to table
                tbl_orderFreq_Daily.Rows.Add(tr);
            }
        }
        public void DisplayWeeklyOrders()
        {
            /*
             * Display Weekly Orders
             */

            // Initialize Variables
            List<List<String>> frequency_of_Orders_Weekly = new List<List<String>>(); // Average Spending (Weekly)

            // Create Table Header
            List<String> HeaderText = new List<String>();

            // Tables
            Table tbl_orderFreq_Weekly = new Table();       // Table for Frequency of Orders (Weekly)

            // Get all contacts
            List<String> all_contacts = ordersController.GetAllContacts();

            /*
             * Reset and Clear
             */
            tbl_orderFreq_Weekly.Rows.Clear();

            /*
             * Frequency of Orders - Weekly
             */
            // Get all contact's frequency of orders
            for (int i = 0; i < all_contacts.Count; i++)
            {
                // Local loop list
                List<String> contact_map = new List<string>();
                int weekly_visits = 0;
                double weekly_avg_spendings = 0.0;

                // Get Current Contact
                string curr_contact = all_contacts[i];

                // Get Number of Visits
                int number_of_visits = ordersController.GetNumberOfVisitsByContacts(curr_contact);

                // Calculate Weekly Visits
                List<List<String>> weekly_avg_spendings_report = ordersController.GetWeeklyAverageSpend(curr_contact);
                for (int j = 0; j < weekly_avg_spendings_report.Count; j++)
                {
                    List<String> curr_row = weekly_avg_spendings_report[j];

                    String curr_start_Week = curr_row[1];
                    String curr_end_Week = curr_row[2];
                    String curr_avg_Spending = curr_row[3];

                    weekly_avg_spendings = double.Parse(curr_avg_Spending);
                }
                weekly_visits = ordersController.CalculateWeeklyVisits(curr_contact);

                // Map Average spending to current contact
                contact_map.Add((i + 1).ToString());            // Row number
                contact_map.Add(curr_contact);                  // Contact
                contact_map.Add(number_of_visits.ToString());   // Number of Total Visits
                contact_map.Add(weekly_visits.ToString());      // Number of Weekly Visits
                contact_map.Add(Math.Round(weekly_avg_spendings, 2).ToString());    // Daily Average Spending

                // Add Local list to Average Spending 
                frequency_of_Orders_Weekly.Add(contact_map);
            }


            /*
             * Headers 
             */

            // Define Header Rows
            HeaderText = new List<String>() { "S/N", "Contact", "Total Visits", "Weekly Visits", "Weekly Average Spendings" };

            // Create new Table Header Row
            TableHeaderRow header_Weekly = new TableHeaderRow();
            for (int i = 0; i < HeaderText.Count; i++)
            {
                // Create new cell
                TableCell curr_header = new TableCell();

                // Populate Cell
                curr_header.Text = HeaderText[i];

                // Add Cell to Header Row
                header_Weekly.Cells.Add(curr_header);
            }

            // Add Header Row to Table
            tbl_orderFreq_Weekly.Rows.Add(header_Weekly);

            for (int i = 0; i < frequency_of_Orders_Weekly.Count; i++)
            {
                TableRow tr = new TableRow();
                List<String> curr_row = frequency_of_Orders_Weekly[i];

                for (int j = 0; j < curr_row.Count; j++)
                {
                    /*
                     * Get Cells in current row
                     */
                    TableCell new_cell = new TableCell();

                    //table_MenuItems.Text += " | " + curr_row[j].ToString().Replace(Environment.NewLine, "<br/>");
                    new_cell.Text = curr_row[j];

                    // Add Cell to row
                    tr.Cells.Add(new_cell);
                }

                // Append Rows to table
                tbl_orderFreq_Weekly.Rows.Add(tr);
            }
        }
        public void DisplayMonthlyOrders()
        {
            /*
             * Display Monthly Orders
             */

            // Initialize Variables
            List<List<String>> frequency_of_Orders_Monthly = new List<List<String>>(); // Average Spending (Monthly)

            // Create Table Header
            List<String> HeaderText = new List<String>();

            // Tables
            Table tbl_orderFreq_Monthly = new Table();       // Table for Frequency of Orders (Monthly)

            // Get all contacts
            List<String> all_contacts = ordersController.GetAllContacts();

            /*
             * Reset and Clear
             */
            tbl_orderFreq_Monthly.Rows.Clear();

            /*
             * Frequency of Orders - Monthly
             */
            // Get all contact's frequency of orders
            for (int i = 0; i < all_contacts.Count; i++)
            {
                // Local loop list
                List<String> contact_map = new List<string>();
                int monthly_visits = 0;
                double monthly_avg_spendings = 0.0;

                // Get Current Contact
                string curr_contact = all_contacts[i];

                // Get Number of Visits
                int number_of_visits = ordersController.GetNumberOfVisitsByContacts(curr_contact);

                // Calculate Monthly Visits
                List<List<String>> monthly_avg_spendings_report = ordersController.GetMonthlyAverageSpend(curr_contact);
                for (int j = 0; j < monthly_avg_spendings_report.Count; j++)
                {
                    List<String> curr_row = monthly_avg_spendings_report[j];

                    String curr_start_Month = curr_row[1];
                    String curr_end_Month = curr_row[2];
                    String curr_avg_Spending = curr_row[3];

                    monthly_avg_spendings = double.Parse(curr_avg_Spending);
                }
                monthly_visits = ordersController.CalculateMonthlyVisits(curr_contact);

                // Map Average spending to current contact
                contact_map.Add((i + 1).ToString());            // Row number
                contact_map.Add(curr_contact);                  // Contact
                contact_map.Add(number_of_visits.ToString());   // Number of Total Visits
                contact_map.Add(monthly_visits.ToString());     // Number of Monthly Visits
                contact_map.Add(Math.Round(monthly_avg_spendings, 2).ToString());    // Daily Average Spending

                // Add Local list to Average Spending 
                frequency_of_Orders_Monthly.Add(contact_map);
            }


            /*
             * Headers 
             */
            // Define Header Rows
            HeaderText = new List<String>() { "S/N", "Contact", "Total Visits", "Monthly Visits", "Monthly Average Spendings" };

            // Create new Table Header Row
            TableHeaderRow header_Monthly = new TableHeaderRow();
            for (int i = 0; i < HeaderText.Count; i++)
            {
                // Create new cell
                TableCell curr_header = new TableCell();

                // Populate Cell
                curr_header.Text = HeaderText[i];

                // Add Cell to Header Row
                header_Monthly.Cells.Add(curr_header);
            }

            // Add Header Row to Table
            tbl_orderFreq_Monthly.Rows.Add(header_Monthly);

            for (int i = 0; i < frequency_of_Orders_Monthly.Count; i++)
            {
                TableRow tr = new TableRow();
                List<String> curr_row = frequency_of_Orders_Monthly[i];

                for (int j = 0; j < curr_row.Count; j++)
                {
                    /*
                     * Get Cells in current row
                     */
                    TableCell new_cell = new TableCell();

                    //table_MenuItems.Text += " | " + curr_row[j].ToString().Replace(Environment.NewLine, "<br/>");
                    new_cell.Text = curr_row[j];

                    // Add Cell to row
                    tr.Cells.Add(new_cell);
                }

                // Append Rows to table
                tbl_orderFreq_Monthly.Rows.Add(tr);
            }
        }

        public void DisplayFrequencyOfOrders()
        {
            /*
             * Display Report Table for Frequency of Orders
             * 
             * - Total
             * - Daily
             * - Monthly
             * - Yearly
             */

            /*
             * Frequency of Orders - Total
             */
            DisplayTotalOrders();

            //=================================================================================================================

            /*
             * Frequency of Orders - Daily
             */
            DisplayDailyOrders();

            //=================================================================================================================

            /*
             * Frequency of Orders - Weekly
             */
            DisplayWeeklyOrders();

            //=================================================================================================================

            /*
             * Frequency of Orders - Monthly
             */
            DisplayMonthlyOrders();
        }

        public void DisplayPreferences()
        {
            /*
             * Check and display the customer's preferences between food/drink categories
             *  - Get the MAX() value of categoryID according to customer
             *  - To reference:
             *      + Category (Food/Drink)
             */

            // Initialize Variables
            List<List<String>> preferences = new List<List<String>>(); // Average Spending (Monthly)

            // Create Table Header
            List<String> HeaderText = new List<String>();

            // Tables
            Table tbl_Preferences = table_Preferences;       // Table for Frequency of Orders (Monthly)

            // Get all contacts
            List<String> all_contacts = ordersController.GetAllContacts();

            /*
             * Reset and Clear
             */
            tbl_Preferences.Rows.Clear();

            // Get Preferences and append to table
            for (int i = 0; i < all_contacts.Count; i++)
            {
                // Temporary Variable
                List<String> contact_map = new List<string>();

                // Get Current Contact
                string curr_contact = all_contacts[i];

                List<String> curr_preferences = ordersController.CalculatePreferences(curr_contact);
                int curr_pref_ID = int.Parse(curr_preferences[0]);
                string curr_pref_Name = curr_preferences[1];

                // Map Average spending to current contact
                contact_map.Add((i + 1).ToString());            // Row number
                contact_map.Add(curr_contact);                  // Contact
                contact_map.Add(curr_pref_ID.ToString());       // Preference Category ID
                contact_map.Add(curr_pref_Name.ToString());     // Preference Category Name

                // Add Local list to Average Spending 
                preferences.Add(contact_map);
            }

            /*
             * Headers 
             */
            // Define Header Rows
            HeaderText = new List<String>() { "S/N", "Contact", "Category ID", "Category Name" };

            // Create new Table Header Row
            TableHeaderRow header_Monthly = new TableHeaderRow();
            for (int i = 0; i < HeaderText.Count; i++)
            {
                // Create new cell
                TableCell curr_header = new TableCell();

                // Populate Cell
                curr_header.Text = HeaderText[i];

                // Add Cell to Header Row
                header_Monthly.Cells.Add(curr_header);
            }

            // Add Header Row to Table
            tbl_Preferences.Rows.Add(header_Monthly);

            for (int i = 0; i < preferences.Count; i++)
            {
                TableRow tr = new TableRow();
                List<String> curr_row = preferences[i];

                for (int j = 0; j < curr_row.Count; j++)
                {
                    /*
                     * Get Cells in current row
                     */
                    TableCell new_cell = new TableCell();

                    //table_MenuItems.Text += " | " + curr_row[j].ToString().Replace(Environment.NewLine, "<br/>");
                    new_cell.Text = curr_row[j];

                    // Add Cell to row
                    tr.Cells.Add(new_cell);
                }

                // Append Rows to table
                tbl_Preferences.Rows.Add(tr);
            }
        }

        public void DisplayReport()
        {
            /*
             * Display Report for Restaurant Owner
             * :: Requirements
             *  - Frequency of Orders
             *  - Customer Preferences and Behaviours
             *      - average spend per visit
             *      - frequency/patterns of visits (or how long since they visited)
             *      - preferences for dishes or drinks
             */

            /*
             * Initialize Variables
             */

            // Lists
            List<List<String>> report_data = new List<List<String>>(); // Full dataset List container of type List<String> to hold all rows to the report
            List<List<String>> preferences = new List<List<String>>();  // Preference for Dishes or Drinks

            /* Page state
             * 0 : View all
             * 1 : Filter Frequency
             * 2 : Filter Preferences
             * 3 : Filter Behaviour
             */
            switch (state)
            {
                case 0:
                    // Show all
                    div_avg_spending.Visible = true;
                    div_preferences.Visible = true;
                    div_freq_orders.Visible = true;

                    /*
                     * Average Spending
                     */
                    DisplayAvgSpending();

                    /*
                     * Frequency of Orders
                     */
                    DisplayFrequencyOfOrders();

                    /*
                     * Preferences
                     */
                    DisplayPreferences();
                    break;
                case 1:
                    /*
                     * Frequency of Orders
                     */
                    // Hide 
                    div_avg_spending.Visible = false;
                    div_preferences.Visible = false;

                    // Show
                    div_freq_orders.Visible = true;

                    DisplayFrequencyOfOrders();
                    break;
                case 2:
                    /*
                     * Preferences
                     */
                    // Hide 
                    div_avg_spending.Visible = false;
                    div_freq_orders.Visible = false;

                    // Show
                    div_preferences.Visible = true;

                    DisplayPreferences();
                    break;
                case 3:
                    /*
                     * Average Spending
                     */
                    // Hide 
                    div_freq_orders.Visible = false;
                    div_preferences.Visible = false;

                    // Show
                    div_avg_spending.Visible = true;

                    DisplayAvgSpending();
                    break;
                default:
                    break;
            }
        }
    }
}