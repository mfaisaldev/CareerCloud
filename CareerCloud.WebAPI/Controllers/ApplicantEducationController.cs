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
    [RoutePrefix("api/careercloud/applicant/v1")]
    public class ApplicantEducationController : ApiController
    {
        private ApplicantEducationLogic _logic;

        public ApplicantEducationController()
        {
			// Accessing business logic layer!
            var repo = new EntityFrameworkDataAccess.EFGenericRepository<ApplicantEducationPoco>();
            _logic = new ApplicantEducationLogic(repo);
        }

        [HttpGet]
        [Route("Education/{Id}")]
        [ResponseType(typeof(ApplicantEducationPoco))]
        public IHttpActionResult GetApplicantEducation(Guid Id)
        {
            if (Id == null) return BadRequest();
            try
            {
                ApplicantEducationPoco poco = _logic.Get(Id);

                if (poco == null) return NotFound();

                return Ok(poco);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
           
        }

        [HttpGet]
        [Route("Education")]
        [ResponseType(typeof(List<ApplicantEducationPoco>))]
        public IHttpActionResult GetAllApplicantEducation()
        {
            try
            {
                List<ApplicantEducationPoco> pocos = _logic.GetAll();
                if (pocos == null) return NotFound();
                return Ok(pocos);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        [Route("Education")]
        public IHttpActionResult PostApplicantEducation([FromBody]ApplicantEducationPoco[] pocos)
        {
            try
            {
                _logic.Add(pocos);
                return Ok();
            }catch(Exception e)
            {
                return InternalServerError(e);
            }
        }
   

        [HttpPut]
        [Route("Education")]
        public IHttpActionResult PutApplicantEducation([FromBody]ApplicantEducationPoco[] pocos)
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
        [Route("Education")]
        public IHttpActionResult DeleteApplicantEducation([FromBody]ApplicantEducationPoco[] pocos)
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
