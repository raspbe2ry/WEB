using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebProjekat2.BModels;
using WebProjekat2.IBL;
using WebProjekat2.ViewModels;

namespace WebProjekat2.MvcControllers
{
    [Route("Trainer")]
    public class TrainerController : Controller
    {
        private ITrainer trainerManager { get; set; }

        public TrainerController(ITrainer trainerManager)
        {
            this.trainerManager = trainerManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<TrainerBasic> trainers = trainerManager.GetAllTrainers();

            return View(trainers);
        }

        [HttpGet]
        [Route("GetAll")]
        public JsonResult GetAll()
        {
            List<TrainerBasic> trainers = trainerManager.GetAllTrainers();

            return Json(new { trainers = trainers });
        }

        [HttpGet]
        [Route("GetFor")]
        public PartialViewResult GetFor(int trainerId, string htmlClass, string actionName, string controllerName)
        {
            TrainerBasic trainerBasic;
            if (trainerId != 0)
            {
                trainerBasic = trainerManager.GetTrainer(trainerId);
            }
            else
            {
                trainerBasic = new TrainerBasic();
            }

            TrainerViewModel trainer = new TrainerViewModel()
            {
                TrainerBasic = trainerBasic,
                HtmlClass = htmlClass,
                ActionName = actionName,
                ControllerName = controllerName
            };

            return PartialView("_TrainerPartial", trainer);
        }

        [HttpPost]
        [Route("Create")]
        public JsonResult Create(TrainerBasic trainer)
        {
            int trainerId = trainerManager.AddTrainer(trainer);

            return Json(trainerId);
        }

        [HttpPost]
        [Route("GetAllTrainers")]
        public PartialViewResult GetAllTrainers()
        {
            List<TrainerBasic> trainers = trainerManager.GetAllTrainers();

            return PartialView("_TrainersPartial", trainers);
        }

        [HttpPost]
        [Route("Edit")]
        public PartialViewResult Edit(TrainerBasic trainer)
        {
            trainerManager.EditTrainer(trainer);

            List<TrainerBasic> trainers = trainerManager.GetAllTrainers();

            return PartialView("_TrainersPartial", trainers);
        }

        [HttpPost]
        [Route("Delete")]
        public JsonResult Delete(int trainerId)
        {
            bool result = trainerManager.DeleteTrainer(trainerId);

            return Json(result);
        }
    }
}