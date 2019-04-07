using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantJobApplicationLogic : BaseLogic<ApplicantJobApplicationPoco>
    {
        public ApplicantJobApplicationLogic(IDataRepository<ApplicantJobApplicationPoco> repository) : base(repository)
        {

        }

        public override List<ApplicantJobApplicationPoco> GetAll()
        {
            return base.GetAll();
        }

        public override ApplicantJobApplicationPoco Get(Guid id)
        {
            return base.Get(id);
        }

        public new void Delete(ApplicantJobApplicationPoco[] pocos)
        {
            base.Delete(pocos);
        }

        public override void Add(ApplicantJobApplicationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(ApplicantJobApplicationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(ApplicantJobApplicationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach(ApplicantJobApplicationPoco poco in pocos)
            {
                if(poco.ApplicationDate>DateTime.Now)
                {
                    exceptions.Add(new ValidationException(110, $"Application Date cannot be greated than today"));
                }
            }
            if(exceptions.Count>0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
