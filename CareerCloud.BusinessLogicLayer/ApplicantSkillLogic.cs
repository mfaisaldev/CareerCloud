﻿using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantSkillLogic : BaseLogic<ApplicantSkillPoco>
    {
        public ApplicantSkillLogic(IDataRepository<ApplicantSkillPoco> repository) : base(repository)
        {

        }

        public override List<ApplicantSkillPoco> GetAll()
        {
            return base.GetAll();
        }

        public override ApplicantSkillPoco Get(Guid id)
        {
            return base.Get(id);
        }

        public new void Delete(ApplicantSkillPoco[] pocos)
        {
            base.Delete(pocos);
        }

        public override void Add(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(ApplicantSkillPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach(ApplicantSkillPoco poco in pocos)
            {
                if(poco.StartMonth>12)
                {
                    exceptions.Add(new ValidationException(101, $"Cannot be greater than 12"));
                }
                if(poco.EndMonth>12)
                {
                    exceptions.Add(new ValidationException(102, $"Cannot be greater than 12"));
                }
                if(poco.StartYear<1900)
                {
                    exceptions.Add(new ValidationException(103, $"Cannot be less than 1900"));
                }
                if(poco.EndYear<DateTime.Now.Year)
                {
                    exceptions.Add(new ValidationException(104, $"Cannot be less then Statr year"));
                }
            }
            
            if(exceptions.Count>0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
