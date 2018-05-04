using System.Collections.Generic;

namespace MSM.Data
{
    public class MojangAuthInfo
    {
        public static string SessionKey = "AuthenticationInfo";

        public string AccessToken { get; set; }
        public string ClientToken { get; set; }
        public Profile SelectedProfile { get; set; }

        public List<Profile> AvailableProfiles { get; set; }
        public UserInfo User { get; set; }
        public class Profile
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        public class UserInfo
        {
            public string Id { get; set; }
        }
    }
}
