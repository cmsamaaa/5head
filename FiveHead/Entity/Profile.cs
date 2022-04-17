using System;

namespace FiveHead.Entity
{
    public class Profile
    {
        private int profileID;
        private string profileName;
        private int permissionLevel;

        public Profile()
        {

        }

        public Profile(string profileName, int permissionLevel)
        {
            this.ProfileName = profileName;
            this.PermissionLevel = permissionLevel;
        }

        public Profile(int profileID, string profileName, int permissionLevel) : this(profileName, permissionLevel)
        {
            this.ProfileID = profileID;
        }

        public Profile (Profile profile) : this(profile.ProfileID, profile.ProfileName, profile.PermissionLevel)
        {
            if (profile == null)
                throw new ArgumentNullException();
        }

        public int ProfileID { get => profileID; set => profileID = value; }
        public string ProfileName { get => profileName; set => profileName = value; }
        public int PermissionLevel { get => permissionLevel; set => permissionLevel = value; }
    }
}