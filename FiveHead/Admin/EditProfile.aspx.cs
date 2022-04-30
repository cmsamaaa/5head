using FiveHead.Controller;
using System;

namespace FiveHead.Admin
{
    public partial class EditProfile : System.Web.UI.Page
    {
        ProfilesController profilesController = new ProfilesController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetInfo();
            }
        }

        private void GetInfo()
        {
            int profileID = Convert.ToInt32(Session["edit_ProfileID"]);
            tb_ProfileName.Value = profilesController.GetProfileNameByID(profileID);
        }

        protected void btn_Update_Click(object sender, EventArgs e)
        {
            HideAllPlaceHolders();
            int profileID = Convert.ToInt32(Session["edit_ProfileID"]);
            string profileName = tb_ProfileName.Value;

            if(!string.IsNullOrEmpty(profileName))
            {
                int result = profilesController.UpdateProfileName(profileID, tb_ProfileName.Value);
                if (result == 1)
                    PlaceHolder_Success_ProfileName.Visible = true;
                else
                    PlaceHolder_Error_ProfileName.Visible = false;
            }
        }

        private void HideAllPlaceHolders()
        {
            PlaceHolder_Error_ProfileName.Visible = false;
            PlaceHolder_Success_ProfileName.Visible = false;
        }
    }
}