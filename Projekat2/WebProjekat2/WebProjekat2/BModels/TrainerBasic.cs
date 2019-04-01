using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat2.Models;

namespace WebProjekat2.BModels
{
    public class TrainerBasic
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public KarateTitle Title { get; set; }
    }
}
