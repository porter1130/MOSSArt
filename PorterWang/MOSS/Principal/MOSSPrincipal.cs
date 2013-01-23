using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System.Globalization;

namespace MOSSArt
{
    public class MOSSPrincipal
    {
        //private static MOSSContext _context;
        //public MOSSPrincipal(MOSSContext context) {
        //    _context = context;
        //}
        public static SPPrincipal FindUserOrSiteGroup(string userOrGroup)
        {
            SPWeb web = MOSSContext.Current.Web;
            SPPrincipal principal = null;
            if (SPUtility.IsLoginValid(web.Site, userOrGroup))
            {
                principal = web.EnsureUser(userOrGroup);
            }
            else
            {
                foreach (SPGroup group in web.SiteGroups)
                {
                    if (group.Name.ToUpper(CultureInfo.InvariantCulture) == userOrGroup.ToUpper(CultureInfo.InvariantCulture))
                    {
                        principal = group;
                        break;
                    }
                }
            }

            return principal;
        }


        public static SPPrincipal FindUserOrSiteGroup(SPWeb web, string userOrGroup)
        {
            SPPrincipal principal = null;
            if (SPUtility.IsLoginValid(web.Site, userOrGroup))
            {
                principal = web.EnsureUser(userOrGroup);
            }
            else
            {
                foreach (SPGroup group in web.SiteGroups)
                {
                    if (group.Name.ToUpper(CultureInfo.InvariantCulture) == userOrGroup.ToUpper(CultureInfo.InvariantCulture))
                    {
                        principal = group;
                        break;
                    }
                }
            }

            return principal;
        }
    }
}
