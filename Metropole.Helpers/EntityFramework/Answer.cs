using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Metropole.Helpers
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }

        public int ResponseId { get; set; }

        public Response Response { get; set; }

        public int QuestionId { get; set; }

        public string Value { get; set; }

        public string Comment { get; set; }

        public Question Question { get; set; }

        public int Score
        {
            get
            {
                if (Question != null)
                {
                    if (Question.Type == "Yes/No")
                        return Value == "Yes" ? 1 : 0;

                   
                }

                return 0;
            }
        }

       
    }
}
