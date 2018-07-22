using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Metropole.Helpers

{
    public class Response
    {
        [Key]
        public int Id { get; set; }

        public int SurveyId { get; set; }

        public Survey Survey { get; set; }

        public string CreatedBy { get; set; }
        public string CreatedByUserId { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<Answer> Answers { get; set; }

       

        public int GetYesNoQuestionCount()
        {
            return Answers == null ? 0 : Answers.Count(a => a.Question.Type == "Yes/No");
        }

        public int GetYesNoAnswerCount()
        {
            return Answers == null ? 0 : Answers.Sum(x => x.Score);
        }

       

        public double CalculateScore()
        {
            var questions = GetYesNoQuestionCount();
            var answers = GetYesNoAnswerCount();

            if (questions == 0 || answers == 0)
                return 0.0;

            return (double)answers / (double)questions;
        }
    }
}
