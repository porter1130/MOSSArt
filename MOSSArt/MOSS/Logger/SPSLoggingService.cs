using System.Collections.Generic;
using Microsoft.SharePoint.Administration;

namespace MOSSArt.Logger
{
    public class SPSLoggingService : SPDiagnosticsServiceBase
    {
        public static string DiagnosticsAreaName = "Medalsoft";
        private static SPSLoggingService _Current;

        internal static SPSLoggingService Current
        {
            get
            {
                if (_Current == null)
                {
                    _Current = new SPSLoggingService();
                }
                return _Current;
            }
        }

        private SPSLoggingService() : base("Medalsoft Logging Service", SPFarm.Local) { }

        protected override IEnumerable<SPDiagnosticsArea> ProvideAreas()
        {
            List<SPDiagnosticsArea> areas = new List<SPDiagnosticsArea>{
                new SPDiagnosticsArea(DiagnosticsAreaName,new List<SPDiagnosticsCategory>{
                    new SPDiagnosticsCategory("TMS",TraceSeverity.Unexpected,EventSeverity.Error)
               })
            };

            return areas;
        }


        public static void LogError(string categoryName, string errorMessage)
        {
            SPDiagnosticsCategory category = SPSLoggingService.Current.Areas[DiagnosticsAreaName].Categories[categoryName];
            SPSLoggingService.Current.WriteTrace(0, category, TraceSeverity.Unexpected, errorMessage);
        }
    }
}
