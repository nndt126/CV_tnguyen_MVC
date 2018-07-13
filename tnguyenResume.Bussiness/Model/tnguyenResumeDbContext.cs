namespace tnguyenResume.Bussiness.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Interface;

    public partial class tnguyenResumeDbContext : DbContext, ItnguyenResumeDbContext
    {
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
            modelBuilder.Entity<Information>()
                .HasOptional(e => e.Account)
                .WithRequired(e => e.Information);

            modelBuilder.Entity<Information>()
                .HasMany(e => e.Educations)
                .WithOptional(e => e.Information)
                .HasForeignKey(e => e.ID_User);

            modelBuilder.Entity<Information>()
                .HasMany(e => e.Projects)
                .WithOptional(e => e.Information)
                .HasForeignKey(e => e.ID_User);

            modelBuilder.Entity<Information>()
                .HasMany(e => e.Skills)
                .WithOptional(e => e.Information)
                .HasForeignKey(e => e.ID_User);

            modelBuilder.Entity<Information>()
                .HasMany(e => e.Testimonials)
                .WithOptional(e => e.Information)
                .HasForeignKey(e => e.ID_User);

            modelBuilder.Entity<Information>()
                .HasMany(e => e.Works)
                .WithOptional(e => e.Information)
                .HasForeignKey(e => e.ID_User);
        }
    }
}
