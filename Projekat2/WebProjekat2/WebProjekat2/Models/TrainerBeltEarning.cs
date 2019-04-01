using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjekat2.Models
{
    public class TrainerBeltEarning
    {
        public int TrainerId { get; set;}
        public Trainer Trainer { get; set; }

        public int BeltEarningId { get; set; }
        public virtual BeltEarning BeltEarning { get; set; }
    }
}
