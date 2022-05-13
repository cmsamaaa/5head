using System;

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

                    if (!string.IsNullOrEmpty(sessionQuery))
                        lbl_Username.Text = sessionQuery;

                    InitComponents(Session["staffProfile"].ToString());
                }
                catch (Exception)
                {
                    Response.Redirect("Login.aspx?sessionExpired=true", true);
                }
            }
        }

        private void InitComponents(string profileName)
        {
            switch(profileName)
            {
                case "Restaurant Staff":
                    PlaceHolder_Nav_Staff.Visible = true;
                    break;
                case "Restaurant Manager":
                    PlaceHolder_Nav_Manager.Visible = true;
                    break;
                case "Restaurant Owner":
                    PlaceHolder_Nav_Owner.Visible = true;
                    break;
                default:
                    break;
            }
        }
    }
}