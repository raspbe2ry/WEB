using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebProjekat2.BModels;
using WebProjekat2.IBL;

namespace WebProjekat2.Controllers
{
    [Produces("application/json")]
    [Route("api/Trainings")]
    public class TrainingsController : Controller
    {
        ITraining trainingManager { get; set; }

        public TrainingsController(ITraining trainingManager)
        {
            this.trainingManager = trainingManager;
        }

        [Route("addTraining")]
        [HttpPost]
        public ActionResult AddTraining(TrainingBasic trainingBasic)
        {
            var res = trainingManager.AddTraining(trainingBasic);
            if(res != -1)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("addTrainer")]
        [HttpPost]
        public ActionResult AddTrainer(int trainingId, int trainerId)
        {
            var res = trainingManager.AddTrainer(trainerId, trainerId);
            if (res == 1)
            {
                return Ok(res);
            }
            else if (res != 404)
            {
                return BadRequest();
            }
            else
            {
                return NotFound();
            }
        }

        [Route("addDescription")]
        [HttpPost]
        public ActionResult AddDescription(string description, int trainingId)
        {
            var res = trainingManager.AddDescription(description, trainingId);
            if (res)
                return Ok();
            else
                return BadRequest();
        }

        [Route("getAllTrainings")]
        [HttpGet]
        public ActionResult GetAllTrainings(DateRange dateRange)
        {
            var res = trainingManager.GetAllTrainings(dateRange);
            return Ok(res);
        }
    }
}