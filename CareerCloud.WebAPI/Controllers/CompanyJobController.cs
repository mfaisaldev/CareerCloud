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
    public class CompanyJobController : ApiController
    {
        private CompanyJobLogic _logic;

        public CompanyJobController()
        {
            var repo = new EntityFrameworkDataAccess.EFGenericRepository<CompanyJobPoco>();
            _logic = new CompanyJobLogic(repo);
        }

        [HttpGet]
        [Route("Job/{Id}")]
        [ResponseType(typeof(CompanyJobPoco))]
        public IHttpActionResult GetCompanyJob(Guid Id)
        {
			if (Id == null) return BadRequest();
			try
			{
				CompanyJobPoco poco = _logic.Get(Id);

				if (poco == null) return NotFound();

				return Ok(poco);
			}
			catch (Exception e)
			{
				return InternalServerError(e);
			}
		}
        [HttpGet]
        [Route("Job")]
        [ResponseType(typeof(List<CompanyJobPoco>))]
        public IHttpActionResult GetAllCompanyJob()
        {
            try
            {
                List<CompanyJobPoco> pocos = _logic.GetAll();
                if (pocos == null) return NotFound();
                return Ok(pocos);
            }
            catch(Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        [Route("Job")]
        public IHttpActionResult PostCompanyJob([FromBody]CompanyJobPoco[] pocos)
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
        [Route("Job")]
        public IHttpActionResult PutCompanyJob([FromBody]CompanyJobPoco[] pocos)
        {
            try
            {
                _logic.Update(pocos);
                return Ok();
            }catch(Exception e)
            {
                return InternalServerError(e);
            }
        }
        [HttpDelete]
        [Route("Job")]
        public IHttpActionResult DeleteCompanyJob([FromBody]CompanyJobPoco[] pocos)
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
