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
        public KarateContext data { get; set; }

        public TrainingManager(KarateContext context)
        {
            data = context;
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

                Trainer trainer = data.Trainers.Find(trainingBasic.TrainerId);
                if(trainer!=null)
                {
                    trainer.Trainings.Add(training);
                    training.Trainer = trainer;
                }

                data.Trainings.Add(training);
                data.SaveChanges();

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
                Trainer trainer = data.Trainers.Find(trainerId);
                Training training = data.Trainings.Find(trainingId);
                if (trainer != null && training != null)
                {
                    trainer.Trainings.Add(training);
                    training.Trainer = trainer;
                }
                else
                {
                    return 404;
                }

                data.SaveChanges();
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
                Training training = data.Trainings.Find(trainingId);
                if (training != null)
                {
                    training.Title=description;
                }
                else
                {
                    return false;
                }

                data.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<TrainingBasic> GetAllTrainings(DateRange dateRange)
        {
            if (dateRange != null)
            {
                return data.Trainings.Where(x =>
                                            (dateRange.StartDate != null ? x.Start > dateRange.StartDate : true)
                                            &&
                                            (dateRange.EndDate != null ? x.Start < dateRange.EndDate : true)
                                            )
                                            .Select(x => new TrainingBasic()
                                            {
                                                Id = x.Id,
                                                Title = x.Title,
                                                Start = x.Start,
                                                End = x.Finish,
                                                Visit = x.Visit,
                                                TrainerId = x.TrainerId,
                                                TrainerName = data.Trainers.Where(m => m.Id == x.TrainerId).Select(m => m.FullName).FirstOrDefault(),
                                            }).ToList();
            }
            else
                return data.Trainings.Select(x => new TrainingBasic()
                                            {
                                                Id = x.Id,
                                                Title = x.Title,
                                                Start = x.Start,
                                                End = x.Finish,
                                                Visit = x.Visit,
                                                TrainerId = x.TrainerId,
                                                TrainerName = data.Trainers.Where(m => m.Id == x.TrainerId).Select(m => m.FullName).FirstOrDefault(),
                                            }).ToList();
        }

        public bool DeleteTraining(int trainingId)
        {
            try
            {
                Training trainingToRemove = data.Trainings.Find(trainingId);
                if (trainingToRemove != null)
                {
                    var training = data.Trainings.Remove(trainingToRemove);
                    data.SaveChanges();

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
            Training training = data.Trainings.Find(trainingId);
            Trainer trainer = data.Trainers.Find(training.TrainerId);

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
                Training training = data.Trainings.Find(trainingUpdated.Id);

                if (training != null)
                {
                    training.Start = trainingUpdated.Start;
                    training.Visit = trainingUpdated.Visit;
                    training.Title = trainingUpdated.Title;
                    training.Finish = trainingUpdated.End;

                    if(training.TrainerId != trainingUpdated.TrainerId)
                    {
                        Trainer newTrainer = data.Trainers.Find(trainingUpdated.TrainerId);
                        Trainer oldTrainer = data.Trainers.Find(training.TrainerId);
                        training.Trainer = newTrainer;
                        if(oldTrainer != null)
                            oldTrainer.Trainings.Remove(training);
                        newTrainer.Trainings.Add(training);
                    }

                    data.SaveChanges();

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
                Student student = data.Students.Include(x => x.Trainings).Where(x => x.Id == StudentId).FirstOrDefault();
                List<int> trainingIds = student.Trainings.Select(x=>x.TrainingId).ToList();
                List<TrainingBasic> trainings = data.Trainings.Where(x => trainingIds.Contains(x.Id))
                                                    .Select(x => new TrainingBasic()
                                                    {
                                                        Id = x.Id,
                                                        End = x.Finish, 
                                                        Start = x.Start,
                                                        Title = x.Title,
                                                        Visit = x.Visit
                                                    }).ToList();

                return trainings;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<BModels.StudentTraining> GetStudents(int trainingId)
        {
            List<BModels.StudentTraining> studentTrainings =
                (from student in data.Students
                 from stTr in data.StudentTrainings.Where(x=>x.TrainingId == trainingId && student.Id == x.StudentId).DefaultIfEmpty()
                 from training in data.Trainings.Where(x => x.Id == stTr.TrainingId).DefaultIfEmpty()
                 select new BModels.StudentTraining()
                 {
                     StudentId = student.Id,
                     TrainingId = trainingId,
                     Present = (training != null),
                     StudentName = student.FullName
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
                data.StudentTrainings.Add(studentTraining);
            }
            else
            {
                Models.StudentTraining studentTraining = data.StudentTrainings.Where(x => x.StudentId == studentId && x.TrainingId == trainingId).First();
                data.StudentTrainings.Remove(studentTraining);
            }
            data.SaveChanges();

            return true;
        }

    }
}
