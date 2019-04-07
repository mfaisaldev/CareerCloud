using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CareerCloud.WCF
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICompany" in both code and config file together.
	[ServiceContract]
	public interface ICompany
	{
		//[OperationContract]
		//void DoWork();

		[OperationContract]
		void AddCompanyDescription(CompanyDescriptionPoco[] items);
		[OperationContract]
		List<CompanyDescriptionPoco> GetAllCompanyDescription();
		[OperationContract]
		CompanyDescriptionPoco GetSingleCompanyDescription(string id);
		[OperationContract]
		void RemoveCompanyDescription(CompanyDescriptionPoco[] items);
		[OperationContract]
		void UpdateCompanyDescription(CompanyDescriptionPoco[] items);

		[OperationContract]
		void AddCompanyJobDescription(CompanyJobDescriptionPoco[] items);
		[OperationContract]
		List<CompanyJobDescriptionPoco> GetAllCompanyJobDescription();
		[OperationContract]
		CompanyJobDescriptionPoco GetSingleCompanyJobDescription(string id);
		[OperationContract]
		void RemoveCompanyJobDescription(CompanyJobDescriptionPoco[] items);
		[OperationContract]
		void UpdateCompanyJobDescription(CompanyJobDescriptionPoco[] items);

		[OperationContract]
		void AddCompanyJobEducation(CompanyJobEducationPoco[] items);
		[OperationContract]
		List<CompanyJobEducationPoco> GetAllCompanyJobEducation();
		[OperationContract]
		CompanyJobEducationPoco GetSingleCompanyJobEducation(string id);
		[OperationContract]
		void RemoveCompanyJobEducation(CompanyJobEducationPoco[] items);
		[OperationContract]
		void UpdateCompanyJobEducation(CompanyJobEducationPoco[] items);

		[OperationContract]
		void AddCompanyJob(CompanyJobPoco[] items);
		[OperationContract]
		List<CompanyJobPoco> GetAllCompanyJob();
		[OperationContract]
		CompanyJobPoco GetSingleCompanyJob(string id);
		[OperationContract]
		void RemoveCompanyJob(CompanyJobPoco[] items);
		[OperationContract]
		void UpdateCompanyJob(CompanyJobPoco[] items);

		[OperationContract]
		void AddCompanyJobSkill(CompanyJobSkillPoco[] items);
		[OperationContract]
		List<CompanyJobSkillPoco> GetAllCompanyJobSkill();
		[OperationContract]
		CompanyJobSkillPoco GetSingleCompanyJobSkill(string id);
		[OperationContract]
		void RemoveCompanyJobSkill(CompanyJobSkillPoco[] items);
		[OperationContract]
		void UpdateCompanyJobSkill(CompanyJobSkillPoco[] items);

		[OperationContract]
		void AddCompanyLocation(CompanyLocationPoco[] items);
		[OperationContract]
		List<CompanyLocationPoco> GetAllCompanyLocation();
		[OperationContract]
		CompanyLocationPoco GetSingleCompanyLocation(string id);
		[OperationContract]
		void RemoveCompanyLocation(CompanyLocationPoco[] items);
		[OperationContract]
		void UpdateCompanyLocation(CompanyLocationPoco[] items);

		[OperationContract]
		void AddCompanyProfile(CompanyProfilePoco[] items);
		[OperationContract]
		List<CompanyProfilePoco> GetAllCompanyProfile();
		[OperationContract]
		CompanyProfilePoco GetSingleCompanyProfile(string id);
		[OperationContract]
		void RemoveCompanyProfile(CompanyProfilePoco[] items);
		[OperationContract]
		void UpdateCompanyProfile(CompanyProfilePoco[] items);
	}
}
