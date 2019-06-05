using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjekat2.BModels.FrontVue
{
    public class AnsweredQuestionVue
    {
        public int QuestionId { get; set; }
        public int CorrectAnswerId { get; set; }
        public int GivenAnswerId { get; set; }
        public bool IsCorrect { get; set; }
    }
}
