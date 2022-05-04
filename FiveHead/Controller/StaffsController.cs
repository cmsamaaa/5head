using FiveHead.Entity;

namespace FiveHead.Controller
{
    public class StaffsController
    {
        Account account;
        Profile profile;
        Staff staff = new Staff();
        AccountsController accountsController = new AccountsController();
        ProfilesController profilesController = new ProfilesController();

        public int CreateStaff(string firstName, string lastName, int accountID)
        {
            return staff.CreateStaff(firstName, lastName, accountID);
        }

        public Staff GetStaffByAccountID(int accountID)
        {
            return staff.GetStaffByAccountID(accountID);
        }

        public bool Authenticate(string username, string password)
        {
            account = accountsController.GetAccount(username, password);
            if (account == null || account.Deactivated)
                return false;

            profile = profilesController.GetProfileByID(account.ProfileID);
            if (profile.ProfileName.Equals("Restaurant Staff") || 
                profile.ProfileName.Equals("Restaurant Manager") ||
                profile.ProfileName.Equals("Restaurant Owner"))
                return true;

            return false;
        }

        public int UpdateName(int staffID, string firstName, string lastName)
        {
            return staff.UpdateName(staffID, firstName, lastName);
        }
    }
}