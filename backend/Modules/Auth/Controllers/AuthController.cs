
using BackEnd.Modules.Auth.Dtos;
using BackEnd.Modules.Auth.Services;
using Microsoft.AspNetCore.Mvc;
namespace BackEnd.Modules.Auth.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class AuthController(AuthService authService) : ControllerBase {
  private readonly AuthService _authService = authService;

  [HttpPost("login")]
  public IActionResult Login(LoginRequestDto loginRequestDto) {
    // Aquí hacemos login
    return Ok(loginRequestDto);
  }

  [HttpPost("register")]
  public IActionResult Register(RegisterRequestDto registerRequestDto) {
    // Aquí creamos un nuevo usuario
    return Ok(registerRequestDto);
  }
}