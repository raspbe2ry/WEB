using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebProjekat2.BL;
using WebProjekat2.BModels;
using WebProjekat2.BModels.FrontVue;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebProjekat2.ApiControllers
{
    [Consumes("application/json")]
    [Route("api/Pools")]
    public class PoolsController : Controller
    {
        private ApiPoolManager poolManager;

        public PoolsController(ApiPoolManager poolManager)
        {
            this.poolManager = poolManager;
        }

        [Route("getActual")]
        [HttpGet]
        public JsonResult GetActualPool()
        {
            List<QuestionBasic> actualQuestions = poolManager.GetActualPool();

            return Json(actualQuestions);
        }

        [Route("evaluatePool")]
        [HttpPost]
        public JsonResult EvaluatePool([FromBody] PoolVue pool)
        {
            var finishedPool = poolManager.EvaluatePool(pool);

            return Json(finishedPool);
        }
    }
}
