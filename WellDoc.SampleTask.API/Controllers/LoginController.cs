using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellDoc.SampleTask.BAL.Implementation;
using WellDoc.SampleTask.BAL.Ports;
using WellDoc.SampleTask.Model;

namespace WellDoc.SampleTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private IConfiguration _configuration;
        private readonly IUserValidator _userValidator;

        public LoginController(IConfiguration configuration, IUserService userService, IUserValidator userValidator)
        {
            _configuration = configuration;
            _userService = userService;
            _userValidator = userValidator;
        }

        [HttpGet]
        [Route("GetToken/{email}/{password}")] 
        public ReturnObject<string> GetToken(string email, string password)
        {
            var strValidate = _userValidator.GetTokenValidator(email, password);
            if (!string.IsNullOrEmpty(strValidate))
            {
                return new ReturnObject<string>()
                {
                    code = Convert.ToString("C301"),
                    isStatus = false,
                    message = strValidate
                };
            }
            else
            {
                ReturnObject<string> response = new ReturnObject<string>();
                var isValidCredentials = ValidateCredentials(email, password).Result;
                if (isValidCredentials.isStatus)
                {
                    var jwt = new JwtService(_configuration);
                    response.data = jwt.GenerateSecurityToken(email);
                    response.isStatus = true;
                    response.code = Convert.ToString("C200");
                    response.message = "Token generated successfully";
                }
                else
                {
                    response.isStatus = false;
                    response.code = Convert.ToString("C201");
                    response.message = "Token generation is failed";
                }
                return response;
            }
        }
        private async Task<ReturnObject<string>> ValidateCredentials(string email, string password)
        {
            return await _userService.ValidateUserCredentials(email, password);
        }
    }
}
