using System;
using System.Web;

namespace FiveHead.Menu
{
    public partial class MasterPage_Menu : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string path = HttpContext.Current.Request.Url.AbsolutePath;
                if (path.Equals("/Menu/EnterTable.aspx"))
                {
                    ContentPlaceHolder_Navbar.Visible = false;
                    return;
                }

                if (path.Equals("/Menu/Menu.aspx"))
                {
                    nav_menu.Attributes["class"] = "active";
                    nav_menu_mobile.Attributes["class"] = "active";
                }

                if (path.Equals("/Menu/Cart.aspx"))
                {
                    nav_cart.Attributes["class"] = "active";
                    nav_cart_mobile.Attributes["class"] = "active";
                }

                if (Session["tableNo"] == null)
                    Response.Redirect("EnterTable.aspx", true);
            }
        }
    }
}