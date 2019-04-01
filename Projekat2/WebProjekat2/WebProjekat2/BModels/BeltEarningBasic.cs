using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat2.Models;

namespace WebProjekat2.BModels
{
    public class BeltEarningBasic
    {
        public int Id { get; set; }
        public DateTime EarnDate { get; set; }
        public KarateTitle Belt { get; set; }
        public bool Success { get; set; }
        public int StudentId { get; set; }
        
        public string StudentName { get; set; }
    }
}
