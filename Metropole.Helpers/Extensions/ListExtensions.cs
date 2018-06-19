using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Metropole.Helpers
{
    static class ListExtensions
    {
        public static List<SelectListItem> AddPrompt(this List<SelectListItem> list)
        {
            var prompt = new SelectListItem(){ Text = "Please select one", Value = "-1" };

            list.Insert(0, prompt);

            return list;
                
        }

    }
}
