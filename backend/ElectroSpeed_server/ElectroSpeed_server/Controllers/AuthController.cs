using ElectroSpeed_server.Models.Data.Dto;
using ElectroSpeed_server.Models.Data.Services;
using Microsoft.AspNetCore.Mvc;


namespace ElectroSpeed_server.Controllers
{
    [Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService userService)
    {
        _authService = userService;
    }

    [HttpPost("/register")]
    public async Task<ActionResult> Register([FromBody] RegisterRequest model)
    {
        try
        {
            var newUser = await _authService.RegisterAsync(model);
            var token = await _authService.LoginAsync(new LoginRequest { Email = model.Email, Password = model.Password });

            return Ok(new
            {
                accessToken = token
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("/login")]
    public async Task<ActionResult> Login([FromBody] LoginRequest model)
    {
        try
        {
            var token = await _authService.LoginAsync(model);
            return Ok(new { accessToken = token });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Email o contraseña incorrectos");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}


}