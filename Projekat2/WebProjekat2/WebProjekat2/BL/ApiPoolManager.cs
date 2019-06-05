using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat2.BModels;
using WebProjekat2.BModels.FrontVue;
using WebProjekat2.IBL;
using WebProjekat2.Models;

namespace WebProjekat2.BL
{
    public class ApiPoolManager
    {
        public UnitOfWork unitOfWork;

        public ApiPoolManager(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public List<QuestionBasic> GetActualPool()
        {
            List<QuestionBasic> questions = unitOfWork.QuestionRepository.Get(x => x.IsActual, null, "Answers")
                .ToList()
                .Select(x => new QuestionBasic()
                {
                    Id = x.Id,
                    Text = x.Text,
                    Answers = x.Answers.Select(y => new AnswerBasic()
                    {
                        Text = y.Text,
                        IsCorrect = y.IsCorrect,
                        Id = y.Id
                    }).ToList()
                }).ToList();

            return questions;
        }

        public FinishedPoolVue EvaluatePool(PoolVue poolVue)
        {
            List<AnsweredQuestionVue> answeredQuestionVues = new List<AnsweredQuestionVue>();

            Pool pool = new Pool()
            {
                SubmitDate = DateTime.Now,
                Submiter = poolVue.Name
            };

            foreach (var q in poolVue.Questions)
            {
                Question question  = unitOfWork.QuestionRepository.GetByID(q.Id);
                PoolQuestion poolQuestion = new PoolQuestion()
                {
                    Pool = pool,
                    Question = question
                };
                unitOfWork.PoolQuestionRepository.Insert(poolQuestion);

                Answer answer = unitOfWork.AnswerRepository.GetByID(q.Result);
                AnsweredQuestion answeredQuestion = new AnsweredQuestion()
                {
                    Answer = answer,
                    Pool = pool,
                    Question = question
                };
                unitOfWork.AnsweredQuestionRepository.Insert(answeredQuestion);

                Answer correctAnswer = unitOfWork.AnswerRepository.Get(x => x.QuestionId == q.Id && x.IsCorrect, null, "").FirstOrDefault();
                AnsweredQuestionVue aqv = new AnsweredQuestionVue()
                {
                    QuestionId = q.Id,
                    CorrectAnswerId = correctAnswer != null ? correctAnswer.Id : 0,
                    GivenAnswerId = q.Result,
                    IsCorrect = ((correctAnswer != null ? correctAnswer.Id : 0) == q.Result)
                };
                answeredQuestionVues.Add(aqv);
            }

            unitOfWork.PoolRepository.Insert(pool);
            unitOfWork.Save();

            double result = (double)answeredQuestionVues.Where(x=>x.IsCorrect).Count() / answeredQuestionVues.Count;
            return new FinishedPoolVue()
            {
                FinalResult = Math.Round(result, 2)*100,
                AnsweredQuestions = answeredQuestionVues
            };
        }
    }
}
