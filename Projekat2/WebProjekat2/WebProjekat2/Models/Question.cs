using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjekat2.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsActual { get; set; }

        [InverseProperty("Question")]
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<PoolQuestion> Pools { get; set; }

        public virtual ICollection<AnsweredQuestion> AnsweredQuestions { get; set; }

        public Question()
        {
            Answers = new List<Answer>();
            Pools = new List<PoolQuestion>();
            AnsweredQuestions = new List<AnsweredQuestion>();
        }
    }
}
