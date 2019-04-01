using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebProjekat2.BModels;
using WebProjekat2.IBL;

namespace WebProjekat2.Controllers
{
    [Produces("application/json")]
    [Route("api/Trainers")]
    public class TrainersController : Controller
    {
        ITrainer trainerManager { get; set; }

        public TrainersController(ITrainer trainerManager)
        {
            this.trainerManager = trainerManager;
        }

        [Route("addTrainer")]
        [HttpPost]
        public ActionResult AddTrainer([FromBody]TrainerBasic trainerBasic)
        {
            int res = -1;

            res = trainerManager.AddTrainer(trainerBasic);

            if (res != -1)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("deleteTrainer")]
        [HttpDelete]
        public ActionResult DeleteTrainer(int trainerId)
        {
            bool res = false;

            res = trainerManager.DeleteTrainer(trainerId);

            if (res)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("allTrainers")]
        [HttpGet]
        public ActionResult GetAllTrainers()
        {
            var res = trainerManager.GetAllTrainers();

            if (res !=null)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("getTrainer")]
        [HttpGet]
        public ActionResult GetTrainer(int trainerId)
        {
            TrainerBasic trainer = trainerManager.GetTrainer(trainerId);

            if (trainer != null)
            {
                return Ok(trainer);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}