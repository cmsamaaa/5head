using System;

namespace FiveHead.Entity
{
    public class Staff
    {
        private int accountID;
        private int staffID;
        private string staffName;

        public Staff()
        {

        }

        public Staff(int accountID)
        {
            this.accountID = accountID;
        }

        public Staff(int accountID, int staffID, string staffName) : this(accountID)
        {
            this.staffID = staffID;
            this.staffName = staffName;
        }

        public int AccountID { get => accountID; set => accountID = value; }
        public string StaffName { get => staffName; set => staffName = value; }
        public int StaffID { get => staffID; set => staffID = value; }
    }
}