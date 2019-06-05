using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat2.BModels;

namespace WebProjekat2.IBL
{
    interface IPool
    {
        List<PoolBasic> GetPools(DateRange dateRange);
        List<QuestionBasic> GetQuestions(bool onlyActual);
        QuestionBasic GetQuestion(int questionId);
    }
}
