using System;

namespace FiveHead.Entity
{
    public class Staff
    {
        private int staffID;
        private string firstName;
        private string lastName;
        private double salary;
        private int accountID;

        public Staff()
        {

        }

        public Staff(int staffID)
        {
            this.StaffID = staffID;
        }

        public Staff(int staffID, string firstName, string lastName) : this(staffID)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public Staff(int staffID, string firstName, string lastName, int accountID) : this(staffID, firstName, lastName)
        {
            this.AccountID = accountID;
        }

        public Staff(int staffID, string firstName, string lastName, double salary, int accountID) : this(staffID, firstName, lastName, accountID)
        {
            this.AccountID = accountID;
            this.Salary = salary;
        }

        public Staff(Staff staff) : this(staff.StaffID, staff.firstName, staff.LastName, staff.Salary, staff.AccountID)
        {
            if (staff == null)
                throw new ArgumentNullException();
        }

        public int StaffID { get => staffID; set => staffID = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public double Salary { get => salary; set => salary = value; }
        public int AccountID { get => accountID; set => accountID = value; }
    }
}