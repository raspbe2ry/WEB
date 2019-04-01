using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebProjekat2.BModels;
using WebProjekat2.IBL;

namespace WebProjekat2.Controllers
{
    [Route("BeltEarning")]
    public class BeltEarningController : Controller
    {
        private IBeltEarning beltEarningManager { get; set; }
        private IStudent studentManager { get; set; }

        public BeltEarningController(IBeltEarning beltEarningManager, IStudent studentManager)
        {
            this.beltEarningManager = beltEarningManager;
            this.studentManager = studentManager;
        }

        public IActionResult Index()
        {
            List<BeltEarningBasic> beltEarnings = beltEarningManager.GetAll();

            return View(beltEarnings);
        }

        [HttpPost]
        [Route("Create")]
        public PartialViewResult Create(BeltEarningBasic beltEarning)
        {
            beltEarningManager.AddBeltEarning(beltEarning);
            List<BeltEarningBasic> beltEarnings = beltEarningManager.GetBeltEarningsForStudent(beltEarning.StudentId);

            return PartialView("_BeltEarningsPartial",beltEarnings);
        }

        [HttpGet]
        [Route("GetAllBeltEarnings")]
        public PartialViewResult GetAllBeltEarnings()
        {
            List<BeltEarningBasic> beltEarnings = beltEarningManager.GetAll();

            return PartialView("_BeltEarningsMiniPartial", beltEarnings);
        }

        [HttpPost]
        [Route("Delete")]
        public JsonResult Delete(int beltEarningId)
        {
            bool deleted = beltEarningManager.DeleteBeltEarning(beltEarningId);

            return Json(deleted);
        }

    }
}