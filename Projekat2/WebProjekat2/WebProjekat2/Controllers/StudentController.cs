using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebProjekat2.BModels;
using WebProjekat2.IBL;
using System.Web;
using WebProjekat2.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebProjekat2.MvcControllers
{
    [Route("Student")]
    public class StudentController : Controller
    {
        private IStudent studentManager { get; set; }
        private IBeltEarning beltEarningManager { get; set; }
        public ITrainer trainerManager { get; set; }
        private ITraining trainingManager { get; set; }

        public StudentController(IStudent studentManager, ITrainer trainerManager, IBeltEarning beltEarningManager, ITraining trainingManager)
        {
            this.studentManager = studentManager;
            this.beltEarningManager = beltEarningManager;
            this.trainerManager = trainerManager;
            this.trainingManager = trainingManager;
        }

        [Route("Index")]
        public IActionResult Index()
        {
            List<StudentBasic> students = studentManager.GetAllStudents();

            ViewBag.ServerAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            return View(students);
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create(StudentViewModel studentView)
        {
            int value = studentManager.AddStudent(studentView.StudentBasic);

            var message = new
            {
                value = value,
                status = value == -1 ? "error" : "ok" 
            };

            List<StudentBasic> students = studentManager.GetAllStudents();

            return View("Index", students);
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(int studentId)
        {
            studentManager.DeleteStudent(studentId);

            List<StudentBasic> students = studentManager.GetAllStudents();

            return View("Index", students);
        }

        [HttpGet]
        [Route("GetForEdit")]
        public PartialViewResult GetForEdit(int studentId)
        {
            StudentBasic studentToReturn = studentManager.GetStudent(studentId);
            StudentViewModel studentView = new StudentViewModel()
            {
                StudentBasic=studentToReturn,
                HtmlClass="edt",
                ControllerName="Student",
                ActionName="Edit"
            };
            return PartialView("_StudentPartial", studentView);
        }

        [HttpPost]
        [Route("Edit")]
        public IActionResult Edit(StudentViewModel studentView)
        {
            StudentBasic studentToUpdate = new StudentBasic()
            {
                Id = studentView.StudentBasic.Id,
                FullName = studentView.StudentBasic.FullName,
                TitleId = studentView.StudentBasic.TitleId,
                TrainingStart = studentView.StudentBasic.TrainingStart
            };

            studentManager.EditStudent(studentToUpdate);

            List<StudentBasic> students = studentManager.GetAllStudents();

            return View("Index", students);
        }

        [HttpGet]
        [Route("StudentDetails")]
        public PartialViewResult GetDetails(int studentId)
        {
            List<BeltEarningBasic> beltEarnings = beltEarningManager.GetBeltEarningsForStudent(studentId);
            List<TrainingBasic> trainings = trainingManager.GetTrainingsForStudent(studentId ,DateTime.Now);
            List<TrainerBasic> trainers = trainerManager.GetAllTrainers();

            return PartialView("_StudentDetailsView",
                new StudentDetailsViewModel() {
                    BeltEarnings = beltEarnings,
                    Trainings = trainings,
                    BeltEarningToCreate = new BeltEarningBasic() { StudentId = studentId },
                    Trainers = new SelectList(trainers, "Id", "FullName"),

                });
        }
    }
}