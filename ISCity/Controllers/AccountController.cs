using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ISCity.Models;
using System.Web.Mvc;

namespace ISCity.Controllers
{
    
    public class AccountController : Controller
    {
        DBISCityEntities dbis = new DBISCityEntities();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LogInModel model)
        {


            return View();
        }
    }
}