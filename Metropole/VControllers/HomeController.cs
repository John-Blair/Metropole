using Metropole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Metropole.Helpers;

namespace Metropole.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }

        public ActionResult EmailAdmin()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmailAdmin(EmailAdminViewModel model)
        {
            // Test error handling. throw new DivideByZeroException();

            GMailService.SendMail(EnvironmentSecret.Instance.EmailAdmin, model.Subject, "User:" + User.Identity.Name + " <br/>"  + model.Body);
            return RedirectToAction("Index");
        }




    }
}