using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjekat2.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Question Question {get; set;}

        public virtual ICollection<AnsweredQuestion> AnsweredQuestions { get; set; }

        public Answer()
        {
            AnsweredQuestions = new List<AnsweredQuestion>();
        }
    }
}
