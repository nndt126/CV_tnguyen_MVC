using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tnguyenResume.Bussiness.Interface;
using tnguyenResume.Bussiness.Model;

namespace tnguyenResume.Bussiness.DAL
{
    public class EducationDAL : IEducationDAL
    {
        private readonly ItnguyenResumeDbContext _itnguyenResumeDbContext;

        public EducationDAL(ItnguyenResumeDbContext itnguyenResumeDbContext)
        {
            _itnguyenResumeDbContext = itnguyenResumeDbContext;
        }

        public IEnumerable<Education> GetAllEducation()
        {
            var userId = _itnguyenResumeDbContext.Educations.FirstOrDefault().ID_User ?? Guid.Empty;
            var result = _itnguyenResumeDbContext.Educations.Where(t => t.ID_User == userId).OrderBy(t => t.EducationDate.Value.Year).ToList();
            return result ?? null;
        }

        public IEnumerable<Education> GetAllEducationById(Guid educationID)
        {
            var result = _itnguyenResumeDbContext.Educations.Where(n => n.ID == educationID);
            return result ?? null;
        }
    }
}
