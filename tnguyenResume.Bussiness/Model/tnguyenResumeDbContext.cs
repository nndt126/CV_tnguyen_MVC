namespace tnguyenResume.Bussiness.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Interface;
    public partial class tnguyenResumeDbContext : DbContext, ItnguyenResumeDbContext
    {
        static tnguyenResumeDbContext()
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<tnguyenResumeDbContext, Configuration>());
            Database.SetInitializer<tnguyenResumeDbContext>(new CreateDatabaseIfNotExists<tnguyenResumeDbContext>());
        }

        public tnguyenResumeDbContext()
            : base("name=tnguyenResumeDbContext")
        {
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
