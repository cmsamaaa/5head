using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FiveHead.BLL;

namespace FiveHead.Restaurant
{
    public partial class Login : System.Web.UI.Page
    {

        private string sessionQuery;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check for staff session
                try
                {
                    sessionQuery = Session["staffSession"].ToString();

                    if (!String.IsNullOrEmpty(sessionQuery))
                        Response.Redirect("Dashboard.aspx", true);
                }
                catch (Exception ex)
                {

                }
            }
        }

        protected void btn_Login_Click(object sender, EventArgs e)
        {
            string username, password;
            bool result;

            username = tb_Username.Value.Trim();
            password = tb_Password.Value.Trim();

            StaffsBLL staff = new StaffsBLL();
            result = staff.Admin_Authentication(username, password);

            if (result == true)
            {
                Session["staffSession"] = username;
                Response.Redirect("Dashboard.aspx", true);
            }
            else
            {
                ShowMessage("Fail to login!");
            }
        }

        private void ShowMessage(string Message)
        {
            if (!ClientScript.IsClientScriptBlockRegistered("MyMessage"))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "MyMessage", "alert('" + Message + "');", true);
            }
        }
    }
}