using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebProjekat2.BModels;
using WebProjekat2.IBL;
using WebProjekat2.Models;

namespace WebProjekat2.BL
{
    public class PoolManager : IPool
    {
        public UnitOfWork unitOfWork;

        public PoolManager(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public List<PoolBasic> GetPools(DateRange dateRange)
        {
            var allPools = (from pool in unitOfWork.PoolRepository.Get(null, null, "Questions").ToList()
                    join pq in unitOfWork.PoolQuestionRepository.Get(null, null, "Question,Pool").ToList() on pool.Id equals pq.PoolId
                    join question in unitOfWork.QuestionRepository.Get(null, null, "Answers").ToList() on pq.QuestionId equals question.Id
                    join answer in unitOfWork.AnswerRepository.Get(null, null, "Question").ToList() on question.Id equals answer.QuestionId
                    join pqa in unitOfWork.AnsweredQuestionRepository.Get(null, null, "Pool,Question,Answer").ToList() on 
                        new { poolId = pool.Id, questionId = question.Id, answerId = answer.Id } equals 
                        new { poolId = pqa.PoolId, questionId = pqa.QuestionId, answerId = pqa.AnswerId }
                    select new {
                        Id = pool.Id,
                        Submiter = pool.Submiter,
                        SubmitDate = pool.SubmitDate,
                        QuestionId = question.Id,
                        AnswerId = answer.Id,
                        IsCorrect = answer.IsCorrect
                    } into pools
                    group pools by new { pools.Id, pools.Submiter, pools.SubmitDate } into groupedPools
                    select new PoolBasic
                    {
                        Id = groupedPools.Key.Id,
                        Submiter = groupedPools.Key.Submiter,
                        SubmitDate = groupedPools.Key.SubmitDate,
                        NumberOfQuestion = groupedPools.Count(),
                        NumberOfCorrectAnswered = groupedPools.Where(x => x.IsCorrect).Count()
                    }
                    ).ToList();

            foreach(var pool in allPools)
            {
                pool.Result = Math.Round((pool.NumberOfQuestion != 0 ? ((double)pool.NumberOfCorrectAnswered / pool.NumberOfQuestion) : 0),2) * 100;
            }

            return allPools;
        }

        public void AddAnswer(AnswerBasic answerBasic)
        {
            Answer answer = new Answer()
            {
                Text = answerBasic.Text,
                IsCorrect = answerBasic.IsCorrect,
                QuestionId = answerBasic.Question.Id
            };

            unitOfWork.AnswerRepository.Insert(answer);
            unitOfWork.Save();
        }

        public void DeleteAnswer(int answerId)
        {
            Answer answer = unitOfWork.AnswerRepository.GetByID(answerId);
            var answeredQuestion = unitOfWork.AnsweredQuestionRepository.Get(x => x.AnswerId == answerId, null, "").ToList();
            
            foreach(var aq in answeredQuestion)
            {
                unitOfWork.AnsweredQuestionRepository.Delete(aq);
            }

            unitOfWork.AnswerRepository.Delete(answer);
            unitOfWork.Save();
        }

        public int DeleteQuestion(int questionId)
        {
            Question questionToDelete = unitOfWork.QuestionRepository.Get(x=>x.Id == questionId, 
                null, "Answers,AnsweredQuestions").FirstOrDefault();
            int lastQuestionId = 0;

            if (questionToDelete != null)
            {
                lastQuestionId = unitOfWork.QuestionRepository.Get(x => x.Id != questionId, null, "").FirstOrDefault().Id;

                foreach (var answer in questionToDelete.Answers)
                {
                    unitOfWork.AnswerRepository.Delete(answer.Id);
                }

                foreach (var answeredQuestion in questionToDelete.AnsweredQuestions)
                {
                    unitOfWork.AnsweredQuestionRepository.Delete(answeredQuestion.Id);
                }

                var poolQuestions = unitOfWork.PoolQuestionRepository.Get(x => x.QuestionId == questionId).ToList();

                foreach(var pq in poolQuestions)
                {
                    var keys = new object[2];
                    keys[0] = pq.PoolId;
                    keys[1] = pq.QuestionId;
                    var poolQuestion = unitOfWork.PoolQuestionRepository.dbSet.Find(keys[0], keys[1]);
                    unitOfWork.PoolQuestionRepository.dbSet.Remove(poolQuestion);
                }

                unitOfWork.QuestionRepository.Delete(questionId);
                unitOfWork.Save();
            }

            return lastQuestionId;
        }

        public int CreateQuestion(QuestionBasic questionBasic)
        {
            Question question = new Question()
            {
                Text = questionBasic.Text,
                IsActual = questionBasic.IsActual
            };
            unitOfWork.QuestionRepository.Insert(question);

            foreach (var answerBasic in questionBasic.Answers)
            {
                Answer answer = new Answer()
                {
                    Text = answerBasic.Text,
                    IsCorrect = answerBasic.IsCorrect,
                    Question = question
                };
                question.Answers.Add(answer);
                unitOfWork.AnswerRepository.Insert(answer);
            }

            unitOfWork.Save();

            return question.Id;
        }

        public QuestionBasic GetQuestion(int questionId)
        {
            return this.GetQuestions(false).Where(x => x.Id == questionId).First();
        }

        public List<QuestionBasic> GetQuestions(bool onlyActual)
        {
            Expression<Func<Question, bool>> lambda = question => question.IsActual;
            var questions = unitOfWork.QuestionRepository.Get(onlyActual? lambda : null, null, "Answers")
                    .ToList()
                .Select(x=> new QuestionBasic{
                    Id = x.Id, 
                    IsActual = x.IsActual,
                    Text = x.Text,
                    Answers = x.Answers.Select(y=> new AnswerBasic() {
                        Id = y.Id,
                        IsCorrect = y.IsCorrect,
                        Text = y.Text
                    }).ToList()
                }).ToList();

            for(int i=1; i<questions.Count-1; i++)
            {
                questions[i].IdOfLast = questions[i - 1].Id;
                questions[i].IdOfNext = questions[i + 1].Id;
            }
            questions[0].IdOfLast = questions[questions.Count - 1].Id;
            questions[questions.Count - 1].IdOfNext = questions[0].Id;

            questions[0].IdOfNext = questions.Count > 1 ? questions[1].Id : questions[0].Id;
            questions[questions.Count - 1].IdOfLast = questions.Count > 1 ? questions[questions.Count - 2].Id : questions[0].Id;

            return questions;
        }

        public bool UpdateQuestion(QuestionBasic question)
        {
            Question questionToUpdate = unitOfWork.QuestionRepository.GetByID(question.Id);
            questionToUpdate.Text = question.Text;
            questionToUpdate.IsActual = question.IsActual;
            unitOfWork.QuestionRepository.Update(questionToUpdate);

            foreach(var answer in question.Answers)
            {
                Answer answerToUpdate = unitOfWork.AnswerRepository.GetByID(answer.Id);
                answerToUpdate.Text = answer.Text;
                answerToUpdate.IsCorrect = answer.IsCorrect;
                unitOfWork.AnswerRepository.Update(answerToUpdate);
            }

            unitOfWork.Save();

            return true;
        }
    }
}
