using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellDoc.SampleTask.BAL.Ports;
using WellDoc.SampleTask.Model;

namespace WellDoc.SampleTask.API.Controllers
{    
    [Route("api/[controller]")]    
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;
        private readonly IUserValidator _userValidator;
        public UsersController(IUserService userService, ILogger<UsersController> logger, IUserValidator userValidator)
        {
            _userService = userService;
            _logger = logger;
            _userValidator = userValidator;
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllUsers")]        
        public async Task<IActionResult> GetAllUsers(int id)
        {
            var list = await _userService.GetUsers(id);
            return Ok(list);
        }

        [Authorize]
        [HttpGet]
        [Route("GetUserDetailById/{id}")] 
        public async Task<IActionResult> GetUserDetailById(int id)
        {
            var strValidate = _userValidator.GetUsersValidator(id);
            if (!string.IsNullOrEmpty(strValidate))
            {
                return Ok(new ReturnObject<string>()
                {
                    code = Convert.ToString("C301"),
                    isStatus = false,
                    message = strValidate
                });
            }
            else
            {
                return Ok(await _userService.GetUsers(id));
            }
        }

        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> SaveUser([FromBody] UsersModel model)
        {
            var strValidate = _userValidator.SaveUserValidator(model);
            if (!string.IsNullOrEmpty(strValidate))
            {
                return Ok(new ReturnObject<string>()
                {
                    code = Convert.ToString("C301"),
                    isStatus = false,
                    message = strValidate
                });
            }
            else
            {
                var result = await _userService.SaveUser(model);
                return Ok(result);
            }            
        }

        [Authorize]
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateUser([FromBody] UsersModel model)
        {
            var strValidate = _userValidator.UpdateUserValidator(model);
            if (!string.IsNullOrEmpty(strValidate))
            {
                return Ok(new ReturnObject<string>()
                {
                    code = Convert.ToString("C301"),
                    isStatus = false,
                    message = strValidate
                });
            }
            else
            {
                var result = await _userService.UpdateUser(model);
                return Ok(result);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var strValidate = _userValidator.DeleteUserValidator(id);
            if (!string.IsNullOrEmpty(strValidate))
            {
                return Ok(new ReturnObject<string>()
                {
                    code = Convert.ToString("C301"),
                    isStatus = false,
                    message = strValidate
                });
            }
            else
            {
                var result = await _userService.DeleteUser(id);
                return Ok(result);
            }
        }

    }
}
