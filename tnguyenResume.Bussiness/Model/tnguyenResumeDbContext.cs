namespace tnguyenResume.Bussiness.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class tnguyenResumeDbContext : DbContext
    {
        public tnguyenResumeDbContext()
            : base("name=tnguyenResumeDbContext")
        {
            try
            {
                Database.SetInitializer<tnguyenResumeDbContext>(null);
                Database.SetInitializer(new CreateDatabaseIfNotExists<tnguyenResumeDbContext>());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<EmailTnguyen> EmailTnguyens { get; set; }
        public virtual DbSet<Information> Information { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<Testimonial> Testimonials { get; set; }
        public virtual DbSet<Work> Works { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
