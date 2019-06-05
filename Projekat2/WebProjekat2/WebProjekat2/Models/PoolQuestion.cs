using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjekat2.Models
{
    public class PoolQuestion
    {
        public int PoolId { get; set; }
        public virtual Pool Pool { get; set; }

        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}
