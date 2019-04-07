using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantEducationLogic : BaseLogic<ApplicantEducationPoco>
    {
        public ApplicantEducationLogic(IDataRepository<ApplicantEducationPoco> repository) : base(repository)
        {

        }

        public override List<ApplicantEducationPoco> GetAll()
        {
            return base.GetAll();
        }

        public override ApplicantEducationPoco Get(Guid id)
        {
            return base.Get(id);
        }
                
        public new void Delete(ApplicantEducationPoco[] pocos) 
        {
            base.Delete(pocos);
        }

        public override void Add(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);

            base.Add(pocos);
        }

        public override void Update(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);

            base.Update(pocos);
        }

        protected override void Verify(ApplicantEducationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach(ApplicantEducationPoco items in pocos)
            {
                if(string.IsNullOrEmpty(items.Major) || items.Major.Length<4)
                {
                    exceptions.Add(new ValidationException(107, $"Cannot be empty or less than 3 characters"));
                }
                if(items.StartDate>DateTime.Now)
                {
                    exceptions.Add(new ValidationException(108, $"Cannot be greated than today"));
                }
                if(items.CompletionDate<items.StartDate)
                {
                    exceptions.Add(new ValidationException(109, $"Completion Date can not earlier than Start Date"));
                }
            }
            if(exceptions.Count>0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
