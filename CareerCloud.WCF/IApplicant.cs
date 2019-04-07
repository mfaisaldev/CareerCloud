using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CareerCloud.WCF
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IApplicant" in both code and config file together.
	[ServiceContract]
	public interface IApplicant
	{
		[OperationContract]
		void AddApplicantEducation(ApplicantEducationPoco[] items);
		[OperationContract]
		List<ApplicantEducationPoco> GetAllApplicantEducation();
		[OperationContract]
		ApplicantEducationPoco GetSingleApplicantEducation(String id);
		[OperationContract]
		void RemoveApplicantEducation(ApplicantEducationPoco[] items);
		[OperationContract]
		void UpdateApplicantEducation(ApplicantEducationPoco[] items);

		[OperationContract]
		void AddApplicantJobApplication(ApplicantJobApplicationPoco[] items);
		[OperationContract]
		List<ApplicantJobApplicationPoco> GetAllApplicantJobApplication();
		[OperationContract]
		ApplicantJobApplicationPoco GetSingleApplicantJobApplication(String id);
		[OperationContract]
		void RemoveApplicantJobApplication(ApplicantJobApplicationPoco[] items);
		[OperationContract]
		void UpdateApplicantJobApplication(ApplicantJobApplicationPoco[] items);

		[OperationContract]
		void AddApplicantProfile(ApplicantProfilePoco[] items);
		[OperationContract]
		List<ApplicantProfilePoco> GetAllApplicantProfile();
		[OperationContract]
		ApplicantProfilePoco GetSingleApplicantProfile(String id);
		[OperationContract]
		void RemoveApplicantProfile(ApplicantProfilePoco[] items);
		[OperationContract]
		void UpdateApplicantProfile(ApplicantProfilePoco[] items);

		[OperationContract]
		void AddApplicantResume(ApplicantResumePoco[] items);
		[OperationContract]
		List<ApplicantResumePoco> GetAllApplicantResume();
		[OperationContract]
		ApplicantResumePoco GetSingleApplicantResume(string id);
		[OperationContract]
		void RemoveApplicantResume(ApplicantResumePoco[] items);
		[OperationContract]
		void UpdateApplicantResume(ApplicantResumePoco[] items);

		[OperationContract]
		void AddApplicantSkill(ApplicantSkillPoco[] items);
		[OperationContract]
		List<ApplicantSkillPoco> GetAllApplicantSkill();
		[OperationContract]
		ApplicantSkillPoco GetSingleApplicantSkill(string id);
		[OperationContract]
		void RemoveApplicantSkill(ApplicantSkillPoco[] items);
		[OperationContract]
		void UpdateApplicantSkill(ApplicantSkillPoco[] items);

		[OperationContract]
		void AddApplicantWorkHistory(ApplicantWorkHistoryPoco[] items);
		[OperationContract]
		List<ApplicantWorkHistoryPoco> GetAllApplicantWorkHistory();
		[OperationContract]
		ApplicantWorkHistoryPoco GetSingleApplicantWorkHistory(string id);
		[OperationContract]
		void RemoveApplicantWorkHistory(ApplicantWorkHistoryPoco[] items);
		[OperationContract]
		void UpdateApplicantWorkHistory(ApplicantWorkHistoryPoco[] items);
	}
}
