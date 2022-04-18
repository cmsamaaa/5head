using System;

namespace FiveHead.Entity
{
    public class Staff
    {
        private int staffID;
        private string staffName;
        private int accountID;

        public Staff()
        {

        }

        public Staff(int staffID)
        {
            this.StaffID = staffID;
        }

        public Staff(int staffID, string staffName) : this(staffID)
        {
            this.StaffName = staffName;
        }

        public Staff(int staffID, string staffName, int accountID) : this(staffID, staffName)
        {
            this.AccountID = accountID;
        }

        public Staff(Staff staff) : this(staff.StaffID, staff.StaffName, staff.AccountID)
        {
            if (staff == null)
                throw new ArgumentNullException();
        }

        public int StaffID { get => staffID; set => staffID = value; }
        public string StaffName { get => staffName; set => staffName = value; }
        public int AccountID { get => accountID; set => accountID = value; }
    }
}