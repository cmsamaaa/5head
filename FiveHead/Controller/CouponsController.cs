using FiveHead.Entity;
using FiveHead.Scripts.Libraries;
using System.Collections.Generic;
using System.Data;

namespace FiveHead.Controller
{
    public class CouponsController
    {
        Coupon coupon = new Coupon();

        public int CreateCoupon(string code, int discount)
        {
            coupon = new Coupon(code, discount);
            return coupon.CreateCoupon();
        }

        public DataSet GetAllCouponsDataSet()
        {
            return coupon.GetAllCoupons();
        }

        public List<Coupon> GetAllCoupons()
        {
            DataSet ds = GetAllCouponsDataSet();

            if (ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].ToList<Coupon>();
            else
                return null;
        }

        public Coupon GetCouponByID(int couponID)
        {
            return coupon.GetCouponByID(couponID);
        }

        public Coupon GetCouponByCode(string code)
        {
            return coupon.GetCouponByCode(code);
        }

        public bool CheckCodeID(int couponID, string code)
        {
            coupon = GetCouponByCode(code);
            return coupon.CouponID == couponID;
        }

        public bool CheckCodeExist(string code)
        {
            coupon = GetCouponByCode(code);
            return coupon != null;
        }

        public int UpdateCoupon(int couponID, string code, int discount)
        {
            if (!CheckCodeExist(code))
            {
                coupon = new Coupon(couponID, code, discount);
                return coupon.UpdateCoupon();
            }
            else
                return 0;
        }

        public int UpdateCoupon(int couponID, int discount)
        {
            coupon = new Coupon(couponID, discount);
            return coupon.UpdateCouponDiscount();
        }

        public int ReactivateCoupon(int couponID)
        {
            coupon = new Coupon(couponID, false);
            return coupon.UpdateCouponStatus();
        }

        public int SuspendCoupon(int couponID)
        {
            coupon = new Coupon(couponID, true);
            return coupon.UpdateCouponStatus();
        }
    }
}