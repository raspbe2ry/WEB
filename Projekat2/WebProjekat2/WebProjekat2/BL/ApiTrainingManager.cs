using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat2.BModels;
using WebProjekat2.Data;
using WebProjekat2.IBL;
using WebProjekat2.Models;

namespace WebProjekat2.BL
{
    public class ApiTrainingManager : ITraining
    {
        private UnitOfWork UnitOfWork;

        public ApiTrainingManager(UnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

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
            Training training = UnitOfWork.TrainingRepository.GetByID(trainingBasic.Id);
            if (training != null)
            {
                training.Visit++;
                UnitOfWork.TrainingRepository.Update(training);

                UnitOfWork.Save();
                return true;
            }
            else
                return false;
        }

        public List<TrainingBasic> GetAllTrainings(DateRange dateRange)
        {
            if (dateRange != null)
            {
                return UnitOfWork.TrainingRepository.Get(x =>
                                            (dateRange.StartDate != null ? x.Start > dateRange.StartDate : true)
                                            &&
                                            (dateRange.EndDate != null ? x.Start < dateRange.EndDate : true)
                                            , null
                                            , "")
                                            .Select(x => new TrainingBasic()
                                            {
                                                Id = x.Id,
                                                Title = x.Title,
                                                Start = x.Start,
                                                End = x.Finish,
                                                Visit = x.Visit,
                                                TrainerId = x.TrainerId,
                                                TrainerName = UnitOfWork.TrainerRepository.Get(m => m.Id == x.TrainerId).Select(m => m.FullName).FirstOrDefault(),
                                            }).ToList();
            }
            else
            {
                return UnitOfWork.TrainingRepository.Get(
                                            null
                                            , null
                                            , "")
                                            .Select(x => new TrainingBasic()
                                            {
                                                Id = x.Id,
                                                Title = x.Title,
                                                Start = x.Start,
                                                End = x.Finish,
                                                Visit = x.Visit,
                                                TrainerId = x.TrainerId,
                                                TrainerName = UnitOfWork.TrainerRepository.Get(m => m.Id == x.TrainerId).Select(m => m.FullName).FirstOrDefault(),
                                            }).ToList();
            }
        }

        public List<BModels.StudentTraining> GetStudents(int trainingId)
        {
            throw new NotImplementedException();
        }

        public TrainingBasic GetTraining(int trainingId)
        {
            Training training = UnitOfWork.TrainingRepository.GetByID(trainingId);
            Trainer trainer = UnitOfWork.TrainerRepository.GetByID(training.TrainerId);

            if (training != null)
            {
                return new TrainingBasic()
                {
                    Id = training.Id,
                    TrainerName = (trainer != null) ? trainer.FullName : "",
                    TrainerId = training.TrainerId,
                    End = training.Finish,
                    Start = training.Start,
                    Title = training.Title,
                    Visit = training.Visit
                };
            }
            else
                return null;
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
