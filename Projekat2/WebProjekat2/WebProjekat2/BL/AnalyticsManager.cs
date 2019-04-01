using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat2.BModels;
using WebProjekat2.Data;

namespace WebProjekat2.BL
{
    public class AnalyticsManager
    {
        public KarateContext data { get; set; }

        public AnalyticsManager(KarateContext context)
        {
            data = context;
        }

        public ReportModel GetReport(ReportConfiguration conf)
        {
            ReportModel result = new ReportModel();
            result.Month = conf.Month;

            DateRange dateRange = new DateRange(conf.Month);

            var trainingQuery = data.Trainings.Where(x => x.Start > dateRange.StartDate && x.Start < dateRange.EndDate);

            var studentQuery = (from training in trainingQuery
                         from studentTraining in data.StudentTrainings.Where(x=>x.TrainingId == training.Id).DefaultIfEmpty()
                         join student in data.Students on studentTraining.StudentId equals student.Id into gr
                         group gr by new { training.Id, training.Start } into grouped
                         select new
                         {
                             p0 = grouped.Key.Id,
                             p1 = grouped.Key.Start,
                             p2 = grouped.Count()
                         });

            if (studentQuery.Count() > 0)
            {
                if (conf.MinStudents)
                {
                    result.MinNumOnTraining = studentQuery.Min(x => x.p2);
                    result.DateWithMinStudents = studentQuery.Where(x => x.p2 == result.MinNumOnTraining).FirstOrDefault().p1;
                }
                if (conf.MaxStudents)
                {
                    result.MaxNumOnTraining = studentQuery.Max(x => x.p2);
                    result.DateWithMaxStudents = studentQuery.Where(x => x.p2 == result.MaxNumOnTraining).FirstOrDefault().p1;
                }
                result.AverageOnTraining = studentQuery.Average(x => x.p2);
            }
            else
            {
                result.MinNumOnTraining = 0;
                result.DateWithMinStudents = null;
                result.MaxNumOnTraining = 0;
                result.DateWithMaxStudents = null;
                result.AverageOnTraining = 0;
            }

            if(conf.NewStudents)
                result.NewStudents = data.Students.Where(x => x.TrainingDate > dateRange.StartDate && x.TrainingDate < dateRange.EndDate)
                                            .Count();

            result.NumOfTrainings = trainingQuery.Count();

            if(conf.NewBeltEarnings && conf.ByTitle)
            {
                result.NewBeltEarningsByBelt = data.BeltEarnings.Where(x => x.EarnDate > dateRange.StartDate && x.EarnDate < dateRange.EndDate)
                                                        .GroupBy(x => x.Belt)
                                                        .Select(x =>
                                                        new BeltEarningsStruct
                                                        {
                                                            belt = x.Key.ToString(),
                                                            count = x.Count()
                                                        }
                                                        ).ToList();
            }
            else if (conf.NewBeltEarnings)
            {
                result.NewBeltEarnings = data.BeltEarnings.Where(x => x.EarnDate > dateRange.StartDate && x.EarnDate < dateRange.EndDate)
                                                        .Count();
            }

            return result;

        }
    }
}
