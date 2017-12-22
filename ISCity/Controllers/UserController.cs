using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISCity.Models;

namespace ISCity.Controllers
{

    public class UserController : Controller
    {
        // GET: User
        [MyRoleAtribute(Roles = "User")]
        public ActionResult Index()
        {
            return View();
        }
    }
}