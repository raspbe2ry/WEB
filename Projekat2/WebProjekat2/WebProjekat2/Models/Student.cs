using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjekat2.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        public string FullName { get; set; }
        public KarateTitle Title { get; set; }
        public DateTime TrainingDate { get; set; }

        public virtual ICollection<StudentTraining> Trainings { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<BeltEarning> BeltEarnings { get; set; }

        public Student()
        {
            Trainings = new List<StudentTraining>();
            BeltEarnings = new List<BeltEarning>();
        }
    }
}
