﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebProjekat2.Data;

namespace WebProjekat2.Data.Migrations
{
    [DbContext(typeof(KarateContext))]
    [Migration("20190519140212_AddPoolTables")]
    partial class AddPoolTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0-preview.19074.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebProjekat2.Models.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsCorrect");

                    b.Property<int?>("QuestionId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("WebProjekat2.Models.BeltEarning", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Belt");

                    b.Property<DateTime>("EarnDate");

                    b.Property<int>("StudentId");

                    b.Property<bool>("Success");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("BeltEarnings");
                });

            modelBuilder.Entity("WebProjekat2.Models.Pool", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("SubmitDate");

                    b.Property<string>("Submiter");

                    b.HasKey("Id");

                    b.ToTable("Pools");
                });

            modelBuilder.Entity("WebProjekat2.Models.PoolQuestion", b =>
                {
                    b.Property<int>("PoolId");

                    b.Property<int>("QuestionId");

                    b.HasKey("PoolId", "QuestionId");

                    b.HasIndex("QuestionId");

                    b.ToTable("PoolQuestions");
                });

            modelBuilder.Entity("WebProjekat2.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActual");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("WebProjekat2.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FullName");

                    b.Property<int>("Title");

                    b.Property<DateTime>("TrainingDate");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("WebProjekat2.Models.StudentTraining", b =>
                {
                    b.Property<int>("StudentId");

                    b.Property<int>("TrainingId");

                    b.HasKey("StudentId", "TrainingId");

                    b.HasIndex("TrainingId");

                    b.ToTable("StudentTrainings");
                });

            modelBuilder.Entity("WebProjekat2.Models.Trainer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("FullName");

                    b.Property<string>("Photo");

                    b.Property<int>("Title");

                    b.HasKey("Id");

                    b.ToTable("Trainers");
                });

            modelBuilder.Entity("WebProjekat2.Models.TrainerBeltEarning", b =>
                {
                    b.Property<int>("TrainerId");

                    b.Property<int>("BeltEarningId");

                    b.HasKey("TrainerId", "BeltEarningId");

                    b.HasIndex("BeltEarningId");

                    b.ToTable("TrainerBeltEarnings");
                });

            modelBuilder.Entity("WebProjekat2.Models.Training", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Finish");

                    b.Property<DateTime>("Start");

                    b.Property<string>("Title")
                        .HasMaxLength(50);

                    b.Property<int>("TrainerId");

                    b.Property<int>("Visit");

                    b.HasKey("Id");

                    b.HasIndex("TrainerId");

                    b.ToTable("Trainings");
                });

            modelBuilder.Entity("WebProjekat2.Models.Answer", b =>
                {
                    b.HasOne("WebProjekat2.Models.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId");
                });

            modelBuilder.Entity("WebProjekat2.Models.BeltEarning", b =>
                {
                    b.HasOne("WebProjekat2.Models.Student", "Student")
                        .WithMany("BeltEarnings")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebProjekat2.Models.PoolQuestion", b =>
                {
                    b.HasOne("WebProjekat2.Models.Pool", "Pool")
                        .WithMany("Questions")
                        .HasForeignKey("PoolId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebProjekat2.Models.Question", "Question")
                        .WithMany("Pools")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebProjekat2.Models.StudentTraining", b =>
                {
                    b.HasOne("WebProjekat2.Models.Student", "Student")
                        .WithMany("Trainings")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebProjekat2.Models.Training", "Training")
                        .WithMany("Students")
                        .HasForeignKey("TrainingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebProjekat2.Models.TrainerBeltEarning", b =>
                {
                    b.HasOne("WebProjekat2.Models.BeltEarning", "BeltEarning")
                        .WithMany("Trainers")
                        .HasForeignKey("BeltEarningId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebProjekat2.Models.Trainer", "Trainer")
                        .WithMany("BeltEarnings")
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebProjekat2.Models.Training", b =>
                {
                    b.HasOne("WebProjekat2.Models.Trainer", "Trainer")
                        .WithMany("Trainings")
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
