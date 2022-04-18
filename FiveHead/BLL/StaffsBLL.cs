using FiveHead.DAL;
using FiveHead.Entity;

namespace FiveHead.BLL
{
    public class StaffsBLL
    {
        Account account;
        Profile profile;
        AccountsBLL accountsBLL = new AccountsBLL();
        ProfilesBLL profilesBLL = new ProfilesBLL();

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