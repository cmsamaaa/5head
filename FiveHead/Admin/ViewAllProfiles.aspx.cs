using FiveHead.Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace FiveHead.Admin
{
    public partial class ViewAllProfiles : System.Web.UI.Page
    {
        ProfilesController profilesController = new ProfilesController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindGridView();
            }
        }

        private void bindGridView()
        {
            DataSet ds = profilesController.GetAllProfilesDataSet();
            gv_Profiles.DataSource = ds;
            gv_Profiles.DataBind();
        }

        protected void gv_Accounts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindGridView();
            gv_Profiles.PageIndex = e.NewPageIndex;
            gv_Profiles.DataBind();
        }
    }
}