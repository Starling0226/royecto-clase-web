using BackEnd.Modules.Auth.Dtos;
using BackEnd.Modules.Auth.Services;
using Microsoft.AspNetCore.Mvc;
namespace BackEnd.Modules.Auth.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class AuthController(AuthService authService) : ControllerBase {
  private readonly AuthService _authService = authService;

  [HttpPost("login")]
  public async Task<IActionResult> Login(LoginRequestDto loginRequestDto) {
    var response = await _authService.LoginAsync(loginRequestDto);
    if (response is null) {
      return Unauthorized(new { message = "Credenciales inválidas" });
    }

    return Ok(response);
  }

  [HttpPost("register")]
  public async Task<IActionResult> Register(RegisterRequestDto registerRequestDto) {
    try {
      var response = await _authService.RegisterAsync(registerRequestDto);
      return Ok(response);
    } catch (InvalidOperationException ex) {
      return BadRequest(new { message = ex.Message });
    }
  }
}
