using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tnguyenResume.Bussiness.Interface;
using tnguyenResume.Bussiness.Model;

namespace tnguyenResume.Bussiness.DAL
{
    public class InformationDAL : IinformationDAL
    {
        private readonly ItnguyenResumeDbContext _itnguyenResumeDbContext;

        public InformationDAL(ItnguyenResumeDbContext itnguyenResumeDbContext)
        {
            _itnguyenResumeDbContext = itnguyenResumeDbContext;
        }

        public Information GetInformation()
        {
            var result = _itnguyenResumeDbContext.Information.FirstOrDefault() ?? null;
            return result;
        }

        public IEnumerable<Information> GetInformations()
        {
            var result = _itnguyenResumeDbContext.Information.ToList() ?? null;
            return result;
        }

        public Information GetInformationById(Guid id)
        {
            var result = _itnguyenResumeDbContext.Information.Where(n=>n.ID == id).FirstOrDefault() ?? null;
            return result;
        }

        public bool Update(Information infor)
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
    }
}
