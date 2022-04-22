﻿using FiveHead.Controller;
using System;

namespace FiveHead
{
    public partial class Register : System.Web.UI.Page
    {
        ProfilesController profilesController = new ProfilesController();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Register_Click(object sender, EventArgs e)
        {
            string username, password, confirmPassword;

            if (cb_Agree.Checked)
            {
                username = tb_Username.Text.Trim();
                password = tb_Password.Text.Trim();
                confirmPassword = tb_ConfirmPassword.Text.Trim();

                if (password.Equals(confirmPassword))
                {
                    AccountsController account = new AccountsController();

                    int result = account.CreateAccount(username, password, profilesController.GetProfileIDByName("Customer"));

                    if (result == 1)
                        Response.Redirect("~/Login.aspx?register=true", true);
                    else
                        ShowMessage("Fail to create account.");
                }
                else
                    ShowMessage("Passwords does not match. Please try again.");
            }
            else
                ShowMessage("Please agree to our terms & conditions");
        }

        private void ShowMessage(string Message)
        {
            if (!ClientScript.IsClientScriptBlockRegistered("MyMessage"))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "MyMessage", "alert('" + Message + "');", true);
            }
        }

        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Register.aspx", true);
        }
    }
}