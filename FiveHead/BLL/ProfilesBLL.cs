using FiveHead.DAL;
using FiveHead.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiveHead.BLL
{
    public class ProfilesBLL
    {
        Profile profile;
        ProfilesDAL dataLayer = new ProfilesDAL();

        public int CreateProfile(string profileName, int permissionLevel)
        {
            profile = new Profile(profileName, permissionLevel);
            return dataLayer.CreateProfile(profile);
        }

        public Profile GetProfileByID(int profile)
        {
            return dataLayer.GetProfileByID(profile);
        }
    }
}