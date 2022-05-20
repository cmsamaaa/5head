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
                        InitComponents(staffProfile);

                }
                catch (Exception)
                {
                    Response.Redirect("Login.aspx?sessionExpired=true", true);
                }
            }
        }

        private void InitComponents(string profileName)
        {
            lbl_Profile.Text = String.Format("{0}s", staffProfile);

            switch (profileName)
            {
                case "Restaurant Staff":
                    PlaceHolder_Staff.Visible = true;
                    break;
                case "Restaurant Manager":
                    PlaceHolder_Manager.Visible = true;
                    break;
                case "Restaurant Owner":
                    PlaceHolder_Owner.Visible = true;
                    break;
                default:
                    break;
            }
        }
    }
}