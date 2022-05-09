using System;

namespace FiveHead.Admin
{
    public partial class MasterPage_Admin : System.Web.UI.MasterPage
    {
        private string sessionQuery;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                // Check for admin session
                try
                {
                    sessionQuery = Session["adminSession"].ToString();

                    if (!string.IsNullOrEmpty(sessionQuery))
                        lbl_Username.Text = sessionQuery;
                }
                catch (Exception)
                {
                    Response.Redirect("Login.aspx?sessionExpired=true", true);
                }
            }
        }
    }
}