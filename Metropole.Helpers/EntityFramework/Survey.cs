using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.IO;

namespace Metropole.Helpers
{
    public class Survey
    {
        public Survey()
        {
            Questions = new List<Question>();
            Responses = new List<Response>();
        }

        [Key]
        public int Id { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Name { get; set; }

        public string CreatedByUserId { get; set; }

        public DateTime StartDate { get; set; }
       
        public DateTime EndDate { get; set; }

        public List<Question> Questions { get; set; }

        public List<Response> Responses { get; set; }

        public bool IsActive
        {
            get { return StartDate < DateTime.Now && EndDate > DateTime.Now; }
        }

        public string ToJson()
        {
            var js = JsonSerializer.Create(new JsonSerializerSettings());
            var jw = new StringWriter();
            js.Serialize(jw, this);
            return jw.ToString();
        }
    }
}
