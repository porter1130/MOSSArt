using System;
using Microsoft.SharePoint;
using Microsoft.Office.Server.UserProfiles;

namespace MOSSArt
{
    public class MOSSUserProfile
    {
        public static UserProfile GetUserProfileByLoginName(string userAccount)
        {
            UserProfile userProfile = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite osite = new SPSite(MOSSContext.Current!=null?MOSSContext.Current.Site.ID:SPContext.Current.Site.ID))
                    {
                        SPServiceContext context = SPServiceContext.GetContext(osite);
                        UserProfileManager profileManager = new UserProfileManager(context);

                        if (profileManager.UserExists(userAccount))
                        {
                            userProfile = profileManager.GetUserProfile(userAccount);
                        }
                    }
                }
                );
            }
            catch (Exception e)
            {
                throw new Exception("获取当前的用户信息出错：" + e.Message);
            }

            return userProfile;
        }
    }
}
