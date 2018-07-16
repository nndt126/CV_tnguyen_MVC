using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tnguyenResume.Bussiness.Model;

namespace tnguyenResume.Bussiness.Interface
{
    public interface IWorkDAL
    {
        IEnumerable<Work> GetAllWorks();
        Work GetWorkById(Guid workId);
        Guid Insert(Work wk);
        bool Update(Work wk);
        bool Delete(Work wk);
    }
}
