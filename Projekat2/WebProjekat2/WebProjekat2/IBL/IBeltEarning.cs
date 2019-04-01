using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat2.BModels;

namespace WebProjekat2.IBL
{
    public interface IBeltEarning
    {
        int AddBeltEarning(BeltEarningBasic beltEarningBasic);
        bool DeleteBeltEarning(int beltEarningId);
        List<BeltEarningBasic> GetAll();
        List<BeltEarningBasic> GetBeltEarningsForStudent(int studentId);
    }
}
