using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Metropole.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Metropole;

namespace SurveyTool.Controllers
{
    [Authorize]
    public class ResponsesController : Controller
    {
        private readonly MetropoleContext _db;

        public ResponsesController()
        {
            _db = new MetropoleContext();
        }

        [HttpGet]
        public ActionResult Index(int surveyId)
        {
            var responses = _db.Responses
                               .Include("Survey")
                               .Include("Answers")
                               .Include("Answers.Question")
                               .Where(x => x.SurveyId == surveyId)
                               .Where(x => x.CreatedBy == User.Identity.Name)
                               .OrderByDescending(x => x.CreatedOn)
                               .ThenByDescending(x => x.Id)
                               .ToList();

            return View(responses);
        }

        [HttpGet]
        public ActionResult Details(int surveyId, int id)
        {
            var response = _db.Responses
                              .Include("Survey")
                              .Include("Answers")
                              .Include("Answers.Question")
                              .Where(x => x.SurveyId == surveyId)
                              //.Where(x => x.CreatedBy == User.Identity.Name)
                              .Single(x => x.Id == id);

            response.Answers = response.Answers.OrderBy(x => x.Question.Priority).ToList();
            return View(response);
        }

        [HttpGet]
        public ActionResult Create(int surveyId)
        {

            // Filter on the given Survey.
            // Only for active questions - in priority order.

            var survey = _db.Surveys
                            .Where(s => s.Id == surveyId)
                            .Select(s => new
                                {
                                    Survey = s,
                                    Questions = s.Questions
                                                 .Where(q => q.IsActive)
                                                 .OrderBy(q => q.Priority)
                                })
                             .AsEnumerable()
                             .Select(x =>
                                 {
                                     x.Survey.Questions = x.Questions.ToList();
                                     return x.Survey;
                                 })
                             .Single();

            return View(survey);
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }

        }


        [HttpPost]
        public ActionResult Create(int surveyId, string action, Response model)
        {

            // Before saving the current response - need to delete any existing responses for ANYONE who is 
            // registered at the current user's address.
            string currentUserId = User.Identity.GetUserId();
            // Get Address for the current user.
            ApplicationUser currentUser = _db.Users.FirstOrDefault(u => u.Id == currentUserId);
            var addressId = currentUser.AddressId;
            // Get all users at this address.
            var userIdsAtAddress = _db.Users.Where(u => u.AddressId == addressId).Select( u => u.Id).ToList();

            // Find all responses for this survey by any of the users at this address.
            var responsesToDelete = _db.Responses.Where(r => r.SurveyId == surveyId && userIdsAtAddress.Contains(r.CreatedByUserId)).ToList();
            if (responsesToDelete.Count > 0)
            {
                _db.Responses.RemoveRange(responsesToDelete);
                _db.SaveChanges();
            }

            // Add the latest response.
            model.Answers = model.Answers.Where(a => !String.IsNullOrEmpty(a.Value)).ToList();
            model.SurveyId = surveyId;
            model.CreatedByUserId = User.Identity.GetUserId();
            model.CreatedBy = UserManager.FindById(model.CreatedByUserId).Name;
            model.CreatedOn = DateTime.Now;
            _db.Responses.Add(model);
            _db.SaveChanges();

            TempData["success"] = "Your response was successfully saved!";

            return RedirectToAction("Index", "Surveys");
        }

        [HttpPost]
        public ActionResult Delete(int surveyId, int id, string returnTo)
        {
            var response = new Response() { Id = id, SurveyId = surveyId };
            _db.Entry(response).State = EntityState.Deleted;
            _db.SaveChanges();
            return Redirect(returnTo ?? Url.RouteUrl("Default"));
        }
    }
}