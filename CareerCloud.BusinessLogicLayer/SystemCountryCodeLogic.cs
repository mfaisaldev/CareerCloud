using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemCountryCodeLogic //: BaseLogic<SystemCountryCodePoco>
    {
        public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco> repository)// : base(repository)
        {

        }

        public List<SystemCountryCodePoco> GetAll()
        {
            return GetAll();
        }

        public SystemCountryCodePoco Get(Guid id)
        {
            return Get(id);
        }

        public void Delete(SystemCountryCodePoco[] pocos)
        {
            Delete(pocos);
        }

        public void Add(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);            
        }

        public void Update(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
        }

        protected void Verify(SystemCountryCodePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach(SystemCountryCodePoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Code))
                {
                    exceptions.Add(new ValidationException(900, $"Code cannot be empty"));
                }
                if (string.IsNullOrEmpty(poco.Name))
                {
                    exceptions.Add(new ValidationException(901, $"Name cannot be empty"));
                }
            }
            if(exceptions.Count>0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
