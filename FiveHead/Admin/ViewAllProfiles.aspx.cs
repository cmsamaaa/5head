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
            DataTable dt = ds.Tables[0];
            dt.Columns.Add("suspend", typeof(string));
            dt.Columns.Add("message", typeof(string));
            dt.Columns.Add("css", typeof(string));
            dt.Columns.Add("editVisible", typeof(Boolean));

            foreach (DataRow dr in dt.Rows)
            {
                bool deactivated = Convert.ToBoolean(dr["deactivated"]);
                if (!deactivated)
                {
                    dr["suspend"] = "Suspend";
                    dr["message"] = "return confirm('Are you sure you want to suspend the profile?')";
                    dr["css"] = "btn btn-danger";
                    dr["editVisible"] = true;
                }
                else
                {
                    dr["suspend"] = "Re-activate";
                    dr["message"] = "return confirm('Are you sure you want to re-activate the profile?')";
                    dr["css"] = "btn btn-success";
                    dr["editVisible"] = false;
                }
            }

            gv_Profiles.DataSource = ds;
            gv_Profiles.DataBind();
        }

        protected void gv_Profiles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindGridView();
            gv_Profiles.PageIndex = e.NewPageIndex;
            gv_Profiles.DataBind();
        }

        protected void gv_Profiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument) % gv_Profiles.PageSize;

            try
            {
                GridViewRow row = gv_Profiles.Rows[index];
            }
            catch (Exception)
            {
                index = 0;
            }

            Label profileIDLabel = (Label)gv_Profiles.Rows[index].FindControl("lbl_ProfileID");
            int profileID = Convert.ToInt32(profileIDLabel.Text);

            int result = 0;
            switch (e.CommandName)
            {
                case "Edit":
                    Session["edit_ProfileID"] = profileID;
                    Response.Redirect("EditProfile.aspx", true);
                    break;
                case "Suspend":
                    result = profilesController.SuspendProfile(profileID);
                    if (result == 1)
                        Response.Redirect("ViewAllProfiles.aspx?suspend=true", true);
                    else
                        Response.Redirect("ViewAllProfiles.aspx?suspend=false", true);
                    break;
                case "Re-activate":
                    result = profilesController.ReactivateProfile(profileID);
                    if (result == 1)
                        Response.Redirect("ViewAllProfiles.aspx?activate=true", true);
                    else
                        Response.Redirect("ViewAllProfiles.aspx?activate=false", true);
                    break;
                default:
                    break;
            }
        }
    }
}