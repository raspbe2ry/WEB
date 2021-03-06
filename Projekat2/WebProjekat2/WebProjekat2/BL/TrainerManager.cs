﻿using Microsoft.EntityFrameworkCore;
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
    public class TrainerManager : ITrainer
    {
        private UnitOfWork unitOfWork;

        public TrainerManager(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public int AddTrainer(TrainerBasic trainerBasic)
        {
            try
            {
                Trainer trainer = new Trainer()
                {
                    Description=trainerBasic.Description,
                    FullName=trainerBasic.FullName,
                    Photo= trainerBasic.Photo,
                    Title=trainerBasic.Title
                };

                unitOfWork.TrainerRepository.Insert(trainer);
                unitOfWork.Save();

                return trainer.Id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public bool DeleteTrainer(int TrainerId)
        {
            try
            {
                var trainer = unitOfWork.TrainerRepository.GetByID(TrainerId);
                unitOfWork.TrainerRepository.Delete(trainer);
                unitOfWork.Save();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<TrainerBasic> GetAllTrainers()
        {
            return unitOfWork.TrainerRepository.Get().Select(x => new TrainerBasic()
            {
                Id=x.Id,
                Description=x.Description,
                FullName=x.FullName,
                Title=x.Title,
                Photo=x.Photo
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

        public int EditTrainer(TrainerBasic trainerBasic)
        {
            try
            {
                Trainer trainer = unitOfWork.TrainerRepository.GetByID(trainerBasic.Id);
                if (trainer != null)
                {
                    trainer.Description = trainerBasic.Description;
                    trainer.FullName = trainerBasic.FullName;
                    trainer.Photo = trainerBasic.Photo;
                    trainer.Title = trainerBasic.Title;

                    unitOfWork.TrainerRepository.Update(trainer);
                    unitOfWork.Save();

                    return trainer.Id;
                }
                else
                    return -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
