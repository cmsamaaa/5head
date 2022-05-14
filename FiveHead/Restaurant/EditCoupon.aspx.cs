using FiveHead.Controller;
using FiveHead.Entity;
using System;
using System.Web.UI.WebControls;

namespace FiveHead.Restaurant
{
    public partial class EditCoupon : System.Web.UI.Page
    {
        CouponsController couponsController;

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
                    Response.Redirect("ViewAllCoupons.aspx", true);
                }
            }
        }

        private void GetInfo()
        {
            couponsController = new CouponsController();
            int couponID = Convert.ToInt32(Session["edit_CouponID"].ToString());

            Coupon coupon = couponsController.GetCouponByID(couponID);
            tb_CouponCode.Value = coupon.Code;
            tb_Discount.Value = coupon.Discount.ToString();
        }

        protected void btn_Update_Click(object sender, EventArgs e)
        {
            HideAllPlaceHolders();

            if (string.IsNullOrEmpty(tb_CouponCode.Value) || string.IsNullOrEmpty(tb_Discount.Value) || tb_Discount.Value.Equals("0"))
                Response.Redirect(string.Format("EditCoupon.aspx?error=empty&code={0}&discount={1}", tb_CouponCode.Value, tb_Discount.Value), true);

            if (!int.TryParse(tb_Discount.Value, out int discount))
                Response.Redirect(string.Format("EditCoupon.aspx?discount=invalid&code={0}", tb_CouponCode.Value), true);

            couponsController = new CouponsController();

            int couponID = Convert.ToInt32(Session["edit_CouponID"].ToString());
            string couponCode = tb_CouponCode.Value;
            discount = Convert.ToInt32(tb_Discount.Value);

            int result = couponsController.UpdateCoupon(couponID, couponCode, discount);
            if (result == 1)
                Response.Redirect("EditCoupon.aspx?update=true", true);

            result = couponsController.UpdateCoupon(couponID, discount);
            if (result == 1)
                PlaceHolder_OnlyDiscount.Visible = true;
            else
                Response.Redirect(string.Format("EditCoupon.aspx?update=false&code={0}&discount={1}", couponCode, discount), true);

            if (!couponsController.CheckCodeID(couponID, couponCode))
                PlaceHolder_ErrorCode.Visible = true;
        }

        private void HideAllPlaceHolders()
        {
            PlaceHolder_OnlyDiscount.Visible = false;
            PlaceHolder_ErrorCode.Visible = false;
        }
    }
}