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
        Random rnd = new Random();
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
        public ActionResult AddSubCompany(SubCompanyModel sc)
        {
            var emailList = dbEnt.Users.Select(s => s.Email);
            var nameList = dbEnt.SubCompany.Select(s => s.Name);
            if (!emailList.Contains(sc.Email) && !nameList.Contains(sc.Name))
            {
                var _acc = (from a in dbEnt.Accounts where a.Users.Email == User.Identity.Name select a.Users).FirstOrDefault();

                SubCompany _scObj = new SubCompany { Name = sc.Name, Street = sc.Street, HouseNumber = sc.HouseNumber, mangeCompany_id=(int)_acc.manageCompany_id };
                dbEnt.SubCompany.Add(_scObj);
                dbEnt.SaveChanges();

                Users _usrObj = new Users { FirstName = sc.FirstName, SecondName = sc.SecondName, ThirdName = sc.ThirdName, Email = sc.Email, EmailConfirm = false, subCompany_id = _scObj.id };
                dbEnt.Users.Add(_usrObj);
                dbEnt.SaveChanges();

                var roleList = dbEnt.Roles.Select(r => r.Name);
                Roles _rlobj;
                if (!roleList.Contains("SubCompany"))
                {
                    _rlobj = new Roles { Name = "SubCompany" };
                    dbEnt.Roles.Add(_rlobj);
                    dbEnt.SaveChanges();
                }
                else
                {
                    _rlobj = dbEnt.Roles.Where(r => r.Name == "SubCompany").Select(r => r).FirstOrDefault();
                }

                dbEnt.Accounts.Add(new Accounts { user_id = _usrObj.id, password = ISCityFramework.GetPass(rnd) });
                dbEnt.SaveChanges();

                dbEnt.UserRoles.Add(new UserRoles { user_id = _usrObj.id, role_id = _rlobj.id });
                dbEnt.SaveChanges();

                ISCityFramework.SendMail(_usrObj, dbEnt);
                return RedirectToAction("SubCompanyList");
            }

            ModelState.AddModelError("", "Управляющая коомпания с таким Наименованием или Email уже существует.");
            return View(sc);
        }

        public ActionResult SubCompanyList()
        {
            var scList = dbEnt.SubCompany.ToList();
            return View(scList);
        }

        public ActionResult EditSubCompany(int id)
        {
            var sc = dbEnt.SubCompany.Where(s => s.id == id).Select(s => s).FirstOrDefault();
            var usr = dbEnt.Users.Where(u => u.subCompany_id == id).Select(u => u).FirstOrDefault();
            SubCompanyModel msm = new SubCompanyModel {Email = usr.Email, FirstName = usr.FirstName, HouseNumber = sc.HouseNumber, Name = sc.Name, SecondName = usr.SecondName, Street = sc.Street, ThirdName = usr.ThirdName };
            return View(msm);
        }

        [HttpPost]
        public ActionResult EditSubCompany(SubCompanyModel _sc, int user_id, int sc_id)
        {
            var sc = dbEnt.SubCompany.Where(s => s.id == user_id).Select(s => s).FirstOrDefault();
            var usr = dbEnt.Users.Where(u => u.subCompany_id == sc_id).Select(u => u).FirstOrDefault();

            sc.Name = _sc.Name;
            sc.Street = _sc.Street;
            sc.HouseNumber = _sc.HouseNumber;

            usr.Email = _sc.Email;
            usr.FirstName = _sc.FirstName;
            usr.SecondName = _sc.SecondName;
            usr.ThirdName = _sc.ThirdName;

            dbEnt.SaveChanges();

            return RedirectToAction("SubCompanyList");
        }
    }
}