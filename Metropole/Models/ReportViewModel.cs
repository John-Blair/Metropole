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
    /// Report is: Survey with all the Responses.
    /// </summary>
    public class ReportViewModel
    {
        public Survey Survey { get; set; }

        public int FlatResponses { get; set; }
        public int TotalFlats { get; set; }
        public int Quorum { get; set; }
        public bool QuorumStatus
        {
            get { return FlatResponses >= Quorum; }
        }



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
        /// Type of Question
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// All the answers.
        /// </summary>
        public List<Answer> Answers { get; set; }

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

        public int Total
        {
            get { return Answers.Count(); }
        }

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
