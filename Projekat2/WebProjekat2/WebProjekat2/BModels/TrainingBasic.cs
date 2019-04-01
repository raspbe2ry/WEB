using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjekat2.BModels
{
    public class TrainingBasic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Visit { get; set; }

        public int TrainerId { get; set; }
        public string TrainerName { get; set; }
    }
}
