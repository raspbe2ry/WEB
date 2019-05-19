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
        public UnitOfWork unitOfWork;

        public BeltEarningManager(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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

                Student student = unitOfWork.StudentRepository.GetByID(beltEarningBasic.StudentId);
                if (student != null)
                    student.BeltEarnings.Add(beltEarning);

                unitOfWork.BeltEarningRepository.Insert(beltEarning);
                unitOfWork.Save();

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
                Student student = unitOfWork.StudentRepository.Get(x => x.Id == studentId, null, "BeltEarnings").FirstOrDefault();
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
                return unitOfWork.BeltEarningRepository.Get(null, null, "Student").Select(x => new BeltEarningBasic()
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
                BeltEarning beltEarningToRemove = unitOfWork.BeltEarningRepository.GetByID(beltEarningId);
                if (beltEarningToRemove != null)
                {
                    unitOfWork.BeltEarningRepository.Delete(beltEarningToRemove);
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

    }
}
