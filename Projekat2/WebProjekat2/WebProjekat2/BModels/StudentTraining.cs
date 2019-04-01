using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjekat2.BModels
{
    public class StudentTraining
    {
        public string StudentName { get; set; }
        public int StudentId { get; set; }
        public int TrainingId { get; set; }
        public bool Present { get; set; }
    }
}
