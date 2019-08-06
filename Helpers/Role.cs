using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Tools.Helpers
{
    public static class RoleHelpers
    {
        public static bool IsInRoles(System.Security.Principal.IPrincipal user, string Role)
        {
            Role = Role + ",Administrator";
            bool inrole = false;
            var roles = Role.Split(new[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);
            foreach (string r in roles)
            {
                inrole = user.IsInRole(r);
                if (inrole) break;

            }
            return inrole;
        }
        public static bool IsInRoles(string Role)
        {
           // if (!Role.Contains("Administrator"))
                Role = Role +",Administrator";
            var user = HttpContext.Current.User;
            bool inrole = false;
            var roles = Role.Split(new[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);

            foreach (string r in roles)
            {
                inrole = user.IsInRole(r.Trim());
                
                if (inrole) break;

            }
            return inrole;
        }


       public static bool IsReadOnly(bool isClosed)
        {
            if (RoleHelpers.IsInRoles("Administrator"))
                return false;
            if (RoleHelpers.IsInRoles("Master_104"))
                return true;
            else if (RoleHelpers.IsInRoles("Operator_104") && (!isClosed))
                return false;
            else if (RoleHelpers.IsInRoles("Operator_104") && (isClosed))
                return true;
            else if (RoleHelpers.IsInRoles("Administrator_104"))
                return false;
            else
                return true;

        }
    }
}
