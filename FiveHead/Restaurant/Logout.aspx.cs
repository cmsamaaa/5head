using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FiveHead.Restaurant
{
    public partial class Logout : System.Web.UI.Page
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
                    {
                        Session["staffSession"] = null;
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