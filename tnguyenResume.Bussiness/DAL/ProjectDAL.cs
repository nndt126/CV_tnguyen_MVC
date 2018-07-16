using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tnguyenResume.Bussiness.Interface;
using tnguyenResume.Bussiness.Model;

namespace tnguyenResume.Bussiness.DAL
{
    public class ProjectDAL : IProjectDAL
    {
        private readonly ItnguyenResumeDbContext _itnguyenResumeDbContext;

        public ProjectDAL(ItnguyenResumeDbContext itnguyenResumeDbContext)
        {
            _itnguyenResumeDbContext = itnguyenResumeDbContext;
        }

        public IEnumerable<Project> GetProjects(int number)
        {
            var userId = _itnguyenResumeDbContext.Projects.FirstOrDefault().ID_User ?? Guid.Empty;
            var result = new List<Project>();
            if (number > 0)
                result = _itnguyenResumeDbContext.Projects.Where(t => t.ID_User == userId).OrderByDescending(t => t.ProjectTime).Take(number).ToList();
            else
                result = _itnguyenResumeDbContext.Projects.Where(t => t.ID_User == userId).OrderByDescending(t => t.ProjectTime).ToList();
            return result ?? null;
        }

        public Project GetProjectById(Guid projectId)
        {
            var result = _itnguyenResumeDbContext.Projects.Where(n => n.ID == projectId).FirstOrDefault();
            return result ?? null;
        }

        public Guid Insert(Project prj)
        {
            var result = _itnguyenResumeDbContext.Projects.Add(prj);
            _itnguyenResumeDbContext.SaveChanges();
            return result.ID;
        }

        public bool Update(Project prj)
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

        public bool Delete(Project prj)
        {
            try
            {
                var project = _itnguyenResumeDbContext.Projects.Find(prj.ID);
                _itnguyenResumeDbContext.Projects.Remove(project);
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
