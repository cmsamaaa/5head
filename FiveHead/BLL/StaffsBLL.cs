using FiveHead.DAL;
using FiveHead.Entity;

namespace FiveHead.BLL
{
    public class StaffsBLL
    {
        Account account;
        Profile profile;
        StaffsDAL dataLayer = new StaffsDAL();
        AccountsBLL accountsBLL = new AccountsBLL();
        ProfilesBLL profilesBLL = new ProfilesBLL();

        public int CreateStaff(string firstName, string lastName, int accountID)
        {
            return dataLayer.CreateStaff(firstName, lastName, accountID);
        }

        public bool Authenticate(string username, string password)
        {
            account = accountsBLL.GetAccount(username, password);
            profile = profilesBLL.GetProfileByID(account.ProfileID);

            if (profile.ProfileName.Equals("Restaurant Staff") || 
                profile.ProfileName.Equals("Restaurant Manager") ||
                profile.ProfileName.Equals("Restaurant Owner"))
                return true;

            return false;
        }
    }
}