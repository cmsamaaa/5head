using System;

namespace FiveHead.Menu
{
    public partial class EnterTable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            bool result = int.TryParse(tb_TableNo.Value, out int tableNo);

            if (!result)
            {
                ShowMessage("Please enter a valid table");
                return;
            }

            Session["tableNo"] = tableNo;
            Response.Redirect("Menu.aspx", true);
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