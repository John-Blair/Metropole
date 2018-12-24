using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metropole.Models
{
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    /// <summary>
    /// Report is: Survey with all the Responses i.e. Questions + all the Answers.
    /// </summary>
    public class ReportViewModel
    {
        // Create a report for this survey.
        public Survey Survey { get; set; }

        // Flat responses allows for 1 response per flat.
        // Multiple registered users per flat - last response is the one used.
        public int FlatResponses { get; set; }

        // TBD: Commercial units excluded.
        public int TotalFlats { get; set; }

        // A response from a third of total flats is required to form a quorum.
        public int Quorum { get; set; }
        public bool QuorumStatus
        {
            get { return FlatResponses >= Quorum; }
        }


        // Survey Questions - each question has all the answers given.
        public List<QuestionViewModel> Questions { get; set; }
    }

    public class QuestionViewModel
    {
        /// <summary>
        /// Question Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Question Body
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Type of Question only Yes/No and Number types supported.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// All the answers.
        /// </summary>
        public List<Answer> Answers { get; set; }

        /// <summary>
        /// For yes/no questions: score gives the number of Yes answers.
        /// Otherwise: sum of individual answers numbers.
        /// </summary>
        public int Score
        {
            get
            {
                if (Type == "Yes/No")
                    return Answers.Sum(x => x.Value == "Yes" ? 1 : 0);

                if (Type == "Number")
                {
                    return Answers.Sum(x =>
                    {
                        int num;
                        Int32.TryParse(x.Value, out num);
                        return num;
                    });
                }

                return 0;
            }
        }


        // Total number of answers to the question.
        public int Total
        {
            get { return Answers.Count(); }
        }

        // Percentage of yes answers.
        public double Percentage
        {
            get { return (double)Score / (double)Total; }
        }

        public string PercentageString
        {
            get { return Answers.Any() ? String.Concat((Int32)(Percentage * 100), "%") : "0%"; }
        }
    }
}
