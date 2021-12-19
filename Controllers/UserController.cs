using Charity_Calculator_Challange.Interfaces;
using Charity_Calculator_Challange.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Charity_Calculator_Challange.Controllers
{
 
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Authenticated()
        {
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Role(string email)
        {
            return Ok(await _userService.GetUserRole(email));
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string token = await _userService.Authenticate(model.Email, model.Password);
                    if (!string.IsNullOrEmpty(token))
                    {
                        return Ok(token);
                    }

                    return Unauthorized();
                }
                catch (Exception)
                {
                    //TODO:Log exception
                    return StatusCode(500);
                }
            }
            else
            {
                return BadRequest();
            }
            
        }

    }
}
