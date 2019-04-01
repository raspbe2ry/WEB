using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebProjekat2.BL;
using WebProjekat2.BModels;
using WebProjekat2.IBL;

namespace WebProjekat2.Controllers
{
    [Route("Analytic")]
    public class AnalyticController : Controller
    {
        public AnalyticsManager analyticsManager { get; set; }

        public AnalyticController(AnalyticsManager analyticsManager)
        {
            this.analyticsManager = analyticsManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("GetReport")]
        public PartialViewResult GetReport(ReportConfiguration reportConfiguration)
        {
            ReportModel report = analyticsManager.GetReport(reportConfiguration);

            ViewBag.ReportConfig = reportConfiguration;
            return PartialView("_MonthlyReportPartial", report);
        }
    }
}