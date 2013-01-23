using System;
using Microsoft.SharePoint;
using System.Diagnostics;
using Microsoft.SharePoint.Utilities;

namespace MOSSArt.Utilities
{
    public class CommonUtil
    {
        public static string FormatDate(DateTime unformatDate)
        {
            return SPUtility.FormatDate(SPContext.Current.Web, unformatDate, SPDateFormat.DateTime);
        }
        public static void LogEventInfo(string info)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                string sourceName = @"Medalsoft";
                string logName = @"WF";

                if (EventLog.SourceExists(sourceName))
                {

                    string oldLogName = EventLog.LogNameFromSourceName(sourceName, System.Environment.MachineName);
                    if (!oldLogName.Equals(logName))
                    {
                        EventLog.Delete(oldLogName);
                    }
                }

                if (!EventLog.Exists(logName))
                {
                    EventLog.CreateEventSource(sourceName, logName);
                }

                EventLog myLog = new EventLog();
                myLog.Source = sourceName;
                myLog.Log = logName;

                myLog.WriteEntry(info, EventLogEntryType.Information);

            });

        }
    }
}
