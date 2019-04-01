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
        public KarateContext data { get; set; }

        public StudentManager(KarateContext context)
        {
            data = context;
        }

        public int AddStudent(StudentBasic basicStudent)
        {
            try
            {
                Models.Student student = new Models.Student()
                {
                    FullName = basicStudent.FullName,
                    Title = basicStudent.TitleId,
                    TrainingDate = basicStudent.TrainingStart,
                };

                data.Students.Add(student);
                data.SaveChanges();

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
                Student studentToRemove = data.Students.Find(StudentId);
                if (studentToRemove != null)
                {
                    var student = data.Students.Remove(studentToRemove);
                    data.SaveChanges();

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
            return data.Students.Select(x => new StudentBasic()
            {
                FullName=x.FullName,
                Id=x.Id,
                TrainingStart=x.TrainingDate,
                Title=x.Title.ToString()
            }).ToList();
        }

        public StudentBasic GetStudent(int StudentId)
        {
            Student student = data.Students.Include(x => x.BeltEarnings)
                                            .Include(x => x.Trainings)
                                            .Where(x=> x.Id == StudentId).FirstOrDefault();

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
                Student student = data.Students.Find(studentBasic.Id);
                if (student != null)
                {
                    student.FullName = studentBasic.FullName;
                    student.Title = studentBasic.TitleId;
                    student.TrainingDate = studentBasic.TrainingStart;

                    data.SaveChanges();

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
