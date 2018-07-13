using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tnguyenResume.Bussiness.Interface;
using tnguyenResume.Bussiness.Model;

namespace tnguyenResume.Bussiness.DAL
{
    public class WorkDAL
    {
        private readonly ItnguyenResumeDbContext _itnguyenResumeDbContext;

        public WorkDAL(ItnguyenResumeDbContext itnguyenResumeDbContext)
        {
            _itnguyenResumeDbContext = itnguyenResumeDbContext;
        }

        public IEnumerable<Work> GetAllWorks()
        {
            var userId = int.Parse(_itnguyenResumeDbContext.Works.ElementAt(0).ID_User.ToString());
            var result = _itnguyenResumeDbContext.Works.Where(t => t.ID_User == userId).OrderByDescending(t => t.WorksDate.Value.Year).ToList();
            return result ?? null;
        }

        public IEnumerable<Work> GetWorkById(int bookId)
        {
            var result = _itnguyenResumeDbContext.Works.Where(n => n.ID == bookId);
            return result ?? null;
        }
    }
}
