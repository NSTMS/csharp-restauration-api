using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using restAPI.Models;
using restAPI.Services;
using Microsoft.AspNetCore.Components.Forms;
using restAPI.Exeptions;

namespace restAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccoutController : Controller
    {
        private readonly IAccountService _accountService;
        public AccoutController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok("pomyślnie dodano");

        }
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            try
            {
                string token = _accountService.GenerateJwt(dto);
                return Ok(token);
            }
            catch(BadRequestException ex)
            { 
                return BadRequest(ex.Message);
                
            }

        }

    }
}
