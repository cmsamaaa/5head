using FiveHead.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FiveHead.Admin
{
    public partial class CreateProfile : System.Web.UI.Page
    {
        ProfilesBLL profilesBLL;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btn_Create_Click(object sender, EventArgs e)
        {
            string profileName = tb_ProfileName.Value;
            int permissionLevel = Convert.ToInt32(tb_PermissionLevel.Value);

            profilesBLL = new ProfilesBLL();
            int result = profilesBLL.CreateProfile(profileName, permissionLevel);

            if (result == 1)
                Response.Redirect("CreateProfile.aspx?create=true");
            else
                Response.Redirect("CreateProfile.aspx?create=false");
        }

        private void ShowMessage(string Message)
        {
            if (!ClientScript.IsClientScriptBlockRegistered("MyMessage"))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "MyMessage", "alert('" + Message + "');", true);
            }
        }
    }
}