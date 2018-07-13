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
        IEnumerable<Work> GetWorkById(int bookId);
    }
}
