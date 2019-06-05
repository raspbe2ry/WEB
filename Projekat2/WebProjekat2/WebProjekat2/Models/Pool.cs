using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjekat2.Models
{
    public class Pool
    {
        [Key]
        public int Id { get; set; }
        public DateTime SubmitDate { get; set; }
        public string Submiter { get; set; }

        public virtual ICollection<PoolQuestion> Questions { get; set; }
        public virtual ICollection<AnsweredQuestion> AnsweredQuestions { get; set; }

        public Pool()
        {
            Questions = new List<PoolQuestion>();
            AnsweredQuestions = new List<AnsweredQuestion>();
        }
    }
}
