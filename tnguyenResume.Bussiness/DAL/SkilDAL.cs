using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tnguyenResume.Bussiness.Interface;
using tnguyenResume.Bussiness.Model;

namespace tnguyenResume.Bussiness.DAL
{
    public class SkilDAL : ISkillDAL
    {
        private readonly ItnguyenResumeDbContext _itnguyenResumeDbContext;

        public SkilDAL(ItnguyenResumeDbContext itnguyenResumeDbContext)
        {
            _itnguyenResumeDbContext = itnguyenResumeDbContext;
        }

        public IEnumerable<Skill> GetAllSkill()
        {
            var userId = _itnguyenResumeDbContext.Skills.FirstOrDefault().ID_User ?? Guid.Empty;
            var result = _itnguyenResumeDbContext.Skills.Where(t => t.ID_User == userId).ToList();
            return result ?? null;
        }

        public Skill GetSkillById(Guid skillId)
        {
            var result = _itnguyenResumeDbContext.Skills.Where(n => n.ID == skillId).FirstOrDefault();
            return result ?? null;
        }
    }
}
