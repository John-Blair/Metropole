﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Metropole.Helpers
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        public int SurveyId { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public string Body { get; set; }

        public int Priority { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public List<Answer> Answers { get; set; }
    }
}
