using System;

namespace FiveHead.Admin
{
    public partial class Logout : System.Web.UI.Page
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
                    {
                        Session["adminSession"] = null;
                        Response.Redirect("Login.aspx?logout=true", true);
                    }
                }
                catch (Exception)
                {
                    Response.Redirect("Login.aspx", true);
                }
            }
        }
    }
}