using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjekat2.BL
{
    public class ReportModel
    {
        public string Month { get; set; }
        public int NewStudents { get; set; }
        public int NewBeltEarnings { get; set; }
        public int NumOfTrainings { get; set; }
        public int MaxNumOnTraining { get; set; }
        public DateTime? DateWithMaxStudents { get; set; }
        public int MinNumOnTraining { get; set; }
        public DateTime? DateWithMinStudents { get; set; }
        public double AverageOnTraining { get; set; }
        public List<BeltEarningsStruct> NewBeltEarningsByBelt { get; set; }
    }

    public struct BeltEarningsStruct
    {
        public string belt { get; set; }
        public int count { get; set; }
    }
}
