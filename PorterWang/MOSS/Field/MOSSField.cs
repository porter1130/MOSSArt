using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace MOSSArt
{
    public class MOSSField
    {

        //public static SPFieldUserValueCollection PickUserValue(PeopleEditor peopleEditor)
        //{
        //    SPFieldUserValueCollection fieldUserValues = new SPFieldUserValueCollection();
        //    foreach (PickerEntity entity in peopleEditor.ResolvedEntities)
        //    {
        //        SPPrincipal principal = MOSSPrincipal.FindUserOrSiteGroup(entity.Key);
        //        SPFieldUserValue userValue = new SPFieldUserValue(MOSSContext.Current.Web, principal.ID,principal.Name);

        //        fieldUserValues.Add(userValue);
        //    }

        //    return fieldUserValues;
        //}
    }
}
