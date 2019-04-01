using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat2.BModels;
using WebProjekat2.Models;

namespace WebProjekat2.ViewModels
{
    public class BeltEarningViewModel
    {
        public BeltEarningBasic BeltEarningBasic { get; set; }
        public string HtmlClass { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public IEnumerable<SelectListItem> Trainers { get; set; }
        public int Trainer { get; set; }
    }
}
