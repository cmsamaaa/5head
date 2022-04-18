using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FiveHead.Restaurant
{
    public partial class MasterPage_Restaurant : System.Web.UI.MasterPage
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
                        lbl_Username.Text = sessionQuery;
                }
                catch (Exception ex)
                {
                    Response.Redirect("Login.aspx?sessionExpired=true", true);
                }
            }
        }
    }
}