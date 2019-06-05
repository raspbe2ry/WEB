using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebProjekat2.BL;
using WebProjekat2.BModels;
using WebProjekat2.ViewModels;

namespace WebProjekat2.Controllers
{
    [Route("Pool")]
    public class PoolController : Controller
    {
        public PoolManager poolManager { get; set; }

        public PoolController(PoolManager poolManager)
        {
            this.poolManager = poolManager;
        }

        public ActionResult Index()
        {
            PoolViewModel pvm = new PoolViewModel();

            pvm.Pools = poolManager.GetPools(null);
            pvm.Question = poolManager.GetQuestions(false).First();
            return View(pvm);
        }

        [HttpGet]
        [Route("GetQuestion")]
        public PartialViewResult GetQuestion(int questionId)
        {
            var question = poolManager.GetQuestion(questionId);

            return PartialView("_QuestionPartial", question);
        }

        [HttpPost]
        [Route("UpdateQuestion")]
        public JsonResult UpdateQuestion(QuestionBasic questionBasic)
        {
            poolManager.UpdateQuestion(questionBasic);
            return Json("true");
        }

        [HttpGet]
        [Route("NewQuestionPartial")]
        public PartialViewResult NewQuestionPartial()
        {
            return PartialView("_NewQuestionPartial");
        }

        [HttpPost]
        [Route("AddNewQuestion")]
        public JsonResult AddNewQuestion(QuestionBasic questionBasic)
        {
            int questionId = poolManager.CreateQuestion(questionBasic);

            return Json(new { questionId });
        }

        [HttpPost]
        [Route("DeleteQuestion")]
        public JsonResult DeleteQuestion(int questionId)
        {
            int lastQuestionId = poolManager.DeleteQuestion(questionId);

            return Json(new { lastQuestionId });
        }

        [HttpGet]
        [Route("AddAnswer")]
        public JsonResult AddAnswer(string answerText,bool isAnswerCorrect,int questionId)
        {
            AnswerBasic answerBasic = new AnswerBasic()
            {
                IsCorrect = isAnswerCorrect,
                Text = answerText,
                Question = new QuestionBasic()
                {
                    Id = questionId
                }
            };
            poolManager.AddAnswer(answerBasic);

            return Json("true");
        }

        [HttpPost]
        [Route("DeleteAnswer")]
        public JsonResult DeleteAnswer(int answerId)
        {
            poolManager.DeleteAnswer(answerId);
            
            return Json("true");
        }
    }
}