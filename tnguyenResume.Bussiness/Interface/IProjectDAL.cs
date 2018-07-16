using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tnguyenResume.Bussiness.Model;

namespace tnguyenResume.Bussiness.Interface
{
    public interface IProjectDAL
    {
        IEnumerable<Project> GetProjects(int number);
        Project GetProjectById(Guid projectId);
        Guid Insert(Project prj);
        bool Update(Project prj);
        bool Delete(Project prj);
    }
}
