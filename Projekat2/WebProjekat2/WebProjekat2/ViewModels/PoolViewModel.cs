using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat2.BModels;

namespace WebProjekat2.ViewModels
{
    public class PoolViewModel
    {
        public List<PoolBasic> Pools { get; set; }
        public QuestionBasic Question { get; set; }
    }
}
