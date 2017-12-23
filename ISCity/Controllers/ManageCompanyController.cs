using ISCity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISCity.Controllers
{
    [MyRoleAtribute(Roles = "ManageCompany")]
    public class ManageCompanyController : Controller
    {
        // GET: ManageCompany
        public ActionResult Index()
        {
            return View();
        }
    }
}