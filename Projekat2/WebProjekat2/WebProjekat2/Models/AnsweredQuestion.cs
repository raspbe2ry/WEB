using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjekat2.Models
{
    public class AnsweredQuestion
    {
        [Key]
        public int Id { get; set; }

        public int PoolId { get; set; }
        public virtual Pool Pool { get; set; }

        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }

        public int AnswerId { get; set; }
        public virtual Answer Answer { get; set; }
    }
}
