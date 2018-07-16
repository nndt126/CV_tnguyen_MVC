using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tnguyenResume.Bussiness.Model;

namespace tnguyenResume.Bussiness.Interface
{
    public interface ItnguyenResumeDbContext
    {
        DbSet<Account> Accounts { get; set; }
        DbSet<Education> Educations { get; set; }
        DbSet<Information> Information { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<Skill> Skills { get; set; }
        DbSet<Testimonial> Testimonials { get; set; }
        DbSet<Work> Works { get; set; }
        int SaveChanges();
    }
}
