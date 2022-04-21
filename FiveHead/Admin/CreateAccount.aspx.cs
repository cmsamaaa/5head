using FiveHead.BLL;
using System;
using System.Web.UI.WebControls;

namespace FiveHead.Admin
{
    public partial class CreateAccount : System.Web.UI.Page
    {
        AccountsBLL accountBLL;
        ProfilesBLL profilesBLL;
        StaffsBLL staffsBLL;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetUserProfiles();
            }
        }

        private void SetUserProfiles()
        {
            profilesBLL = new ProfilesBLL();
            profilesBLL.GetAllProfiles().ForEach(profile =>
            {
                if (!profile.ProfileName.Equals("Administrator"))
                    ddl_UserProfile.Items.Add(new ListItem(profile.ProfileName, profile.ProfileID.ToString()));
            });
        }

        protected void btn_Create_Click(object sender, EventArgs e)
        {
            HideAllPlaceHolders();

            switch (ddl_AccountType.Value)
            {
                case "customer":
                    PlaceHolder_TempMsg.Visible = true;
                    break;
                case "staff":
                    CreateStaffAccount();
                    break;
                case "administrator":
                    CreateAdminAccount();
                    break;
                default:
                    PlaceHolder_TempMsg.Visible = true;
                    break;
            }
                
        }

        private void CreateStaffAccount()
        {
            accountBLL = new AccountsBLL();
            staffsBLL = new StaffsBLL();

            string firstName = tb_FirstName.Value;
            string lastName = tb_LastName.Value;
            string username = tb_Username.Value;
            string password = tb_Password.Value;
            string repeatPassword = tb_RepeatPassword.Value;
            string strProfileID = ddl_UserProfile.Value;

            bool isEmptyField = String.IsNullOrEmpty(firstName) || String.IsNullOrEmpty(lastName) || String.IsNullOrEmpty(username) ||
                String.IsNullOrEmpty(password) || String.IsNullOrEmpty(repeatPassword) || String.IsNullOrEmpty(strProfileID);

            if (!isEmptyField)
            {
                int result = accountBLL.CreateAccount(username, password, Convert.ToInt32(strProfileID));
                if (result == 1)
                {
                    result = 0;
                    result = staffsBLL.CreateStaff(firstName, lastName, accountBLL.GetAccountIDByUsername(username));

                    if (result == 1)
                        Response.Redirect("CreateAccount.aspx?create=true", true);
                    else
                        PlaceHolder_Error_Staff.Visible = true;
                }
                else
                    PlaceHolder_Error_Account.Visible = true;
            }
            else
                PlaceHolder_EmptyFields.Visible = true;
        }

        private void CreateAdminAccount()
        {
            accountBLL = new AccountsBLL();

            string username = tb_Username.Value;
            string password = tb_Password.Value;
            string repeatPassword = tb_RepeatPassword.Value;

            bool isEmptyField = String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password) || String.IsNullOrEmpty(repeatPassword);

            if (!isEmptyField)
            {
                int result = accountBLL.CreateAdminAccount(Session["adminSession"].ToString(), username, password);
                if (result == 1)
                    Response.Redirect("CreateAccount.aspx?create=true", true);
                else
                    PlaceHolder_Error_Account.Visible = true;
            }
        }

        private void HideAllPlaceHolders()
        {
            PlaceHolder_TempMsg.Visible = false;
            PlaceHolder_EmptyFields.Visible = false;
            PlaceHolder_Error_Account.Visible = false;
            PlaceHolder_Error_Staff.Visible = false;
        }
    }
}