using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Metropole.Helpers;
using System.Web.UI;
using Microsoft.AspNet.Identity;

namespace Metropole.Controllers
{
    [Authorize]
    public class SurveysController : Controller
    {
        private readonly MetropoleContext _db;

        public SurveysController()
        {
            _db = new MetropoleContext();
        }

        [HttpGet]
        [OutputCache(Location = OutputCacheLocation.None)]
        public ActionResult Index()
        {
            var surveys = _db.Surveys.Include("Responses").Include("Questions").OrderBy(s => s.Name).ToList();
            return View(surveys);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var survey = new Survey
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14)
            };

            return View(survey);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Survey survey, string action)
        {
            if (ModelState.IsValid)
            {
                survey.CreatedByUserId = User.Identity.GetUserId();

                survey.Questions.ForEach(q => q.CreatedOn = q.ModifiedOn = DateTime.Now);
                _db.Surveys.Add(survey);
                _db.SaveChanges();
                TempData["info"] = "The survey was successfully created!";
                return RedirectToAction("Edit", new { id = survey.Id });
            }
            else
            {
                TempData["error"] = "An error occurred while attempting to create this survey.";
                return View(survey);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var survey = _db.Surveys.Include("Questions").Single(x => x.Id == id);
            survey.Questions = survey.Questions.OrderBy(q => q.Priority).ToList();
            return View(survey);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Survey model)
        {
            foreach (var question in model.Questions)
            {
                question.SurveyId = model.Id;

                if (question.Id == 0)
                {
                    question.CreatedOn = DateTime.Now;
                    question.ModifiedOn = DateTime.Now;
                    _db.Entry(question).State = EntityState.Added;
                }
                else
                {
                    question.ModifiedOn = DateTime.Now;
                    _db.Entry(question).State = EntityState.Modified;
                    _db.Entry(question).Property(x => x.CreatedOn).IsModified = false;
                }
            }
            // Only created user can edit.
            model.CreatedByUserId = User.Identity.GetUserId();
            _db.Entry(model).State = EntityState.Modified;
            _db.SaveChanges();
            TempData["info"] = "The survey was successfully updated!";
            return RedirectToAction("Edit", new { id = model.Id });
            //return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Survey survey)
        {
            _db.Entry(survey).State = EntityState.Deleted;
            _db.SaveChanges();
            TempData["info"] = "The survey was successfully deleted!";
            // return RedirectToAction("Index", new { id = survey.Id , time= DateTime.Now.Ticks});
            return Json(Url.Action("Index", "Surveys"));

        }
    }
}
