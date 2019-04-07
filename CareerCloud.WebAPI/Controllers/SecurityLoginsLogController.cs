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
    [RoutePrefix("api/careercloud/security/v1")]
    public class SecurityLoginsLogController : ApiController
    {
        private SecurityLoginsLogLogic _logic;

        public SecurityLoginsLogController()
        {
            var repo = new EntityFrameworkDataAccess.EFGenericRepository<SecurityLoginsLogPoco>();
            _logic = new SecurityLoginsLogLogic(repo);
        }

        [HttpGet]
        [Route("LoginsLog/{Id}")]
        [ResponseType(typeof(SecurityLoginsLogPoco))]
        public IHttpActionResult GetSecurityLoginLog(Guid Id)
        {
			if (Id == null) return BadRequest();
			try
			{
				SecurityLoginsLogPoco poco = _logic.Get(Id);

				if (poco == null) return NotFound();

				return Ok(poco);
			}
			catch (Exception e)
			{
				return InternalServerError(e);
			}
		}
        [HttpGet]
        [Route("LoginsLog")]
        [ResponseType(typeof(List<SecurityLoginsLogPoco>))]
        public IHttpActionResult GetAllSecurityLoginLog()
        {
            try
            {
                List<SecurityLoginsLogPoco> pocos = _logic.GetAll();
                if (pocos == null) return NotFound();
                return Ok(pocos);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        [Route("LoginsLog")]
        public IHttpActionResult PostSecurityLoginLog([FromBody]SecurityLoginsLogPoco[] pocos)
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
        [Route("LoginsLog")]
        public IHttpActionResult PutSecurityLoginLog([FromBody]SecurityLoginsLogPoco[] pocos)
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
        [Route("LoginsLog")]
        public IHttpActionResult DeleteSecurityLoginLog([FromBody]SecurityLoginsLogPoco[] pocos)
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
