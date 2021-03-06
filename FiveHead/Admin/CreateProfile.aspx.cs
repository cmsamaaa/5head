using FiveHead.Controller;
using System;

namespace FiveHead.Admin
{
    public partial class CreateProfile : System.Web.UI.Page
    {
        ProfilesController profilesController;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btn_Create_Click(object sender, EventArgs e)
        {
            string profileName = tb_ProfileName.Value;

            profilesController = new ProfilesController();
            int result = profilesController.CreateProfile(profileName);

            if (result == 1)
                Response.Redirect("CreateProfile.aspx?create=true");
            else
                Response.Redirect("CreateProfile.aspx?create=false");
        }
    }
}