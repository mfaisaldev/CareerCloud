using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace CareerCloud.WebAPI.Controllers
{
    [RoutePrefix("api/careercloud/company/v1")]
    public class CompanyJobSkillController : ApiController
    {
        private CompanyJobSkillLogic _logic;

        public CompanyJobSkillController()
        {
            var repo = new EntityFrameworkDataAccess.EFGenericRepository<CompanyJobSkillPoco>();
            _logic = new CompanyJobSkillLogic(repo);
        }

        [HttpGet]
        [Route("JobSkill/{Id}")]
        [ResponseType(typeof(CompanyJobSkillPoco))]
        public IHttpActionResult GetCompanyJobSkill(Guid Id)
        {
			if (Id == null) return BadRequest();
			try
			{
				CompanyJobSkillPoco poco = _logic.Get(Id);

				if (poco == null) return NotFound();

				return Ok(poco);
			}
			catch (Exception e)
			{
				return InternalServerError(e);
			}
		}
        [HttpGet]
        [Route("JobSkill")]
        [ResponseType(typeof(List<CompanyJobSkillPoco>))]
        public IHttpActionResult GetAllCompanyJobSkill()
        {
            try
            {
                List<CompanyJobSkillPoco> pocos = _logic.GetAll();
                if (pocos == null) return NotFound();
                return Ok(pocos);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        [Route("JobSkill")]
        public IHttpActionResult PostCompanyJobSkill([FromBody]CompanyJobSkillPoco[] pocos)
        {
            try
            {
                _logic.Add(pocos);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        [HttpPut]
        [Route("JobSkill")]
        public IHttpActionResult PutCompanyJobSkill([FromBody]CompanyJobSkillPoco[] pocos)
        {
            try
            {
                _logic.Update(pocos);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        [HttpDelete]
        [Route("JobSkill")]
        public IHttpActionResult DeleteCompanyJobSkill([FromBody]CompanyJobSkillPoco[] pocos)
        {
            try
            {
                _logic.Delete(pocos);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
