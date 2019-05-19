using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat2.BModels;
using WebProjekat2.Data;
using WebProjekat2.IBL;
using Microsoft.EntityFrameworkCore;
using WebProjekat2.Models;

namespace WebProjekat2.BL
{
    public class StudentManager : IStudent
    {
        public UnitOfWork unitOfWork;

        public StudentManager(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public int AddStudent(StudentBasic basicStudent)
        {
            try
            {
                Student student = new Student()
                {
                    FullName = basicStudent.FullName,
                    Title = basicStudent.TitleId,
                    TrainingDate = basicStudent.TrainingStart,
                };

                unitOfWork.StudentRepository.Insert(student);
                unitOfWork.Save();

                return student.Id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public bool DeleteStudent(int StudentId)
        {
            try
            {
                Student studentToRemove = unitOfWork.StudentRepository.GetByID(StudentId);
                if (studentToRemove != null)
                {
                    unitOfWork.StudentRepository.Delete(studentToRemove);
                    unitOfWork.Save();

                    return true;
                }
                else
                    return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public List<StudentBasic> GetAllStudents()
        {
            return unitOfWork.StudentRepository.Get().Select(x => new StudentBasic()
            {
                FullName=x.FullName,
                Id=x.Id,
                TrainingStart=x.TrainingDate,
                Title=x.Title.ToString()
            }).ToList();
        }

        public StudentBasic GetStudent(int StudentId)
        {
            Student student = unitOfWork.StudentRepository.Get(x => x.Id == StudentId, null, "BeltEarnings,Trainings")
                .First();

            if (student != null)
            {
                return new StudentBasic()
                {
                    FullName = student.FullName,
                    TrainingStart = student.TrainingDate,
                    Id = student.Id,
                    Title = student.Title.ToString(),
                    TitleId = student.Title
                };
            }
            else
                return null;
        }

        public int EditStudent(StudentBasic studentBasic)
        {
            try
            {
                Student student = unitOfWork.StudentRepository.GetByID(studentBasic.Id);
                if (student != null)
                {
                    student.FullName = studentBasic.FullName;
                    student.Title = studentBasic.TitleId;
                    student.TrainingDate = studentBasic.TrainingStart;

                    unitOfWork.StudentRepository.Update(student);
                    unitOfWork.Save();

                    return studentBasic.Id;
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
