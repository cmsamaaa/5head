using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FiveHead.Restaurant
{
    public partial class Dashboard : System.Web.UI.Page
    {
        private string sessionQuery;
        private string staffProfile;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check for staff session
                try
                {
                    sessionQuery = Session["staffSession"].ToString();

                    staffProfile = Session["staffProfile"].ToString();
                    if (!string.IsNullOrEmpty(staffProfile))
                        lbl_Profile.Text = String.Format("{0}s", staffProfile);
                }
                catch (Exception ex)
                {
                    Response.Redirect("Login.aspx?sessionExpired=true", true);
                }
            }
        }
    }
}