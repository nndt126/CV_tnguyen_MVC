using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tnguyenResume.Bussiness.Interface;
using tnguyenResume.Bussiness.Model;

namespace tnguyenResume.Bussiness.DAL
{
    public class WorkDAL : IWorkDAL
    {
        private readonly ItnguyenResumeDbContext _itnguyenResumeDbContext;

        public WorkDAL(ItnguyenResumeDbContext itnguyenResumeDbContext)
        {
            _itnguyenResumeDbContext = itnguyenResumeDbContext;
        }

        public IEnumerable<Work> GetAllWorks()
        {
            var userId = _itnguyenResumeDbContext.Works.FirstOrDefault().ID_User ?? Guid.Empty;
            var result = _itnguyenResumeDbContext.Works.Where(t => t.ID_User == userId).OrderByDescending(t => t.WorksDate.Value.Year).ToList();
            return result ?? null;
        }

        public Work GetWorkById(Guid workId)
        {
            var result = _itnguyenResumeDbContext.Works.Where(n => n.ID == workId).FirstOrDefault();
            return result ?? null;
        }

        public Guid Insert(Work wk)
        {
            var result = _itnguyenResumeDbContext.Works.Add(wk);
            _itnguyenResumeDbContext.SaveChanges();
            return result.ID;
        }

        public bool Update(Work wk)
        {
            try
            {
                //_itnguyenResumeDbContext.Entry(infor).State = EntityState.Modified;
                _itnguyenResumeDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }
        }

        public bool Delete(Work wk)
        {
            try
            {
                var work = _itnguyenResumeDbContext.Works.Find(wk.ID);
                _itnguyenResumeDbContext.Works.Remove(work);
                _itnguyenResumeDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }
    }
}
