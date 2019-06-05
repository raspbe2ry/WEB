using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjekat2.BModels
{
    public class QuestionBasic
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsActual { get; set; }
        public int IdOfLast { get; set; }
        public int IdOfNext { get; set; }
        public string Result { get; set; }

        public List<PoolBasic> Pools {get; set;}
        public List<AnswerBasic> Answers {get; set;}
    }
}
