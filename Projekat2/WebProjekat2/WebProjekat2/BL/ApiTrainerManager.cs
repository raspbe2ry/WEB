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
    public class ApiTrainerManager : ITrainer
    {
        public UnitOfWork unitOfWork { get; set; }

        public ApiTrainerManager(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public int AddTrainer(TrainerBasic trainerBasic)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTrainer(int TrainerId)
        {
            throw new NotImplementedException();
        }

        public int EditTrainer(TrainerBasic trainerBasic)
        {
            throw new NotImplementedException();
        }

        public List<TrainerBasic> GetAllTrainers()
        {
            return unitOfWork.TrainerRepository.Get().Select(x => new TrainerBasic()
            {
                Id = x.Id,
                Description = x.Description,
                FullName = x.FullName,
                Title = x.Title,
                Photo = x.Photo
            }).ToList();
        }

        public TrainerBasic GetTrainer(int trainerId)
        {
            try
            {
                Trainer trainer = unitOfWork.TrainerRepository.GetByID(trainerId);

                if (trainer != null)
                {
                    return new TrainerBasic()
                    {
                        Id = trainer.Id,
                        FullName = trainer.FullName,
                        Description = trainer.Description,
                        Photo = trainer.Photo,
                        Title = trainer.Title
                    };
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
