using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjekat2.Models
{
    public class Training
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public int Visit { get; set; }

        public int TrainerId { get; set; }

        [ForeignKey("TrainerId")]
        public virtual Trainer Trainer{get; set;}
        public virtual ICollection<StudentTraining> Students { get; set; }

        public Training()
        {
            Students = new List<StudentTraining>();
        }
    }
}
