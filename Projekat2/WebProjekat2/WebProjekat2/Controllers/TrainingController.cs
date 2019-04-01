using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebProjekat2.BModels;
using WebProjekat2.IBL;
using WebProjekat2.ViewModels;

namespace WebProjekat2.Controllers
{
    [Route("Training")]
    public class TrainingController : Controller
    {
        private ITraining trainingManager { get; set; }
        private ITrainer trainerManager { get; set; }

        public TrainingController(ITraining trainingManager, ITrainer trainerManager)
        {
            this.trainingManager = trainingManager;
            this.trainerManager = trainerManager;
        }

        [Route("Index")]
        public IActionResult Index()
        {
            List<TrainingBasic> trainings = trainingManager.GetAllTrainings(null);

            return View(trainings);
        }

        [HttpPost]
        [Route("Create")]
        public JsonResult Create(TrainingBasic trainingBasic)
        {
            int trainingId = trainingManager.AddTraining(trainingBasic);

            return Json(new { id=trainingId });
        }

        [HttpGet]
        [Route("GetTrainingsTable")]
        public PartialViewResult GetTrainingsTable()
        {
            List<TrainingBasic> trainings = trainingManager.GetAllTrainings(null);

            return PartialView("_TrainingsPartial", trainings);
        }

        [HttpPost]
        [Route("Delete")]
        public PartialViewResult Delete(int trainingId)
        {
            bool result = trainingManager.DeleteTraining(trainingId);

            List<TrainingBasic> trainings = trainingManager.GetAllTrainings(null);

            return PartialView("_TrainingsPartial", trainings);
        }

        [HttpGet]
        [Route("GetTraining")]
        public PartialViewResult GetTraining(int trainingId, string action)
        {
            TrainingBasic trainingToReturn = trainingManager.GetTraining(trainingId);
            List<MiniTrainer> miniTrainers = trainerManager.GetAllTrainers().Select(x => new MiniTrainer()
                {
                    Id = x.Id,
                    FullName = x.FullName, 
                    Selected = (x.Id == trainingToReturn.TrainerId)
                }
            ).ToList();

            TrainingViewModel trainingView = new TrainingViewModel()
            {
                TrainingBasic = trainingToReturn,
                Trainers = new SelectList(miniTrainers, "Id", "FullName"),
                HtmlClass = "edt",
                ControllerName = "Training",
                ActionName = action
            };

            return PartialView("_TrainingPartial", trainingView);
        }

        [HttpPost]
        [Route("Edit")]
        public PartialViewResult Edit(TrainingBasic trainingUpdated)
        {
            bool result = trainingManager.EditTraining(trainingUpdated);

            List<TrainingBasic> trainings = trainingManager.GetAllTrainings(null);

            return PartialView("_TrainingsPartial", trainings);
        }

        [HttpGet]
        [Route("StudentsPanel")]
        public PartialViewResult StudentsPanel(int trainingId)
        {
            List<StudentTraining> studentTrainings = trainingManager.GetStudents(trainingId);

            return PartialView("_StudentsPanelPartial", studentTrainings);
        }

        [HttpPost]
        [Route("ChangeStudentPresence")]
        public JsonResult ChangeStudentPresence(int trainingId, int studentId, bool presence)
        {
            trainingManager.StudentPresence(trainingId, studentId, presence);

            return Json(true);
        }
    }
}