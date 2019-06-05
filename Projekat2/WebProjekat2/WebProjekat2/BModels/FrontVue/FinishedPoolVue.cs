using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjekat2.BModels.FrontVue
{
    public class FinishedPoolVue
    {
        public double FinalResult { get; set; }
        public List<AnsweredQuestionVue> AnsweredQuestions { get; set; }
    }
}
