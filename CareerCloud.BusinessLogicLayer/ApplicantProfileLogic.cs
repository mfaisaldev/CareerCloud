using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantProfileLogic : BaseLogic<ApplicantProfilePoco>
    {
        public ApplicantProfileLogic(IDataRepository<ApplicantProfilePoco> repository) : base(repository)
        {

        }

        public override List<ApplicantProfilePoco> GetAll()
        {
            return base.GetAll();
        }

        public override ApplicantProfilePoco Get(Guid id)
        {
            return base.Get(id);
        }

        public new void Delete(ApplicantProfilePoco[] pocos)
        {
            base.Delete(pocos);
        }
        public override void Add(ApplicantProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(ApplicantProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(ApplicantProfilePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach(ApplicantProfilePoco poco in pocos)
            {
                if(poco.CurrentSalary<0)
                {
                    exceptions.Add(new ValidationException(111, $"Current Salary can not be negative"));
                }
                if(poco.CurrentRate<0)
                {
                    exceptions.Add(new ValidationException(112, $"Current rate cannot be negative"));
                }                
            }

            if(exceptions.Count>0)
            {
                throw new AggregateException(exceptions);
            }
            //base.Verify(pocos);
        }

    }
}
