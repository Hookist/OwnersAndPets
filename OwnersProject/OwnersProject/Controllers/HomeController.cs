﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OwnersProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "First Page";

            return View();
        }
    }
}
