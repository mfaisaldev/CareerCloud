﻿using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobDescriptionLogic : BaseLogic<CompanyJobDescriptionPoco>
    {
        public CompanyJobDescriptionLogic(IDataRepository<CompanyJobDescriptionPoco> repository) : base(repository)
        {

        }

        public override List<CompanyJobDescriptionPoco> GetAll()
        {
            return base.GetAll();
        }

        public override CompanyJobDescriptionPoco Get(Guid id)
        {
            return base.Get(id);
        }

        public new void Delete(CompanyJobDescriptionPoco[] pocos)
        {
            base.Delete(pocos);
        }

        public override void Add(CompanyJobDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(CompanyJobDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(CompanyJobDescriptionPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (CompanyJobDescriptionPoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.JobName))
                {
                    exceptions.Add(new ValidationException(300, $"Job Name cannot be empty"));
                }
                if(string.IsNullOrEmpty(poco.JobDescriptions))
                {
                    exceptions.Add(new ValidationException(301, $"Job Description cannot be empty"));
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
