using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat2.BModels;
using WebProjekat2.IBL;

namespace WebProjekat2.BL
{
    public class ApiTrainingManager : ITraining
    {
        public bool AddDescription(string description, int trainingId)
        {
            throw new NotImplementedException();
        }

        public int AddTrainer(int TrainingId, int TrainerId)
        {
            throw new NotImplementedException();
        }

        public int AddTraining(TrainingBasic trainingBasic)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTraining(int trainingId)
        {
            throw new NotImplementedException();
        }

        public bool EditTraining(TrainingBasic trainingBasic)
        {
            throw new NotImplementedException();
        }

        public List<TrainingBasic> GetAllTrainings(DateRange dateRange)
        {
            throw new NotImplementedException();
        }

        public List<StudentTraining> GetStudents(int trainingId)
        {
            throw new NotImplementedException();
        }

        public TrainingBasic GetTraining(int trainingId)
        {
            throw new NotImplementedException();
        }

        public List<TrainingBasic> GetTrainingsForStudent(int StudentId, DateTime monthDate)
        {
            throw new NotImplementedException();
        }

        public bool StudentPresence(int trainingId, int studentId, bool presence)
        {
            throw new NotImplementedException();
        }
    }
}
