using GlobalProject.Business;
using GlobalProject.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GlobalProject.Api.Controllers
{
    [ApiController]
    
    [Route("api/[controller]")]
    public class LoggedInController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoggedInController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        /// <summary>
        /// Get login landing page view
        /// </summary>
        /// <returns></returns>
        [HttpGet("manager")]
        [Authorize(Roles = "Admin")]
       
        [ProducesDefaultResponseType(typeof(LoginLandingModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [NonAction]
        public async Task<IActionResult> ManagerView()
        {
            LoginLandingModel model = await _loginService.GetLoginLandingViewDetails();

            return Ok(model);
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Register([FromBody] LoginRegisterModel request)
        {
            await _loginService.Register(request);

            return StatusCode(201);
        }

        /// <summary>
        /// update user details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update([FromBody] LoginUpdateModel request)
        {
            await _loginService.Update(request);

            return StatusCode(204);
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(string id)
        {
            await _loginService.Delete(id);

            return StatusCode(204);
        }
    }
}
