using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat2.Models;

namespace WebProjekat2.BModels
{
    public class StudentBasic
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public KarateTitle TitleId { get; set; }
        public string Title { get; set; }
        public DateTime TrainingStart { get; set; }
    }
}
