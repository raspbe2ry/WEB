using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjekat2.BModels
{
    public class DateRange
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateRange(string monthAndYear)
        {
            string[] date = monthAndYear.Split("/");
            int month = Int32.Parse(date[0]);
            int year = Int32.Parse(date[1]);
            this.StartDate = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            this.EndDate = this.StartDate.AddDays(daysInMonth);
        }
    }
}
