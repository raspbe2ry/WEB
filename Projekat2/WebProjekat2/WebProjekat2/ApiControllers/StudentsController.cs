using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebProjekat2.BModels;
using WebProjekat2.IBL;

namespace WebProjekat2.Controllers
{
    [Produces("application/json")]
    [Route("api/Students")]
    public class StudentsController : Controller
    {
        IStudent studentManager { get; set; }
        IBeltEarning beltEarningManager { get; set; }

        public StudentsController(IStudent studentManager, IBeltEarning beltEarningManager)
        {
            this.studentManager = studentManager;
            this.beltEarningManager = beltEarningManager;
        }

        [Route("addStudent")]
        [HttpPost]
        public ActionResult AddStudent([FromBody]StudentBasic studentBasic)
        {
            int res = -1;

            res = studentManager.AddStudent(studentBasic);

            if (res != -1)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest();
            }
        }        

        [Route("deleteStudent")]
        [HttpDelete]
        public ActionResult DeleteStudent(int studentId)
        {
            bool res = false;

            res = studentManager.DeleteStudent(studentId);

            if (res)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("addBeltEarning")]
        [HttpPost]
        public ActionResult AddBeltEarning([FromBody] BeltEarningBasic beltEarningBasic)
        {
            int res = -1;

            res = beltEarningManager.AddBeltEarning(beltEarningBasic);

            if (res != -1)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}