using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat2.BModels;

namespace WebProjekat2.ViewModels
{
    public class TrainingViewModel
    {
        public TrainingBasic TrainingBasic { get; set; }
        public IEnumerable<SelectListItem> Trainers { get; set; }
        public string HtmlClass { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
    }
}
