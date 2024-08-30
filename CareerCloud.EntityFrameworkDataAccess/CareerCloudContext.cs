using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class CareerCloudContext : DbContext
    {
        public DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        public DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
        public DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistories { get; set; }
        public DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        public DbSet<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }
        public DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        public DbSet<CompanyJobPoco> CompanyJobs { get; set; }
        public DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        public DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
        public DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }
        public DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set; }
        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
        public DbSet<SecurityRolePoco> SecurityRoles { get; set; }
        public DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
        public DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=JOB_PORTAL_DB; Integrated Security=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ignore the Id property for certain POCOs
            modelBuilder.Entity<SystemCountryCodePoco>().HasKey(e => e.Code);
            modelBuilder.Entity<SystemLanguageCodePoco>().HasKey(e => e.LanguageID);

            // Configure Timestamp properties
            ConfigureTimestamps(modelBuilder);

            // Configure relationships
            ConfigureRelationships(modelBuilder);
        }

        private void ConfigureTimestamps(ModelBuilder modelBuilder)
        {
            // Use IsRowVersion for concurrency control
            var timestampEntities = new[]
            {
                typeof(ApplicantProfilePoco),
                typeof(ApplicantEducationPoco),
                typeof(ApplicantJobApplicationPoco),
                typeof(ApplicantSkillPoco),
                typeof(ApplicantWorkHistoryPoco),
                typeof(CompanyProfilePoco),
                typeof(CompanyDescriptionPoco),
                typeof(CompanyJobPoco),
                typeof(CompanyJobDescriptionPoco),
                typeof(CompanyLocationPoco),
                typeof(CompanyJobEducationPoco),
                typeof(CompanyJobSkillPoco),
                typeof(SecurityLoginPoco),
                typeof(SecurityLoginsRolePoco)
            };

            foreach (var entityType in timestampEntities)
            {
                modelBuilder.Entity(entityType)
                    .Property("TimeStamp")
                    .IsRowVersion();
            }
        }

        private void ConfigureRelationships(ModelBuilder modelBuilder)
        {
            // Define relationships with foreign keys
            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasOne(item => item.SystemCountryCode)
                .WithMany(item => item.ApplicantProfiles)
                .HasForeignKey(item => item.Country);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasOne(item => item.SecurityLogin)
                .WithMany(item => item.ApplicantProfiles)
                .HasForeignKey(item => item.Login);

            modelBuilder.Entity<ApplicantEducationPoco>()
                .HasOne(item => item.ApplicantProfile)
                .WithMany(item => item.ApplicantEducations)
                .HasForeignKey(item => item.Applicant);

            modelBuilder.Entity<ApplicantJobApplicationPoco>()
                .HasOne(item => item.ApplicantProfile)
                .WithMany(item => item.ApplicantJobApplications)
                .HasForeignKey(item => item.Applicant);

            modelBuilder.Entity<ApplicantJobApplicationPoco>()
                .HasOne(item => item.CompanyJob)
                .WithMany(item => item.ApplicantJobApplications)
                .HasForeignKey(item => item.Job);

            modelBuilder.Entity<ApplicantResumePoco>()
                .HasOne(item => item.ApplicantProfile)
                .WithMany(item => item.ApplicantResumes)
                .HasForeignKey(item => item.Applicant);

            modelBuilder.Entity<ApplicantSkillPoco>()
                .HasOne(item => item.ApplicantProfile)
                .WithMany(item => item.ApplicantSkills)
                .HasForeignKey(item => item.Applicant);

            modelBuilder.Entity<ApplicantWorkHistoryPoco>()
                .HasOne(item => item.ApplicantProfile)
                .WithMany(item => item.ApplicantWorkHistorys)
                .HasForeignKey(item => item.Applicant);

            modelBuilder.Entity<ApplicantWorkHistoryPoco>()
                .HasOne(item => item.SystemCountryCode)
                .WithMany(item => item.ApplicantWorkHistories)
                .HasForeignKey(item => item.CountryCode);

            modelBuilder.Entity<CompanyDescriptionPoco>()
                .HasOne(item => item.SystemLanguageCode)
                .WithMany(item => item.CompanyDescriptions)
                .HasForeignKey(item => item.LanguageId);

            modelBuilder.Entity<CompanyDescriptionPoco>()
                .HasOne(item => item.CompanyProfile)
                .WithMany(item => item.CompanyDescriptions)
                .HasForeignKey(item => item.Company);

            modelBuilder.Entity<CompanyJobPoco>()
                .HasOne(item => item.CompanyProfile)
                .WithMany(item => item.CompanyJobs)
                .HasForeignKey(item => item.Company);

            modelBuilder.Entity<CompanyJobDescriptionPoco>()
                .HasOne(item => item.CompanyJob)
                .WithMany(item => item.CompanyJobDescriptions)
                .HasForeignKey(item => item.Job);

            modelBuilder.Entity<CompanyLocationPoco>()
                .HasOne(item => item.CompanyProfile)
                .WithMany(item => item.CompanyLocations)
                .HasForeignKey(item => item.Company);

            modelBuilder.Entity<CompanyLocationPoco>()
                .HasOne(item => item.SystemCountryCode)
                .WithMany(item => item.CompanyLocations)
                .HasForeignKey(item => item.CountryCode);

            modelBuilder.Entity<CompanyJobEducationPoco>()
                .HasOne(item => item.CompanyJob)
                .WithMany(item => item.CompanyJobEducations)
                .HasForeignKey(item => item.Job);

            modelBuilder.Entity<CompanyJobSkillPoco>()
                .HasOne(item => item.CompanyJob)
                .WithMany(item => item.CompanyJobSkills)
                .HasForeignKey(item => item.Job);

            modelBuilder.Entity<SecurityLoginsLogPoco>()
                .HasOne(item => item.SecurityLogin)
                .WithMany(item => item.SecurityLoginsLogs)
                .HasForeignKey(item => item.Login);

            modelBuilder.Entity<SecurityLoginsRolePoco>()
                .HasOne(item => item.SecurityRole)
                .WithMany(item => item.SecurityLoginsRoles)
                .HasForeignKey(item => item.Role);

            modelBuilder.Entity<SecurityLoginsRolePoco>()
                .HasOne(item => item.SecurityLogin)
                .WithMany(item => item.SecurityLoginsRoles)
                .HasForeignKey(item => item.Login);
        }
    }
}
