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
        DBISCityEntities dbEnt = new DBISCityEntities();
        // GET: ManageCompany
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RequestList()
        {
            var _acc = (from a in dbEnt.Accounts where a.Users.Email == User.Identity.Name select a.Users).FirstOrDefault();
            var _requests = dbEnt.UserRequests.Where(r => r.mangeCompany_id == _acc.manageCompany_id).Select(r => r);

            return View(_requests);
        }

        public ActionResult DetailsRequest(int id)
        {
            var _requests = dbEnt.UserRequests.Where(r => r.id == id).Select(r => r).FirstOrDefault();
            return View(_requests);
        }

        public ActionResult ConsolidateSubCompany(int id)
        {
            var _requests = dbEnt.UserRequests.Where(r => r.id == id).Select(r => r).FirstOrDefault();
            ViewBag.SubCompanyList = dbEnt.SubCompany.ToList();
            return View(_requests);
        }

        [HttpPost]
        public ActionResult ConsolidateSubCompany(int id_request, int subCompany_id)
        {
            var _requests = dbEnt.UserRequests.Where(r => r.id == id_request).Select(r => r).FirstOrDefault();
            _requests.subCompany_id = subCompany_id;
            dbEnt.SaveChanges();
            return RedirectToAction("DetailsRequest", new { id = id_request });
        }


        public ActionResult AddSubCompany()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSubCompany(SubCompany sc)
        {
            var _acc = (from a in dbEnt.Accounts where a.Users.Email == User.Identity.Name select a.Users).FirstOrDefault();
            dbEnt.SubCompany.Add(new SubCompany { mangeCompany_id= (int)_acc.manageCompany_id, Name=sc.Name, Street=sc.Street, HouseNumber=sc.HouseNumber });
            dbEnt.SaveChanges();
            return RedirectToAction("SubCompanyList");
        }

        public ActionResult SubCompanyList()
        {
            var scList = dbEnt.SubCompany.ToList();
            return View(scList);
        }

        public ActionResult EditSubCompany(int id)
        {
            var sc = dbEnt.SubCompany.Where(s => s.id == id).Select(s => s).FirstOrDefault();

            return View(sc);
        }

        [HttpPost]
        public ActionResult EditSubCompany(SubCompany _sc)
        {
            var sc = dbEnt.SubCompany.Where(s => s.id == _sc.id).Select(s => s).FirstOrDefault();
            sc.Name = _sc.Name;
            sc.Street = _sc.Street;
            sc.HouseNumber = _sc.HouseNumber;
            dbEnt.SaveChanges();

            return RedirectToAction("SubCompanyList");
        }
    }
}