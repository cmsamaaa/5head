using FiveHead.Controller;
using System;

namespace FiveHead.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        private string sessionQuery;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check for admin session
                try
                {
                    sessionQuery = Session["adminSession"].ToString();

                    if (!String.IsNullOrEmpty(sessionQuery))
                        Response.Redirect("Dashboard.aspx", true);
                }
                catch (Exception)
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

            AccountsController account = new AccountsController();
            result = account.Admin_Authentication(username, password);

            if (result == true)
            {
                Session["adminSession"] = username;
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