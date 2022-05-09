using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FiveHead.Admin
{
    public partial class Dashboard : System.Web.UI.Page
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