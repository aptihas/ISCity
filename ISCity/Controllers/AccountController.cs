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

                    if (_usr.EmailConfirm != true)
                    {
                        _usr.EmailConfirm = true;
                        dbEnt.SaveChanges();
                    }
                    if (_usr.manageCompany_id != null)
                    {
                        return RedirectToAction("Index", "User");
                    }
                    else if(_usr.subCompany_id!=null)
                    {
                        return RedirectToAction("Index", "User");
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


                dbEnt.Accounts.Add(new Accounts { password = GetPass(), user_id = _usr.id });
                dbEnt.SaveChanges();
                SendMail(_usr);
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

        private void SendMail(Users user)
        {
            string _pass = dbEnt.Accounts.Where(a => a.user_id == user.id).Select(a=>a.password).FirstOrDefault();
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("otdel_rsis@mail.ru", "ИС \"Грозный\"");
            // кому отправляем
            MailAddress to = new MailAddress(user.Email);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Подтверждение Email";
            // текст письма
            m.Body = "<h2>Здравствуйте, "+user.SecondName+ "!</h2><h4>Ваши учетные данные от системы ИС Грозный</h4><p>Логин:" + user.Email+"<br>Пароль:" + _pass+"<br/><br/>Пока вы не выполните вход, Ваш Email не будет подтвержден.</p>";
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 25);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("otdel_rsis@mail.ru", "razrab0tka");
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(m);
        }

        private string GetPass()
        {
            int[] arr = new int[16]; // сделаем длину пароля в 16 символов
            string Password = "";

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rnd.Next(33, 125);
                Password += (char)arr[i];
            }
            return Password;
        }

        private string GetLogin()
        {
            int[] arr = new int[8]; // сделаем длину пароля в 16 символов
            string Password = "";

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rnd.Next(33, 125);
                Password += (char)arr[i];
            }
            return Password;
        }

    }
}