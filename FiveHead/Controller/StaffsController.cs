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
            staff = new Staff(firstName, lastName, accountID);
            return staff.CreateStaff();
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

        public string GetStaffProfile(string username)
        {
            account = accountsController.GetAccountByUsername(username);
            return profilesController.GetProfileByID(account.ProfileID).ProfileName;
        }

        public int UpdateName(int staffID, string firstName, string lastName)
        {
            staff = new Staff(staffID, firstName, lastName);
            return staff.UpdateName();
        }
    }
}