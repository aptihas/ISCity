using ISCity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISCity.Controllers
{
    [MyRoleAtribute(Roles = "SubCompany")]
    public class SubCompanyController : Controller
    {
        // GET: SubCompany
        public ActionResult Index()
        {
            return View();
        }
    }
}