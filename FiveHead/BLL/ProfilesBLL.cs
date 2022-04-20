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

        public List<Profile> GetAllProfiles()
        {
            return dataLayer.GetAllProfiles();
        }

        public List<string> GetAllProfileNames()
        {
            List<Profile> profileList = GetAllProfiles();
            List<string> list = new List<String>();
            foreach (Profile profile in profileList)
                list.Add(profile.ProfileName);
            return list;
        }
    }
}