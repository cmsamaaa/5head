using FiveHead.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FiveHead
{
    public partial class Login : System.Web.UI.Page
    {
        private string queryStr;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Check for registration success
                try
                {
                    queryStr = Request.QueryString["register"];

                    if (queryStr.Equals("true"))
                        ShowMessage("You have successfully registered!");
                }
                catch (Exception ex)
                {

                }

                //Check for login success
                try
                {
                    queryStr = Request.QueryString["login"];

                    if (queryStr.Equals("true"))
                        ShowMessage("Login successful!");
                    else if (queryStr.Equals("false"))
                        ShowMessage("Login failed! Please re-enter your credentials.");
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

            username = tb_Username.Text.Trim();
            password = tb_Password.Text.Trim();

            AccountsBLL account = new AccountsBLL();
            result = account.Authenticate(username, password);

            if (result == true)
            {
                Session["username"] = username;
                Response.Redirect("~/Login.aspx?login=true", true);
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