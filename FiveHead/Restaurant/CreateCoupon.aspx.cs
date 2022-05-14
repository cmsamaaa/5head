using FiveHead.Controller;
using System;

namespace FiveHead.Restaurant
{
    public partial class CreateCoupon : System.Web.UI.Page
    {
        CouponsController couponsController;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btn_Create_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_CouponCode.Value) || string.IsNullOrEmpty(tb_Discount.Value) || tb_Discount.Value.Equals("0")) 
                Response.Redirect(string.Format("CreateCoupon.aspx?error=empty&code={0}&discount={1}", tb_CouponCode.Value, tb_Discount.Value), true);

            if (!int.TryParse(tb_Discount.Value, out int discount))
                Response.Redirect(string.Format("CreateCoupon.aspx?discount=invalid&code={0}", tb_CouponCode.Value), true);

            couponsController = new CouponsController();

            string couponCode = tb_CouponCode.Value;
            discount = Convert.ToInt32(tb_Discount.Value);

            int result = couponsController.CreateCoupon(couponCode, discount);
            if (result == 1)
                Response.Redirect("CreateCoupon.aspx?create=true", true);
            else
                Response.Redirect(string.Format("CreateCoupon.aspx?create=false&code={0}&discount={1}", couponCode, discount), true);
        }
    }
}