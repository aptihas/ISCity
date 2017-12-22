using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.IO;
using System.Text;

namespace ISCity.Models
{
    //довести до ума
    public class MyRoleAtribute : AuthorizeAttribute
    {
        DBISCityEntities dbEnt = new DBISCityEntities();

        private string[] allowedUsers = new string[] { };
        private string[] allowedRoles = new string[] { };

        public MyRoleAtribute()
        { }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!String.IsNullOrEmpty(base.Users))
            {
                allowedUsers = base.Users.Split(new char[] { ',' });
                for (int i = 0; i < allowedUsers.Length; i++)
                {
                    allowedUsers[i] = allowedUsers[i].Trim();
                }
            }
            if (!String.IsNullOrEmpty(base.Roles))
            {
                allowedRoles = base.Roles.Split(new char[] { ',' });
                for (int i = 0; i < allowedRoles.Length; i++)
                {
                    allowedRoles[i] = allowedRoles[i].Trim();
                }
            }
            return httpContext.Request.IsAuthenticated &&
                 User(httpContext) && Role(httpContext);
        }

        private bool User(HttpContextBase httpContext)
        {
            if (allowedUsers.Length > 0)
            {
                return allowedUsers.Contains(httpContext.User.Identity.Name);
            }
            return true;
        }

        private bool Role(HttpContextBase httpContext)
        {
            var _usr = dbEnt.Users.Where(u => u.Email == httpContext.User.Identity.Name).Select(s => s).FirstOrDefault();
            if (_usr != null)
            {
                var userRoles = from r in dbEnt.UserRoles
                                where r.user_id == _usr.id
                                select r.Roles.Name;

                if (allowedRoles.Length > 0)
                {
                    for (int i = 0; i < allowedRoles.Length; i++)
                    {
                        if (userRoles.Contains(allowedRoles[i]))
                            return true;
                    }
                    return false;
                }
                return true;
            }
            else
            {
                return true;
            }
        }
    }
}