using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjekat2.BModels
{
    public class PoolBasic
    {
        public int Id { get; set; }
        public string Submiter { get; set; }
        public DateTime SubmitDate { get; set; }
        public int NumberOfQuestion { get; set; }
        public int NumberOfCorrectAnswered { get; set; }
        public double Result { get; set; }

        public List<QuestionBasic> Questions { get; set; }
    }
}
