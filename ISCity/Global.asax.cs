using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISCity.Models;
using System.Web.Routing;

namespace ISCity
{
    public class MvcApplication : System.Web.HttpApplication
    {
        DBISCityEntities dbEnt = new DBISCityEntities();
        Random rnd = new Random();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var roleAdmin = dbEnt.Roles.Select(r => r.Name);
            if (!roleAdmin.Contains("Admin"))
            {
                Roles role = new Roles { Name = "Admin" };
                Users usr = new Users { FirstName = "Керимов", SecondName = "Алихан", Email = "admin@admin.ru", EmailConfirm = true };
                dbEnt.Roles.Add(role);
                dbEnt.Users.Add(usr);
                dbEnt.SaveChanges();
                dbEnt.Accounts.Add(new Accounts { user_id = usr.id, password = "admin" });
                dbEnt.UserRoles.Add(new UserRoles { user_id = usr.id, role_id = role.id });
                dbEnt.SaveChanges();

            }
        }
    }
}
