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
        //private ItnguyenResumeDbContext _itnguyenResumeDbContext;

        tnguyenResumeDbContext _itnguyenResumeDbContext = null;

        public WorkDAL()
        {
            _itnguyenResumeDbContext = new tnguyenResumeDbContext();
        }


        public IEnumerable<Work> GetAllWorks()
        {
            var userId = _itnguyenResumeDbContext.Works.FirstOrDefault().ID_User ?? Guid.Empty;
            var result = _itnguyenResumeDbContext.Works.Where(t => t.ID_User == userId).OrderByDescending(t => t.WorksDate.Value.Year).ToList();
            return result ?? null;
        }

        public IEnumerable<Work> GetWorkById(Guid bookId)
        {
            var result = _itnguyenResumeDbContext.Works.Where(n => n.ID == bookId);
            return result ?? null;
        }
    }
}
