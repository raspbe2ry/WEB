using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat2.BModels;

namespace WebProjekat2.ViewModels
{
    public class StudentDetailsViewModel
    {
        public List<BeltEarningBasic> BeltEarnings { get; set; }
        public List<TrainingBasic> Trainings { get; set; }
        public BeltEarningBasic BeltEarningToCreate { get; set; }

        public IEnumerable<SelectListItem> Trainers { get; set; }
    }
}
