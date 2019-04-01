using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat2.BModels;

namespace WebProjekat2.IBL
{
    public interface ITraining
    {
        int AddTraining(TrainingBasic trainingBasic);
        int AddTrainer(int TrainingId, int TrainerId);
        bool AddDescription(string description, int trainingId);
        bool DeleteTraining(int trainingId);
        bool EditTraining(TrainingBasic trainingBasic);
        TrainingBasic GetTraining(int trainingId);
        List<TrainingBasic> GetAllTrainings(DateRange dateRange);
        List<TrainingBasic> GetTrainingsForStudent(int StudentId, DateTime monthDate);

        List<StudentTraining> GetStudents(int trainingId);
        bool StudentPresence(int trainingId, int studentId, bool presence);
    }
}
