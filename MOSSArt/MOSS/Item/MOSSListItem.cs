using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using System.Collections;

namespace MOSSArt
{
    public class MOSSListItem
    {


        public static SPListItem AddItemWithElevatedPrivileges(string listName, System.Collections.Hashtable htItem)
        {
            SPListItem item = null;
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite osite = new SPSite(MOSSContext.Current.Site.ID))
                {
                    using (SPWeb oweb = osite.OpenWeb(MOSSContext.Current.Web.ID))
                    {
                        SPList olist = oweb.Lists[listName];
                        SPListItem oitem = olist.AddItem();
                        foreach (DictionaryEntry entry in htItem)
                        {
                            oitem[entry.Key.ToString()] = entry.Value;
                        }
                        oweb.AllowUnsafeUpdates = true;
                        oitem.Update();
                        item = oitem;
                        oweb.AllowUnsafeUpdates = false;
                    }
                }
            });

            return item;
        }

        public static SPListItem AddItem(string listName, Hashtable htItem)
        {
            SPList list = SPContext.Current.Web.Lists[listName];
            SPListItem item = list.AddItem();
            foreach (DictionaryEntry entry in htItem)
            {
                item[entry.Key.ToString()] = entry.Value;
            }
            SPContext.Current.Web.AllowUnsafeUpdates = true;
            item.Update();
            SPContext.Current.Web.AllowUnsafeUpdates = false;

            return item;
        }
    }
}
