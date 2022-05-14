using FiveHead.Scripts.Libraries;
using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace FiveHead.Entity
{
    public class Coupon
    {
        private int couponID;
        private string code;
        private int discount;
        private bool deactivated;

        public Coupon()
        {

        }

        public Coupon(int couponID, int discount)
        {
            this.CouponID = couponID;
            this.discount = discount;
        }

        public Coupon(string code, int discount)
        {
            this.Code = code;
            this.Discount = discount;
        }

        public Coupon(int couponID, string code, int discount) : this(couponID, discount)
        {
            this.Code = code;
        }

        public Coupon(int couponID, bool deactivated)
        {
            this.CouponID = couponID;
            this.Deactivated = deactivated;
        }

        public Coupon(int couponID, string code, int discount, bool deactivated) : this (couponID, code, discount)
        {
            this.Deactivated = deactivated;
        }

        public Coupon(Coupon coupon) : this (coupon.CouponID, coupon.Code, coupon.Discount, coupon.Deactivated)
        {
            if (coupon == null)
                throw new ArgumentNullException();
        }

        public int CouponID { get => couponID; set => couponID = value; }
        public string Code { get => code; set => code = value; }
        public int Discount { get => discount; set => discount = value; }
        public bool Deactivated { get => deactivated; set => deactivated = value; }

        // Data Access
        MySQL_Utils mySQL = new MySQL_Utils();
        DBConn dbConn = new DBConn();
        private string errMsg;

        public int CreateCoupon()
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("INSERT INTO Coupons (code, discount)");
            sql.AppendLine(" ");
            sql.AppendLine("VALUES (@code, @discount)");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@code", this.Code);
                sqlCmd.Parameters.AddWithValue("@discount", this.Discount);
                conn.Open();
                result = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return result;
        }

        public DataSet GetAllCoupons()
        {
            StringBuilder sql;
            MySqlDataAdapter da;
            DataSet ds;

            MySqlConnection conn = dbConn.GetConnection();
            ds = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT *");
            sql.AppendLine(" ");
            sql.AppendLine("FROM Coupons");
            conn.Open();

            try
            {
                da = mySQL.adapter_set_query(sql.ToString(), conn);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return ds;
        }

        public Coupon GetCouponByID(int couponID)
        {
            StringBuilder sql;
            MySqlDataAdapter da;
            DataSet ds;

            MySqlConnection conn = dbConn.GetConnection();
            ds = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT *");
            sql.AppendLine(" ");
            sql.AppendLine("FROM Coupons");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE couponID = @couponID");
            conn.Open();

            try
            {
                da = mySQL.adapter_set_query(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("couponID", couponID);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            if (ds.Tables[0].Rows.Count > 0)
                return new Coupon(ds.Tables[0].ToList<Coupon>()[0]);
            else
                return null;
        }

        public Coupon GetCouponByCode(string code)
        {
            StringBuilder sql;
            MySqlDataAdapter da;
            DataSet ds;

            MySqlConnection conn = dbConn.GetConnection();
            ds = new DataSet();
            sql = new StringBuilder();
            sql.AppendLine("SELECT *");
            sql.AppendLine(" ");
            sql.AppendLine("FROM Coupons");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE code = @code");
            conn.Open();

            try
            {
                da = mySQL.adapter_set_query(sql.ToString(), conn);
                da.SelectCommand.Parameters.AddWithValue("code", code);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            if (ds.Tables[0].Rows.Count > 0)
                return new Coupon(ds.Tables[0].ToList<Coupon>()[0]);
            else
                return null;
        }

        public int UpdateCoupon()
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("UPDATE Coupons");
            sql.AppendLine(" ");
            sql.AppendLine("SET code=@code, discount=@discount");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE couponID=@couponID");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@couponID", this.CouponID);
                sqlCmd.Parameters.AddWithValue("@code", this.Code);
                sqlCmd.Parameters.AddWithValue("@discount", this.Discount);
                conn.Open();
                result = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return result;
        }

        public int UpdateCouponDiscount()
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("UPDATE Coupons");
            sql.AppendLine(" ");
            sql.AppendLine("SET discount=@discount");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE couponID=@couponID");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@couponID", this.CouponID);
                sqlCmd.Parameters.AddWithValue("@discount", this.Discount);
                conn.Open();
                result = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return result;
        }

        public int UpdateCouponStatus()
        {
            StringBuilder sql;
            MySqlCommand sqlCmd;
            int result;

            result = 0;

            sql = new StringBuilder();
            sql.AppendLine("UPDATE Coupons");
            sql.AppendLine(" ");
            sql.AppendLine("SET deactivated=@deactivated");
            sql.AppendLine(" ");
            sql.AppendLine("WHERE couponID=@couponID");
            MySqlConnection conn = dbConn.GetConnection();
            try
            {
                sqlCmd = mySQL.cmd_set_connection(sql.ToString(), conn);
                sqlCmd.Parameters.AddWithValue("@couponID", this.CouponID);
                sqlCmd.Parameters.AddWithValue("@deactivated", this.Deactivated);
                conn.Open();
                result = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            finally
            {
                dbConn.CloseConnection(conn);
            }

            return result;
        }
    }
}