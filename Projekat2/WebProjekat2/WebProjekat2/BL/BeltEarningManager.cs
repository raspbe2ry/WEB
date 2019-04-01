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
    public class BeltEarningManager : IBeltEarning
    {
        public KarateContext data { get; set; }

        public BeltEarningManager(KarateContext context)
        {
            data = context;
        }

        public int AddBeltEarning(BeltEarningBasic beltEarningBasic)
        {
            try
            {
                BeltEarning beltEarning = new BeltEarning()
                {
                    EarnDate=beltEarningBasic.EarnDate,
                    StudentId=beltEarningBasic.StudentId,
                    Belt=beltEarningBasic.Belt,
                    Success=beltEarningBasic.Success
                };

                Student student = data.Students.Find(beltEarningBasic.StudentId);
                if (student != null)
                    student.BeltEarnings.Add(beltEarning);

                data.BeltEarnings.Add(beltEarning);
                data.SaveChanges();

                return beltEarning.Id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public List<BeltEarningBasic> GetBeltEarningsForStudent(int studentId)
        {
            try
            {
                Student student = data.Students.Include(x=> x.BeltEarnings).Where(x=>x.Id ==  studentId).FirstOrDefault();
                return student.BeltEarnings.Select(x => new BeltEarningBasic()
                {
                    Id = x.Id,
                    StudentId = x.StudentId,
                    Success = x.Success,
                    Belt = x.Belt,
                    EarnDate = x.EarnDate
                }).ToList();
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<BeltEarningBasic> GetAll()
        {
            try
            {
                return data.BeltEarnings.Include(x=> x.Student).Select(x => new BeltEarningBasic()
                {
                    Id = x.Id,
                    StudentId = x.Id,
                    StudentName = x.Student.FullName,
                    Belt = x.Belt,
                    EarnDate = x.EarnDate,
                    Success = x.Success
                }).ToList();
            }
            catch(Exception ex)
            {
                return new List<BeltEarningBasic>();
            }
        }

        public bool DeleteBeltEarning(int beltEarningId)
        {
            try
            {
                BeltEarning beltEarningToRemove = data.BeltEarnings.Find(beltEarningId);
                if (beltEarningToRemove != null)
                {
                    var beltEarning = data.BeltEarnings.Remove(beltEarningToRemove);
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

    }
}
