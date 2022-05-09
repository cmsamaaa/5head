using FiveHead.Controller;
using FiveHead.Entity;
using System;

namespace FiveHead.Admin
{
    public partial class EditAccount : System.Web.UI.Page
    {
        Staff staff;

        AccountsController accountsController = new AccountsController();
        StaffsController staffsController = new StaffsController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    GetInfo();
                }
                catch (Exception)
                {
                    Response.Redirect("ViewAllAccounts.aspx", true);
                }
            }
        }

        private void GetInfo()
        {
            int accountID = Convert.ToInt32(Session["edit_AccountID"]);
            string profileName = Session["edit_ProfileName"].ToString();

            lbl_AccountID.Text = accountID.ToString();
            tb_UserProfile.Value = profileName;
            tb_Username.Value = accountsController.GetAccount(accountID).Username;
            lbl_Username.Text = tb_Username.Value;

            if (profileName.Equals("Administrator"))
            {
                div_Name.Visible = false;
                tb_AccountType.Value = profileName;
            }
            else if (profileName.Contains("Restaurant"))
            {
                tb_AccountType.Value = "Staff";
                staff = staffsController.GetStaffByAccountID(accountID);
                lbl_StaffID.Text = staff.StaffID.ToString();
                lbl_FirstName.Text = staff.FirstName;
                lbl_LastName.Text = staff.LastName;
                tb_FirstName.Value = staff.FirstName;
                tb_LastName.Value = staff.LastName;
            }
        }

        protected void btn_Update_Click(object sender, EventArgs e)
        {
            HideAllPlaceHolders();

            switch (tb_AccountType.Value)
            {
                case "Administrator":
                    UpdateAccount();
                    break;
                case "Staff":
                    UpdateStaff();
                    break;
                default:
                    break;
            }
        }

        private void UpdateAccount()
        {
            int result = 0;

            // Update Username
            int accountID = Convert.ToInt32(lbl_AccountID.Text);
            if (!lbl_Username.Text.Equals(tb_Username.Value))
            {
                result = accountsController.UpdateUsername(accountID, tb_Username.Value);
                if (result == 1)
                {
                    lbl_Username.Text = tb_Username.Value;
                    PlaceHolder_Success_Username.Visible = true;
                }
                else
                    PlaceHolder_Error_Username.Visible = true;
            }

            // Update Password
            if (!string.IsNullOrEmpty(tb_Password.Value))
            {
                string password = tb_Password.Value;
                string repeatPassword = tb_RepeatPassword.Value;

                if (password.Equals(repeatPassword))
                {
                    result = 0;
                    result = accountsController.UpdatePassword(tb_Username.Value, password);

                    if (result == 1)
                        PlaceHolder_Success_Password.Visible = true;
                    else
                        PlaceHolder_Error_Password.Visible = true;
                }
                else
                    PlaceHolder_Mismatch_Password.Visible = true;
            }
        }

        private void UpdateStaff()
        {
            UpdateAccount();

            // Update First Name & Last Name
            if (!lbl_FirstName.Text.Equals(tb_FirstName.Value) || !lbl_LastName.Text.Equals(tb_LastName.Value))
            {
                int staffID = Convert.ToInt32(lbl_StaffID.Text);
                string firstName = tb_FirstName.Value;
                string lastName = tb_LastName.Value;

                if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
                {
                    int result = 0;
                    result = staffsController.UpdateName(staffID, firstName, lastName);

                    if (result == 1)
                    {
                        lbl_FirstName.Text = firstName;
                        lbl_LastName.Text = lastName;
                        PlaceHolder_Success_Staff.Visible = true;
                    }
                    else
                        PlaceHolder_Error_Staff.Visible = true;
                }
            }            
        }

        private void HideAllPlaceHolders()
        {
            PlaceHolder_Error_Password.Visible = false;
            PlaceHolder_Error_Staff.Visible = false;
            PlaceHolder_Error_Username.Visible = false;
            PlaceHolder_Mismatch_Password.Visible = false;
            PlaceHolder_Success_Password.Visible = false;
            PlaceHolder_Success_Staff.Visible = false;
            PlaceHolder_Success_Username.Visible = false;
        }
    }
}