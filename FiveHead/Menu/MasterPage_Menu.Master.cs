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

                if (Session["tableNo"] == null)
                    Response.Redirect("EnterTable.aspx", true);
            }
        }
    }
}