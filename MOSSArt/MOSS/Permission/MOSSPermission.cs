using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace MOSSArt
{
    public class MOSSPermission
    {
        //private MOSSContext _context;

        //public MOSSPermission(MOSSContext context)
        //{
        //    _context = context;
        //}

        public static SPRoleAssignment GetRoleAssignment(SPPrincipal principal, SPRoleType roleType)
        {
            SPWeb web = MOSSContext.Current.Web;
            SPRoleAssignment roleAssign = new SPRoleAssignment(principal);
            SPRoleDefinition roleDef = web.RoleDefinitions.GetByType(roleType);
            roleAssign.RoleDefinitionBindings.Add(roleDef);

            return roleAssign;
        }

        public static SPRoleAssignment GetRoleAssignment(SPWeb web, SPPrincipal principal, SPRoleType roleType)
        {
            SPRoleAssignment roleAssign = new SPRoleAssignment(principal);
            SPRoleDefinition roleDef = web.RoleDefinitions.GetByType(roleType);
            roleAssign.RoleDefinitionBindings.Add(roleDef);

            return roleAssign;
        }
    }
}
