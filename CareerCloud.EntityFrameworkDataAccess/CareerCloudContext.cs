using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;

namespace CareerCloud.EntityFrameworkDataAccess
{
   public class CareerCloudContext : BaseRepository
    {

        DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
        DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
        DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
        DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistories { get; set; }
        DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        DbSet<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }
        DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        DbSet<CompanyJobPoco> CompanyJobs { get; set; }
        DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
        DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }
        DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
        DbSet<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set; }
        DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
        DbSet<SecurityRolePoco> SecurityRoles { get; set; }
        DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
        DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connStr);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            //ApplicantEducationPoco
            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(a => a.ApplicantEducations)
                .WithOne(e => e.ApplicantProfile)
                .HasForeignKey(l => l.Applicant);

            //ApplicantJobApplicationPoco
            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(a => a.ApplicantJobApplications)
                .WithOne(j => j.ApplicantProfile)
                .HasForeignKey(l => l.Applicant);

            modelBuilder.Entity<CompanyJobPoco>()
                .HasMany(a => a.ApplicantJobApplications)
                .WithOne(j => j.CompanyJob)
                .HasForeignKey(l => l.Job);

            //ApplicantProfilePoco
            modelBuilder.Entity<SecurityLoginPoco>()
                .HasMany(a => a.ApplicantProfiles)
                .WithOne(j => j.SecurityLogin)
                .HasForeignKey(l => l.Login);
            

            //ApplicantResumePoco
            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(a => a.ApplicantResumes)
                .WithOne(j => j.ApplicantProfile)
                .HasForeignKey(l => l.Applicant);


            //ApplicantSkillPoco
            modelBuilder.Entity<ApplicantProfilePoco>()
               .HasMany(a => a.ApplicantSkills)
               .WithOne(j => j.ApplicantProfile)
               .HasForeignKey(l => l.Applicant);


            //ApplicantWorkHistoryPoco
            modelBuilder.Entity<SystemCountryCodePoco>()
               .HasMany(a => a.ApplicantWorkHistories)
               .WithOne(j => j.SystemCountryCode)
               .HasForeignKey(l => l.CountryCode);

            modelBuilder.Entity<SystemCountryCodePoco>()
                .HasMany(a => a.ApplicantProfiles)
                .WithOne(j => j.SystemCountryCode)
                .HasForeignKey(l => l.Country);

            modelBuilder.Entity<ApplicantProfilePoco>()
               .HasMany(a => a.ApplicantWorkHistorys)
               .WithOne(j => j.ApplicantProfile)
               .HasForeignKey(l => l.Applicant);


            //CompanyDescriptionPoco
            modelBuilder.Entity<CompanyProfilePoco>()
               .HasMany(a => a.CompanyDescriptions)
               .WithOne(j => j.CompanyProfile)
               .HasForeignKey(l => l.Company);
            modelBuilder.Entity<SystemLanguageCodePoco>()
               .HasMany(a => a.CompanyDescriptions)
               .WithOne(j => j.SystemLanguageCode)
               .HasForeignKey(l => l.LanguageId);

            //CompanyJobDescriptionPoco
            modelBuilder.Entity<CompanyJobPoco>()
               .HasMany(a => a.CompanyJobDescriptions)
               .WithOne(j => j.CompanyJob)
               .HasForeignKey(l => l.Job);


            //CompanyJobEducationPoco
            modelBuilder.Entity<CompanyJobPoco>()
              .HasMany(a => a.CompanyJobEducations)
              .WithOne(j => j.CompanyJob)
              .HasForeignKey(l => l.Job);


            //CompanyJobPoco
            modelBuilder.Entity<CompanyProfilePoco>()
              .HasMany(a => a.CompanyJobs)
              .WithOne(j => j.CompanyProfile)
              .HasForeignKey(l => l.Company);


            //CompanyJobSkillPoco
            modelBuilder.Entity<CompanyJobPoco>()
              .HasMany(a => a.CompanyJobSkills)
              .WithOne(j => j.CompanyJob)
              .HasForeignKey(l => l.Job);

            //CompanyLocationPoco
            modelBuilder.Entity<CompanyProfilePoco>()
              .HasMany(a => a.CompanyLocations)
              .WithOne(j => j.CompanyProfile)
              .HasForeignKey(l => l.Company);


            //SecurityLoginsLogPoco
            modelBuilder.Entity<SecurityLoginPoco>()
              .HasMany(a => a.SecurityLoginsLogs)
              .WithOne(j => j.SecurityLogin)
              .HasForeignKey(l => l.Login);


            //SecurityLoginsRolePoco
            modelBuilder.Entity<SecurityLoginPoco>()
              .HasMany(a => a.SecurityLoginsRoles)
              .WithOne(j => j.SecurityLogin)
              .HasForeignKey(l => l.Login);

            modelBuilder.Entity<SecurityRolePoco>()
              .HasMany(a => a.SecurityLoginsRoles)
              .WithOne(j => j.SecurityRole)
              .HasForeignKey(l => l.Role);

            //////////////////////////////////////////////////////////////////////////////////////

            modelBuilder.Entity<CompanyJobDescriptionPoco>(entity =>
            {
                entity.Property(e => e.TimeStamp)
                    .IsRowVersion();
            });

            modelBuilder.Entity<CompanyDescriptionPoco>(entity =>
            {
                entity.Property(e => e.LanguageId)
                    .HasColumnName("LanguageID");

                entity.Property(e => e.TimeStamp)
                    .IsRowVersion();
            });

            modelBuilder.Entity<CompanyProfilePoco>(entity =>
            {
                entity.Property(e => e.TimeStamp)
                    .IsRowVersion();
            });

            modelBuilder.Entity<CompanyJobPoco>(entity =>
            {
                entity.Property(e => e.TimeStamp)
                    .IsRowVersion();
            });

            modelBuilder.Entity<CompanyLocationPoco>(entity =>
            {
                entity.Property(e => e.TimeStamp)
                    .IsRowVersion();
            });

            modelBuilder.Entity<CompanyJobEducationPoco>(entity =>
            {
                entity.Property(e => e.TimeStamp)
                    .IsRowVersion();
            });

            modelBuilder.Entity<CompanyJobSkillPoco>(entity =>
            {
                entity.Property(e => e.TimeStamp)
                    .IsRowVersion();
            });
            modelBuilder.Entity<SecurityLoginPoco>(entity =>
            {
                entity.Property(e => e.TimeStamp)
                    .IsRowVersion();
            });
            modelBuilder.Entity<SecurityLoginsRolePoco>(entity =>
            {
                entity.Property(e => e.TimeStamp)
                    .IsRowVersion();
            });
            modelBuilder.Entity<ApplicantProfilePoco>(entity =>
            {
                entity.Property(e => e.TimeStamp)
                    .IsRowVersion();
            });
            modelBuilder.Entity<ApplicantEducationPoco>(entity =>
            {
                entity.Property(e => e.TimeStamp)
                    .IsRowVersion();
            });
            modelBuilder.Entity<ApplicantJobApplicationPoco>(entity =>
            {
                entity.Property(e => e.TimeStamp)
                    .IsRowVersion();
            });
            modelBuilder.Entity<ApplicantSkillPoco>(entity =>
            {
                entity.Property(e => e.TimeStamp)
                    .IsRowVersion();
            });
            modelBuilder.Entity<ApplicantWorkHistoryPoco>(entity =>
            {
                entity.Property(e => e.TimeStamp)
                    .IsRowVersion();
            });


        }

    }
}
