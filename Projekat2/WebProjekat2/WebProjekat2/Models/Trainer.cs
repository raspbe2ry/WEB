using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjekat2.Models
{
    public class Trainer
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public KarateTitle Title { get; set; }

        [InverseProperty("Trainer")]
        public virtual ICollection<Training> Trainings { get; set; }
        public virtual ICollection<TrainerBeltEarning> BeltEarnings { get; set; }

        public Trainer()
        {
            Trainings = new List<Training>();
            BeltEarnings = new List<TrainerBeltEarning>();
        }
    }
}
