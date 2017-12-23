using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ISCity.Models;
using System.Web.Mvc;
using System.Web.Security;
using System.Net.Mail;
using System.Net;

namespace ISCity.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        Random rnd = new Random();
        DBISCityEntities dbEnt = new DBISCityEntities();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LogInModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                Accounts _acc = null;
                using (DBISCityEntities db = new DBISCityEntities())
                {
                    _acc = db.Accounts.FirstOrDefault(u => u.Users.Email == model.Login && u.password == model.Password);
                }
                if (_acc != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Login, true);

                    var _usr = (from u in dbEnt.Users
                                where u.id == _acc.user_id
                                select u).FirstOrDefault();

                    var _usrRoles = dbEnt.UserRoles.Where(r => r.user_id == _usr.id).Select(s => s.Roles.Name);

                    if (_usr.EmailConfirm != true)
                    {
                        _usr.EmailConfirm = true;
                        dbEnt.SaveChanges();
                    }



                    if (_usrRoles.Contains("ManageCompany"))
                    {
                        return RedirectToAction("Index", "ManageCompany");
                    }
                    else if(_usrRoles.Contains("SubCompany"))
                    {
                        return RedirectToAction("Index", "SubCompany");
                    }
                    else if (_usrRoles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "User");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel register)
        {
            Users _usr = new Users
            {
                FirstName = register.FirstName,
                SecondName = register.SecondName,
                ThirdName = register.ThirdName,
                Street = register.Street,
                HouseNumber = register.HouseNumber,
                KorpusNumber = register.KorpusNumber,
                RoomNumber = register.RoomNumber,
                Email = register.Email,
                EmailConfirm = false,
                Telephone = register.Telephone
            };
            var emails = dbEnt.Users.Select(s => s.Email);
            if (emails.Contains(_usr.Email))
            {
                ModelState.AddModelError("", "Пользователь с указанным email уже зарегестрирован");
                return View(register);
            }
            try
            {
                var _roleUserContains = (from r in dbEnt.Roles
                                 where r.Name == "User"
                                 select r).FirstOrDefault();
                if (_roleUserContains == null)
                {
                    dbEnt.Roles.Add(new ISCity.Models.Roles { Name = "User" });
                    dbEnt.SaveChanges();
                }
                var _roleUser = (from r in dbEnt.Roles
                                 where r.Name == "User"
                                 select r).FirstOrDefault();
                dbEnt.Users.Add(_usr);
                dbEnt.SaveChanges();

                dbEnt.UserRoles.Add(new UserRoles { user_id = _usr.id, role_id = _roleUser.id });
                dbEnt.SaveChanges();

                dbEnt.Accounts.Add(new Accounts { password = ISCityFramework.GetPass(rnd), user_id = _usr.id });
                dbEnt.SaveChanges();
                ISCityFramework.SendMail(_usr,dbEnt);
                return RedirectToAction("Index", "User");
            }
            catch
            {
                var _usrRls = from r in dbEnt.UserRoles
                              where r.user_id == _usr.id
                              select r;
                dbEnt.UserRoles.RemoveRange(_usrRls);
                dbEnt.SaveChanges();

                var _usrAccs = from r in dbEnt.Accounts
                              where r.user_id == _usr.id
                              select r;
                dbEnt.Accounts.RemoveRange(_usrAccs);
                dbEnt.SaveChanges();

                dbEnt.Users.Remove(_usr);
                dbEnt.SaveChanges();
                ModelState.AddModelError("", "Что-то случилось, Вы не прошли регестрацию. Повторите позже.");
            }
            return View(register);


            //нужно добавить подтверждение emaila

        }

    }
}