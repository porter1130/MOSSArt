﻿using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint;

namespace MOSSArt
{
    public class MOSSMail
    {

        public static bool SendMail(string to, string subject, string messageBody)
        {
            return SPUtility.SendEmail(MOSSContext.Current != null ? MOSSContext.Current.Web : SPContext.Current.Web, true, false, to, subject, messageBody);
        }

        public static bool SendMail(SPWeb web,string to, string subject, string messageBody)
        {
            return SPUtility.SendEmail(web, true, false, to, subject, messageBody);
        }
    }
}
