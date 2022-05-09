using FiveHead.Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace FiveHead.Admin
{
    public partial class ViewAllAccounts : System.Web.UI.Page
    {
        AccountsController accountsController = new AccountsController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                bindGridView();
            }
        }

        private void bindGridView()
        {
            DataSet ds = accountsController.Admin_GetAllAccounts();
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
                    dr["message"] = "return confirm('Are you sure you want to suspend the account?')";
                    dr["css"] = "btn btn-danger";
                    dr["editVisible"] = true;
                }
                else
                {
                    dr["suspend"] = "Re-activate";
                    dr["message"] = "return confirm('Are you sure you want to re-activate the account?')";
                    dr["css"] = "btn btn-success";
                    dr["editVisible"] = false;
                }
            }

            gv_Accounts.DataSource = ds;
            gv_Accounts.DataBind();
        }

        protected void gv_Accounts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindGridView();
            gv_Accounts.PageIndex = e.NewPageIndex;
            gv_Accounts.DataBind();
        }

        protected void gv_Accounts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument) % gv_Accounts.PageSize;

            try
            {
                GridViewRow row = gv_Accounts.Rows[index];
            }
            catch (Exception)
            {
                index = 0;
            }

            Label accountIDLabel = (Label)gv_Accounts.Rows[index].FindControl("lbl_AccountID");
            int accountID = Convert.ToInt32(accountIDLabel.Text);

            Label profileNameLabel = (Label)gv_Accounts.Rows[index].FindControl("lbl_ProfileName");
            string profileName = profileNameLabel.Text;

            int result = 0;
            switch (e.CommandName)
            {
                case "Edit":
                    Session["edit_AccountID"] = accountID;
                    Session["edit_ProfileName"] = profileName;
                    Response.Redirect("EditAccount.aspx", true);
                    break;
                case "Suspend":
                    result = accountsController.SuspendAccount(accountID);
                    if (result == 1)
                        Response.Redirect("ViewAllAccounts.aspx?suspend=true", true);
                    else
                        Response.Redirect("ViewAllAccounts.aspx?suspend=false", true);
                    break;
                case "Re-activate":
                    result = accountsController.ReactivateAccount(accountID);
                    if (result == 1)
                        Response.Redirect("ViewAllAccounts.aspx?activate=true", true);
                    else
                        Response.Redirect("ViewAllAccounts.aspx?activate=false", true);
                    break;
                default:
                    break;
            }
        }
    }
}