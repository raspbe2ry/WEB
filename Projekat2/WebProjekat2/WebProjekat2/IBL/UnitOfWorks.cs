using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat2.Data;
using WebProjekat2.Models;

namespace WebProjekat2.IBL
{
    public class UnitOfWork : IDisposable
    {
        public KarateContext context { get; set; }
        private GenericRepository<Training> trainingRepository;
        private GenericRepository<Trainer> trainerRepository;
        private GenericRepository<Student> studentRepository;
        private GenericRepository<BeltEarning> beltEarningRepository;
        private GenericRepository<StudentTraining> studentTrainingRepository;
        private GenericRepository<TrainerBeltEarning> trainerBeltEarningRepository;

        public UnitOfWork(KarateContext context)
        {
            this.context = context;
        }

        public GenericRepository<Training> TrainingRepository
        {
            get
            {
                return this.trainingRepository ?? new GenericRepository<Training>(context);
            }
        }

        public GenericRepository<Trainer> TrainerRepository
        {
            get
            {
                return this.trainerRepository ?? new GenericRepository<Trainer>(context);
            }
        }

        public GenericRepository<Student> StudentRepository
        {
            get
            {
                return this.studentRepository ?? new GenericRepository<Student>(context);
            }
        }

        public GenericRepository<BeltEarning> BeltEarningRepository
        {
            get
            {
                return this.beltEarningRepository ?? new GenericRepository<BeltEarning>(context);
            }
        }

        public GenericRepository<StudentTraining> StudentTrainingRepository
        {
            get
            {
                return this.studentTrainingRepository ?? new GenericRepository<StudentTraining>(context);
            }
        }

        public GenericRepository<TrainerBeltEarning> TrainerBeltEarningRepository
        {
            get
            {
                return this.trainerBeltEarningRepository ?? new GenericRepository<TrainerBeltEarning>(context);
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
