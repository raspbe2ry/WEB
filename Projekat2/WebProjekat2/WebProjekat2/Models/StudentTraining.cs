using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjekat2.Models
{
    public class StudentTraining
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int TrainingId { get; set; }
        public virtual Training Training { get; set; }
    }
}
