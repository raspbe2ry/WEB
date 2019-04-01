using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjekat2.Models
{
    public class BeltEarning
    {
        [Key]
        public int Id { get; set; }

        public DateTime EarnDate { get; set; }
        public KarateTitle Belt { get; set; }
        public bool Success { get; set; }
        public int StudentId { get; set; }
        
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }

        public virtual ICollection<TrainerBeltEarning> Trainers { get; set; }

        public BeltEarning()
        {
            Trainers = new List<TrainerBeltEarning>();
        }
    }

    public enum KarateTitle{
        White=1, 
        Yellow=2,
        Orange=3,
        Green=4,
        Blue1=5,
        Blue2=6,
        Brown1=7, 
        Brown2=8, 
        Brown3=9,
        Black1=10, 
        Black2=11,
        Black3=12,
        Black4=13,
        Black5=14,
        Black6=15,
        Black7=16,
    }
}
