﻿using System;
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
        // GET: User
        
        public ActionResult Index()
        {
            return View();
        }
    }
}