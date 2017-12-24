using ISCity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISCity.Controllers
{
    [MyRoleAtribute(Roles = "Admin")]
    public class AdminController : Controller
    {
        Random rnd = new Random();
        DBISCityEntities dbEnt = new DBISCityEntities();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManageCompanyList()
        {
            var mcList = dbEnt.ManageCompany.ToList();
            return View(mcList);
        }


        public ActionResult AddManageCompany()
        {
            return View();
        }

        public ActionResult EditManageCompany(int id)
        {
            var mc = dbEnt.ManageCompany.Where(m => m.id == id).Select(m => m).FirstOrDefault();
            var usr = dbEnt.Users.Where(u => u.manageCompany_id == id).Select(u => u).FirstOrDefault();

            ManageCompanyModel mcm = new ManageCompanyModel { Category=mc.Category, Email=usr.Email, FirstName=usr.FirstName, HouseNumber=mc.HouseNumber, Name=mc.Name, SecondName=usr.SecondName, Street=mc.Street, ThirdName=usr.ThirdName};

            ViewBag.UserID = usr.id;
            ViewBag.MCID = mc.id;

            return View(mcm);
        }

        [HttpPost]
        public ActionResult EditManageCompany(ManageCompanyModel mc, int user_id, int mc_id)
        {
            var _usr = dbEnt.Users.Where(u => u.id == user_id).Select(u => u).FirstOrDefault();
            var _mc = dbEnt.ManageCompany.Where(m => m.id == mc_id).Select(m => m).FirstOrDefault();

            _mc.Name = mc.Name;
            _mc.Street = mc.Street;
            _mc.Category = mc.Category;
            _mc.HouseNumber = mc.HouseNumber;

            _usr.Email = mc.Email;
            _usr.FirstName = mc.FirstName;
            _usr.SecondName = mc.SecondName;
            _usr.ThirdName = mc.ThirdName;

            dbEnt.SaveChanges();

            var mcList = dbEnt.ManageCompany.ToList();
            return RedirectToAction("ManageCompanyList", mcList);
        }

        [HttpPost]
        public ActionResult AddManageCompany(ManageCompanyModel mc)
        {
            var categoryList = dbEnt.ManageCompany.Select(s => s.Category);
            var nameList = dbEnt.ManageCompany.Select(s => s.Name);
            var emailList = dbEnt.Users.Select(s => s.Email);

            if (!categoryList.Contains(mc.Category) && !nameList.Contains(mc.Name) && !emailList.Contains(mc.Email))
            {
                ManageCompany _mcObj = new ManageCompany { Name = mc.Name, Category = mc.Category, Street=mc.Street, HouseNumber=mc.HouseNumber };
                dbEnt.ManageCompany.Add(_mcObj);
                dbEnt.SaveChanges();

                Users _usrObj = new Users { FirstName = mc.FirstName, SecondName = mc.SecondName, ThirdName = mc.ThirdName, Email = mc.Email, EmailConfirm=false,manageCompany_id= _mcObj.id };
                dbEnt.Users.Add(_usrObj);
                dbEnt.SaveChanges();

                var roleList = dbEnt.Roles.Select(r => r.Name);
                Roles _rlobj;
                if (!roleList.Contains("ManageCompany"))
                {
                    _rlobj = new Roles { Name = "ManageCompany" };
                    dbEnt.Roles.Add(_rlobj);
                    dbEnt.SaveChanges();
                }
                else
                {
                    _rlobj = dbEnt.Roles.Where(r => r.Name == "ManageCompany").Select(r => r).FirstOrDefault();
                }

                dbEnt.Accounts.Add(new Accounts { user_id = _usrObj.id, password = ISCityFramework.GetPass(rnd) });
                dbEnt.SaveChanges();

                dbEnt.UserRoles.Add(new UserRoles { user_id = _usrObj.id, role_id = _rlobj.id });
                dbEnt.SaveChanges();

                ISCityFramework.SendMail(_usrObj, dbEnt);

                return RedirectToAction("Index", "Admin",null);
            }
            else
            {
                ModelState.AddModelError("", "Организация с таким Наименованием, Категорией или Email уже существует.");
            }

            return View(mc);
        }
    }
}