using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tnguyenResume.Bussiness.Model;

namespace tnguyenResume.Bussiness.Interface
{
    public interface IinformationDAL
    {
        Information GetInformation();
        Information GetInformationById(Guid id);
        IEnumerable<Information> GetInformations();
        bool Update(Information infor);
    }
}
