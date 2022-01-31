using Auth.Business.Abstract;
using Auth.DAL.Dto.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auth.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var result = await _authService.GetRoles();
                if (result == null) return Ok();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole(string role)
        {
            try
            {
                var list = new Lazy<List<string>>();
                var result = await _authService.AddRole(role);
                if (result != Guid.Empty)
                {
                    list.Value.Add("Adding is successful.");
                    return Ok(new { code = StatusCode(1000), message = list.Value, guid = result, type = "success" });
                }
                list.Value.Add("Adding is failed.");
                return Ok(new { code = StatusCode(1001), message = list.Value, type = "error" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var result = await _authService.GetUsers();
                if (result == null) return Ok();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            try
            {
                var list = new Lazy<List<string>>();

                var registerResult = await _authService.Register(userRegisterDto);
                if (registerResult > 0)
                {
                    list.Value.Add("Registration is successful.");
                    return Ok(new { code = StatusCode(1000), message = list.Value, type = "success" });
                }
                list.Value.Add("Registration is failed.");
                return Ok(new { code = StatusCode(1001), message = list.Value, type = "error" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            try
            {
                var list = new Lazy<List<string>>();
                var currentUser = await _authService.GetLoginUser(userLoginDto);
                if (currentUser == null)
                {
                    list.Value.Add("User not found.");
                    return Ok(new { code = StatusCode(1001), message = list.Value, type = "error" });
                }
                else if (currentUser.Name == null)
                {
                    list.Value.Add("Username or password is incorrect");
                    return Ok(new { code = StatusCode(1001), message = list.Value, type = "error" });
                }
                var accessToken = await _authService.CreateAccessToken(currentUser);
                return Ok(accessToken);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
