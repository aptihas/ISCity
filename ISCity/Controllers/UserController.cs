using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISCity.Models;

namespace ISCity.Controllers
{
    [MyRoleAtribute(Roles = "User")]
    public class UserController : Controller
    {
        DBISCityEntities dbEnt = new DBISCityEntities();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewRequest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewRequest(UserRequestModel usrReq)
        {
            var _acc = (from a in dbEnt.Accounts where a.Users.Email == User.Identity.Name select a.Users).FirstOrDefault();
            dbEnt.UserRequests.Add(new UserRequests { mangeCompany_id = usrReq.Category, Message = usrReq.Message, DateOfCreate = DateTime.Now, Closed = false, user_id = _acc.id });
            dbEnt.SaveChanges();
            return View("MyRequestList");
        }

        public ActionResult MyRequestList()
        {
            var _usr = (from a in dbEnt.Accounts where a.Users.Email == User.Identity.Name select a.Users).FirstOrDefault();
            List<UserRequests> _myRequestList = dbEnt.UserRequests.Where(r => r.user_id == _usr.id).Select(r => r).ToList();
            return View(_myRequestList);
        }

        public ActionResult CloseMyRequest(int id)
        {
            var _myRequest = dbEnt.UserRequests.Where(r => r.id == id).Select(r => r).FirstOrDefault();
            _myRequest.Closed = true;
            dbEnt.SaveChanges();

            var _usr = (from a in dbEnt.Accounts where a.Users.Email == User.Identity.Name select a.Users).FirstOrDefault();
            List<UserRequests> _myRequestList = dbEnt.UserRequests.Where(r => r.user_id == _usr.id).Select(r => r).ToList();
            return View("MyRequestList", _myRequestList);
        }

        public ActionResult OpenMyRequest(int id)
        {
            var _myRequest = dbEnt.UserRequests.Where(r => r.id == id).Select(r => r).FirstOrDefault();
            _myRequest.Closed = false;
            var _markObj = dbEnt.RequestsMark.Where(rm => rm.userRequest_id == id).Select(rm => rm).FirstOrDefault();
            _markObj.Mark = null;
            dbEnt.SaveChanges();
            var _usr = (from a in dbEnt.Accounts where a.Users.Email == User.Identity.Name select a.Users).FirstOrDefault();
            List<UserRequests> _myRequestList = dbEnt.UserRequests.Where(r => r.user_id == _usr.id).Select(r => r).ToList();

            return View("MyRequestList", _myRequestList);
        }

        public ActionResult AddMark(int id)
        {
            var _markObj = dbEnt.RequestsMark.Where(rm => rm.userRequest_id == id).Select(rm => rm).FirstOrDefault();
            ViewBag.RequestId = id;
            if (_markObj != null)
            {
                _markObj.Mark = null;
                return View();
            }
            else
            {
                dbEnt.RequestsMark.Add(new RequestsMark { userRequest_id = id });
                dbEnt.SaveChanges();
                return View();
            }
        }

        [HttpPost]
        public ActionResult AddMark(int id, int mark)
        {
            var _rm = dbEnt.RequestsMark.Where(rm => rm.id == id).Select(rm => rm).FirstOrDefault();
            _rm.Mark = mark;
            dbEnt.SaveChanges();

            var _usr = (from a in dbEnt.Accounts where a.Users.Email == User.Identity.Name select a.Users).FirstOrDefault();
            List<UserRequests> _myRequestList = dbEnt.UserRequests.Where(r => r.user_id == _usr.id).Select(r => r).ToList();

            return View("MyRequestList", _myRequestList);
        }
    }
}