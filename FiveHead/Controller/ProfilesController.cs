using FiveHead.Entity;
using FiveHead.Scripts.Libraries;
using System;
using System.Collections.Generic;
using System.Data;

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
            if (profile == null)
                return null;
            return profile.ProfileName;
        }

        public int GetProfileIDByName(string profileName)
        {
            profile = profile.GetProfileByName(profileName);
            if (profile == null)
                return -1;
            return profile.ProfileID;
        }

        public DataSet GetAllProfilesDataSet()
        {
            return profile.GetAllProfiles();
        }

        public List<Profile> GetAllProfiles()
        {
            DataSet ds = GetAllProfilesDataSet();

            if (ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].ToList<Profile>();
            else
                return null;
        }

        public List<string> GetAllProfileNames()
        {
            List<Profile> profileList = GetAllProfiles();
            List<string> list = new List<String>();
            foreach (Profile profile in profileList)
                list.Add(profile.ProfileName);
            return list;
        }

        public int UpdateProfileName(int profileID, string profileName)
        {
            int result = GetProfileIDByName(profileName);
            if (result == -1)
                return profile.UpdateProfileName(profileID, profileName);
            return 0;
        }

        public int ReactivateProfile(int profileID)
        {
            return profile.UpdateProfileStatus(profileID, false);
        }

        public int SuspendProfile(int profileID)
        {
            return profile.UpdateProfileStatus(profileID, true);
        }
    }
}