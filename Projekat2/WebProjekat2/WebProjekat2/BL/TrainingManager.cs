using Microsoft.EntityFrameworkCore;
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
    public class TrainingManager: ITraining
    {
        UnitOfWork unitOfWork;

        public TrainingManager(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public int AddTraining(TrainingBasic trainingBasic)
        {
            try
            {
                Training training = new Training()
                {
                    Start = trainingBasic.Start,
                    Finish = trainingBasic.End,
                    Title = trainingBasic.Title,
                    Visit = trainingBasic.Visit,
                };

                Trainer trainer = unitOfWork.TrainerRepository.GetByID(trainingBasic.TrainerId);
                if(trainer != null)
                {
                    trainer.Trainings.Add(training);
                    training.Trainer = trainer;
                }

                unitOfWork.TrainingRepository.Insert(training);
                unitOfWork.Save();

                return training.Id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int AddTrainer(int trainingId, int trainerId)
        {
            try
            {
                Trainer trainer = unitOfWork.TrainerRepository.GetByID(trainerId);
                Training training = unitOfWork.TrainingRepository.GetByID(trainingId);
                if (trainer != null && training != null)
                {
                    trainer.Trainings.Add(training);
                    training.Trainer = trainer;
                }
                else
                {
                    return 404;
                }

                unitOfWork.Save();
                return 1;
            }
            catch(Exception ex)
            {
                return -1;
            }
        }

        public bool AddDescription(string description, int trainingId)
        {
            try
            {
                Training training = unitOfWork.TrainingRepository.GetByID(trainingId);
                if (training != null)
                {
                    training.Title=description;
                    unitOfWork.TrainingRepository.Update(training);
                }
                else
                {
                    return false;
                }

                unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<TrainingBasic> GetAllTrainings(DateRange dateRange)
        {
            if( dateRange != null )
            {
                return unitOfWork.TrainingRepository.Get(x =>
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
                                                TrainerName = unitOfWork.TrainerRepository.Get(m => m.Id == x.TrainerId).Select(m => m.FullName).FirstOrDefault(),
                                            }).ToList();
            }
            else
            {
                return unitOfWork.TrainingRepository.Get(
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
                                                TrainerName = unitOfWork.TrainerRepository.Get(m => m.Id == x.TrainerId).Select(m => m.FullName).FirstOrDefault(),
                                            }).ToList();
            }
        }

        public bool DeleteTraining(int trainingId)
        {
            try
            {
                Training trainingToRemove = unitOfWork.TrainingRepository.GetByID(trainingId);
                if (trainingToRemove != null)
                {
                    unitOfWork.TrainingRepository.Delete(trainingToRemove);
                    unitOfWork.Save();

                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public TrainingBasic GetTraining(int trainingId)
        {
            Training training = unitOfWork.TrainingRepository.GetByID(trainingId);
            Trainer trainer = unitOfWork.TrainerRepository.GetByID(training.TrainerId);

            if (training != null)
            {
                return new TrainingBasic()
                {
                    Id = training.Id, 
                    TrainerName = (trainer != null) ? trainer.FullName : "", 
                    TrainerId = training.TrainerId,
                    End =  training.Finish,
                    Start = training.Start,
                    Title = training.Title,
                    Visit = training.Visit
                };
            }
            else
                return null;
        }

        public bool EditTraining(TrainingBasic trainingUpdated)
        {
            try
            {
                Training training = unitOfWork.TrainingRepository.GetByID(trainingUpdated.Id);

                if (training != null)
                {
                    training.Start = trainingUpdated.Start;
                    training.Visit = trainingUpdated.Visit;
                    training.Title = trainingUpdated.Title;
                    training.Finish = trainingUpdated.End;

                    if(training.TrainerId != trainingUpdated.TrainerId)
                    {
                        Trainer newTrainer = unitOfWork.TrainerRepository.GetByID(trainingUpdated.TrainerId);
                        Trainer oldTrainer = unitOfWork.TrainerRepository.GetByID(training.TrainerId);
                        training.Trainer = newTrainer;
                        unitOfWork.TrainingRepository.Update(training);
                    }

                    unitOfWork.Save();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public List<TrainingBasic> GetTrainingsForStudent(int StudentId, DateTime monthDate)
        {
            try
            {
                List<int> trainingIds = unitOfWork.StudentTrainingRepository.Get(x=>x.StudentId == StudentId).Select(x=> x.TrainingId).ToList();
                List<TrainingBasic> trainingBasics =unitOfWork.TrainingRepository.Get(x=> trainingIds.Contains(x.Id)).Select(x => new TrainingBasic()
                {
                    Id = x.Id,
                    End = x.Finish,
                    Start = x.Start,
                    Title = x.Title,
                    Visit = x.Visit
                }).ToList();

                return trainingBasics;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<BModels.StudentTraining> GetStudents(int trainingId)
        {
            List<int> studentIds = unitOfWork.StudentTrainingRepository.Get(x => x.TrainingId == trainingId)
                .Select(x => x.StudentId).ToList();
            List<BModels.StudentTraining> studentTrainings = unitOfWork.StudentRepository.Get(null, null, "").
                Select(x => new BModels.StudentTraining()
                {
                    StudentId = x.Id,
                    TrainingId = trainingId,
                    Present = studentIds.Contains(x.Id),
                    StudentName = x.FullName
                }).ToList();

            return studentTrainings;
        }

        public bool StudentPresence(int trainingId, int studentId, bool presence)
        {
            if (presence)
            {
                Models.StudentTraining studentTraining= new Models.StudentTraining()
                {
                    StudentId = studentId,
                    TrainingId = trainingId
                };
                unitOfWork.StudentTrainingRepository.Insert(studentTraining);
            }
            else
            {
                Models.StudentTraining studentTraining = unitOfWork.StudentTrainingRepository.Get(x => x.StudentId == studentId && x.TrainingId == trainingId).First();
                unitOfWork.StudentTrainingRepository.Delete(studentTraining);
            }
            unitOfWork.Save();

            return true;
        }

    }
}
