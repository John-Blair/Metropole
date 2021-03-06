﻿using Metropole.Helpers;
using Metropole.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Metropole.Controllers
{
    public class DebugController : Controller
    {
        // Get /Debug/TestEmail
        [AllowAnonymous]
        public ActionResult TestEmail()
        {
            var model = new TestEmailViewModel()
            {
                EmailTo = "john_blair@hotmail.com",
                Subject = "Test Email " + DateTime.Now.ToString("yyyy-MMM-dd HH:mm:ss"),
                Body = "Some test body content"
            };
            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult TestEmail(TestEmailViewModel model) {

            var userId = User.Identity.GetUserId();

            GMailService.SendMail(model.EmailTo, model.Subject, model.Body, userId);


            return View();
        }

    }
}