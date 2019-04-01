using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat2.BModels;

namespace WebProjekat2.IBL
{
    public interface IStudent
    {
        int AddStudent(StudentBasic basicStudent);
        bool DeleteStudent(int StudentId);
        List<StudentBasic> GetAllStudents();
        StudentBasic GetStudent(int StudentId);
        int EditStudent(StudentBasic basicStudent);
    }
}
