using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat2.Models;

namespace WebProjekat2.Data
{
    public class KarateContext: DbContext
    {
        public KarateContext()
        {

        }

        public KarateContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<StudentTraining>().HasKey(st => new { st.StudentId, st.TrainingId });

            builder.Entity<StudentTraining>()
                        .HasOne<Student>(st => st.Student)
                        .WithMany(s => s.Trainings)
                        .HasForeignKey(st => st.StudentId);


            builder.Entity<StudentTraining>()
                .HasOne<Training>(st => st.Training)
                .WithMany(s => s.Students)
                .HasForeignKey(st => st.TrainingId);

            builder.Entity<TrainerBeltEarning>().HasKey(tbe => new { tbe.TrainerId, tbe.BeltEarningId });

            builder.Entity<TrainerBeltEarning>()
                        .HasOne<Trainer>(tbe => tbe.Trainer)
                        .WithMany(be => be.BeltEarnings)
                        .HasForeignKey(st => st.TrainerId);


            builder.Entity<TrainerBeltEarning>()
                .HasOne<BeltEarning>(tbe => tbe.BeltEarning)
                .WithMany(t => t.Trainers)
                .HasForeignKey(tbe => tbe.BeltEarningId);
        }

        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<BeltEarning> BeltEarnings { get; set; }
        public DbSet<StudentTraining> StudentTrainings { get; set; }
        public DbSet<TrainerBeltEarning> TrainerBeltEarnings { get; set; }
    }
}
