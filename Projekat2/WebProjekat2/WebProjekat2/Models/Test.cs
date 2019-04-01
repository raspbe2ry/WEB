using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjekat2.Models
{
    public class Test
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
