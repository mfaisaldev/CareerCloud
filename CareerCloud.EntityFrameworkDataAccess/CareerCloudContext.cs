using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class CareerCloudContext : DbContext
    {
		//public bool createProxy  { get; set; }
		public CareerCloudContext() : base(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString)
		{
			//Test
			//Configuration.ProxyCreationEnabled = createProxy;
		}
		
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			//throw new UnintentionalCodeFirstException();
			modelBuilder.Entity<ApplicantProfilePoco>()
				.HasMany(e => e.ApplicantEducations)
				.WithRequired(e => e.ApplicantProfiles)
				.HasForeignKey(e => e.Applicant);
			modelBuilder.Entity<ApplicantProfilePoco>()
				.HasMany(e => e.ApplicantJobApplications)
				.WithRequired(e => e.ApplicantProfiles)
				.HasForeignKey(e => e.Applicant);
			modelBuilder.Entity<ApplicantProfilePoco>()
				.HasMany(e => e.ApplicantResumes)
				.WithRequired(e => e.ApplicantProfiles)
				.HasForeignKey(e => e.Applicant);
			modelBuilder.Entity<ApplicantProfilePoco>()
				.HasMany(e => e.ApplicantSkills)
				.WithRequired(e => e.ApplicantProfiles)
				.HasForeignKey(e => e.Applicant);
			modelBuilder.Entity<ApplicantProfilePoco>()
				.HasMany(e => e.ApplicantWorkHistories)
				.WithRequired(e => e.ApplicantProfiles)
				.HasForeignKey(e => e.Applicant);

			modelBuilder.Entity<ApplicantProfilePoco>()
				.HasMany(e => e.ApplicantJobApplications)
				.WithRequired(e => e.ApplicantProfiles)
				.HasForeignKey(e => e.Applicant);

			modelBuilder.Entity<CompanyProfilePoco>()
				.HasMany(e => e.CompanyDescriptions)
				.WithRequired(e => e.CompanyProfiles)
				.HasForeignKey(e => e.Company);			
			modelBuilder.Entity<CompanyProfilePoco>()
				.HasMany(e => e.CompanyJobs)
				.WithRequired(e => e.CompanyProfiles)
				.HasForeignKey(e => e.Company);
			modelBuilder.Entity<CompanyProfilePoco>()
				.HasMany(e => e.CompanyLocations)
				.WithRequired(e => e.CompanyProfiles)
				.HasForeignKey(e => e.Company);

			modelBuilder.Entity<CompanyJobPoco>()
				.HasOptional(e => e.CompanyJobsDescriptions)
				.WithRequired(e => e.CompanyJobs);
				
				//.HasForeignKey(e => e.Job);
			modelBuilder.Entity<CompanyJobPoco>()
				.HasMany(e => e.CompanyJobEducations)
				.WithRequired(e => e.CompanyJobs)
				.HasForeignKey(e => e.Job);
			modelBuilder.Entity<CompanyJobPoco>()
				.HasMany(e => e.CompanyJobSkills)
				.WithRequired(e => e.CompanyJobs)
				.HasForeignKey(e => e.Job);
			modelBuilder.Entity<CompanyJobPoco>()
				.HasMany(e => e.ApplicantJobApplications)
				.WithRequired(e => e.CompanyJobs)
				.HasForeignKey(e => e.Job);
			modelBuilder.Entity<SecurityLoginPoco>()
				.HasMany(e => e.SecurityLoginsRoles)
				.WithRequired(e => e.SecurityLogins)
				.HasForeignKey(e => e.Login);
			modelBuilder.Entity<SecurityRolePoco>()
				.HasMany(e => e.SecurityLoginsRoles)
				.WithRequired(e => e.SecurityRoles)
				.HasForeignKey(e => e.Role);
			modelBuilder.Entity<SecurityLoginPoco>()
				.HasMany(e => e.SecurityLoginsLogs)
				.WithRequired(e => e.SecurityLogins)
				.HasForeignKey(e => e.Login);

			modelBuilder.Entity<SecurityLoginPoco>()
				.HasMany(e => e.ApplicantProfiles)
				.WithRequired(e => e.SecurityLogins)
				.HasForeignKey(e => e.Login);
			modelBuilder.Entity<SystemCountryCodePoco>()
				.HasMany(e => e.ApplicantProfiles)
				.WithRequired(e => e.CountryCodes)
				.HasForeignKey(e => e.Country);

			modelBuilder.Entity<SystemCountryCodePoco>()
				.HasMany(e => e.ApplicantWorkHistory)
				.WithRequired(e => e.CountryCodes)
				.HasForeignKey(e => e.CountryCode);
			

			modelBuilder.Entity<CompanyProfilePoco>().Ignore(e => e.TimeStamp);
			modelBuilder.Entity<CompanyProfilePoco>().Ignore(e => e.TimeStamp);
			modelBuilder.Entity<CompanyDescriptionPoco>().Ignore(e => e.TimeStamp);
			modelBuilder.Entity<CompanyJobDescriptionPoco>().Ignore(e => e.TimeStamp);
			modelBuilder.Entity<CompanyJobPoco>().Ignore(e => e.TimeStamp);
			modelBuilder.Entity<CompanyLocationPoco>().Ignore(e => e.TimeStamp);
			modelBuilder.Entity<CompanyJobEducationPoco>().Ignore(e => e.TimeStamp);
			modelBuilder.Entity<CompanyJobSkillPoco>().Ignore(e => e.TimeStamp);
			modelBuilder.Entity<SecurityLoginPoco>().Ignore(e => e.TimeStamp);
			modelBuilder.Entity<SecurityLoginsRolePoco>().Ignore(e => e.TimeStamp);
			modelBuilder.Entity<ApplicantEducationPoco>().Ignore(e => e.TimeStamp);
			modelBuilder.Entity<ApplicantJobApplicationPoco>().Ignore(e => e.TimeStamp);
			modelBuilder.Entity<ApplicantProfilePoco>().Ignore(e => e.TimeStamp);
			modelBuilder.Entity<ApplicantSkillPoco>().Ignore(e => e.TimeStamp);
			modelBuilder.Entity<ApplicantWorkHistoryPoco>().Ignore(e => e.TimeStamp);
			
			base.OnModelCreating(modelBuilder);
		}
		public virtual DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
		public virtual DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
		public virtual DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
		public virtual DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
		public virtual DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
		public virtual DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistory { get; set; }
		public virtual DbSet<CompanyJobPoco> CompanyJobs { get; set; }
		public virtual DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
		public virtual DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
		public virtual DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
		public virtual DbSet<CompanyJobDescriptionPoco> CompanyJobsDescriptions { get; set; }
		public virtual DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
		public virtual DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }
		public virtual DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
		public virtual DbSet<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set; }
		public virtual DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
		public virtual DbSet<SecurityRolePoco> SecurityRoles { get; set; }
		
		public virtual DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
		public virtual DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }
	}
}
