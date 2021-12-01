using GlobalProject.Business;
using GlobalProject.Domain.Entities;
using GlobalProject.Domain.Model;
using GlobalProject.Repository.Repositories;
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
    [Route("api/authenticate")]
    public class AuthenticateController : ControllerBase
    {
        private readonly AuthenticateService _authService;

        public AuthenticateController(AuthenticateService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public ActionResult<List<UserModel>> Get() =>
            _authService.Get();

        [HttpGet("{id:length(24)}", Name = "Anjali")]
        public ActionResult<UserModel> Get(String id)
        {
            var auth = _authService.Get(id);

            if (auth == null)
            {
                return NotFound();
            }

            return auth;
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
        public async Task<IActionResult> ManagerView()
        {
            LoginLandingModel model = await _authService.GetLoginLandingViewDetails();

            return Ok(model);
        }

    }
}
