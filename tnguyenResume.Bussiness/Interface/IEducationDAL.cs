using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tnguyenResume.Bussiness.Model;

namespace tnguyenResume.Bussiness.Interface
{
    public interface IEducationDAL
    {
        IEnumerable<Education> GetAllEducation();
        IEnumerable<Education> GetAllEducationById(Guid educationID);
    }
}
