using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat2.BModels;

namespace WebProjekat2.IBL
{
    public interface ITrainer
    {
        int AddTrainer(TrainerBasic trainerBasic);
        bool DeleteTrainer(int TrainerId);
        int EditTrainer(TrainerBasic trainerBasic);
        TrainerBasic GetTrainer(int trainerId);
        List<TrainerBasic> GetAllTrainers();
    }
}
