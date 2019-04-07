using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
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
    public class ApplicantWorkHistoryController : ApiController
    {
        private ApplicantWorkHistoryLogic _logic;

        public ApplicantWorkHistoryController()
        {
            var repo = new EntityFrameworkDataAccess.EFGenericRepository<ApplicantWorkHistoryPoco>();
            _logic = new ApplicantWorkHistoryLogic(repo);
        }

        [HttpGet]
        [Route("WorkHistory/{Id}")]
        [ResponseType(typeof(ApplicantSkillPoco))]
        public IHttpActionResult GetApplicantWorkHistory(Guid Id)
        {
            if (Id == null) return BadRequest();
            try
            {
                ApplicantWorkHistoryPoco poco = _logic.Get(Id);

                if (poco == null) return NotFound();

                return Ok(poco);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

        }

        [HttpGet]
        [Route("WorkHistory")]
        [ResponseType(typeof(List<ApplicantWorkHistoryPoco>))]
        public IHttpActionResult GetApplicantWorkHistory()
        {
            try
            {
                List<ApplicantWorkHistoryPoco> pocos = _logic.GetAll();
                if (pocos == null) return NotFound();
                return Ok(pocos);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        [Route("WorkHistory")]
        public IHttpActionResult PostApplicantWorkHistory([FromBody]ApplicantWorkHistoryPoco[] pocos)
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
        [Route("WorkHistory")]
        public IHttpActionResult PutApplicantWorkHistory([FromBody]ApplicantWorkHistoryPoco[] pocos)
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
        [Route("WorkHistory")]
        public IHttpActionResult DeleteApplicantWorkHistory([FromBody]ApplicantWorkHistoryPoco[] pocos)
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
