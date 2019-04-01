using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjekat2.BModels
{

    public class ReportConfiguration
    {
        public bool NewStudents { get; set; }
        public bool NewBeltEarnings { get; set; }
        public bool ByTitle { get; set; }
        public bool NumOfTrainings { get; set; }
        public bool MaxStudents { get; set; }
        public bool MinStudents { get; set; }
        public string Month { get; set; }
    }
}
