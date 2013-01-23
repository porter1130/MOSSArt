using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Workflow;
using System.Globalization;
using Microsoft.SharePoint;


namespace MOSSArt
{
    public class MOSSWorkflow
    {

        public static SPWorkflow StartWorkflow(Microsoft.SharePoint.SPListItem item, string wfName, WorkflowVariableValues variableValues)
        {
            SPWorkflowAssociation wfAss = item.ParentList.WorkflowAssociations.GetAssociationByName(wfName, CultureInfo.CurrentCulture);
            string eventData = SerializeUtil.Serialize(variableValues);
            return item.Web.Site.WorkflowManager.StartWorkflow(item, wfAss, eventData);
        }

        public static void TerminateWorkflow(int itemId, string listName, string wfName)
        {
            SPWeb currWeb = MOSSContext.Current != null ? MOSSContext.Current.Web : SPContext.Current.Web;

            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite osite = new SPSite(currWeb.Site.ID))
                {
                    using (SPWeb oweb = osite.OpenWeb(currWeb.ID))
                    {
                        SPListItem oitem = oweb.Lists[listName].GetItemById(itemId);
                        if (oitem != null)
                        {

                            foreach (SPWorkflow workflow in oitem.Workflows)
                            {
                                if (oweb.Lists[listName].WorkflowAssociations[workflow.AssociationId].Name.Equals(wfName)
                                    && !workflow.IsCompleted)
                                {
                                    oweb.AllowUnsafeUpdates = true;
                                    SPWorkflowManager.CancelWorkflow(workflow);
                                    oweb.AllowUnsafeUpdates = false;
                                    break;
                                }
                            }

                        }
                    }
                }
            });
        }
    }
}
