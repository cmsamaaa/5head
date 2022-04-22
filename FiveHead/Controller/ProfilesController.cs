using FiveHead.Entity;
using System;
using System.Collections.Generic;

namespace FiveHead.Controller
{
    public class ProfilesController
    {
        Profile profile = new Profile();

        public int CreateProfile(string profileName)
        {
            profile = new Profile(profileName);
            return profile.CreateProfile(profile);
        }

        public Profile GetProfileByID(int profileID)
        {
            return profile.GetProfileByID(profileID);
        }

        public string GetProfileNameByID(int profileID)
        {
            profile = profile.GetProfileByID(profileID);
            return profile.ProfileName;
        }

        public int GetProfileIDByName(string profileName)
        {
            profile = profile.GetProfileByName(profileName);
            return profile.ProfileID;
        }

        public List<Profile> GetAllProfiles()
        {
            return profile.GetAllProfiles();
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