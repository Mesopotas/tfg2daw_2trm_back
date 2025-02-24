using Microsoft.AspNetCore.Mvc;
using Models;
using CoWorking.Service;

namespace BankApp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDto login)
    {
        try
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var token = await _authService.Login(login);
            return Ok(token);
        }
        catch (KeyNotFoundException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest("Error generating the token: " + ex.Message);
        }
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterDTO register)
    {
        try
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var token = await _authService.Register(register);
            return Ok(token);
        }
        catch (Exception ex)
        {
            return BadRequest("Error generating the token: " + ex.Message);
        }
    }
}