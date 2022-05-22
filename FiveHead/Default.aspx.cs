using System;

namespace FiveHead
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Redirect("Menu/Menu.aspx", true);
            }
        }
    }
}