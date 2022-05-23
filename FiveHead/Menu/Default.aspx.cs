using System;

namespace FiveHead.Menu
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Response.Redirect("Menu.aspx", true);
        }
    }
}