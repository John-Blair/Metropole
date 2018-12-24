using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Metropole.Helpers;
using Metropole.Models;
using System.Data.Entity;

namespace Metropole.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly MetropoleContext _db;

        public ReportsController()
        {
            _db = new MetropoleContext();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var surveys = _db.Surveys.Include("Responses").Include("Questions").OrderBy(s => s.Name).ToList();
            return View(surveys);
        }

        public ActionResult Survey(int id)
        {
            var questions = new List<QuestionViewModel>();

            var survey = _db.Surveys.Include("Responses").Include("Responses.Answers").Include("Responses.Answers.Question").Single(s => s.Id == id);

            _db.Questions
               .Where(q => q.SurveyId == id)
               .OrderBy(q => q.Priority)
               .Select(q => new
               {
                   q.Title,
                   q.Body,
                   q.Type,
                   Answers = _db.Answers.Where(a => a.QuestionId == q.Id)
               })
               .ToList()
               .ForEach(r => questions.Add(new QuestionViewModel
               {
                   Title = r.Title,
                   Body = r.Body,
                   Type = r.Type,
                   Answers = r.Answers.ToList()
               }));

            var vm = new ReportViewModel
            {
                Survey = survey,
                Questions = questions,
                Quorum = Metropole.Helpers.Environment.Instance.Quorum,
                TotalFlats = Metropole.Helpers.Environment.Instance.TotalUnits,

            };

            return View(vm);
        }


        [HttpGet]
        public ActionResult Details(int id, int? departmentId, DateTime? startDate, DateTime? endDate)
        {


            return View();

        }



        
    }
}
