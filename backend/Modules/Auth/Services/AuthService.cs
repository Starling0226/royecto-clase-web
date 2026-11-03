using BackEnd.Modules.Auth.Dtos;
using BackEnd.Modules.Auth.Entities;
using BackEnd.Modules.Auth.Repositories;
using Microsoft.AspNetCore.Identity;

namespace BackEnd.Modules.Auth.Services;

public class AuthService(AuthRepository authRepository, JwtService jwtService) {
  private readonly AuthRepository _authRepository = authRepository;
  private readonly JwtService _jwtService = jwtService;
  private readonly PasswordHasher<User> _passwordHasher = new();

  public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginRequestDto) {
    var user = await _authRepository.GetByEmailAsync(loginRequestDto.Email);
    if (user is null) {
      return null;
    }

    var verificationResult = _passwordHasher.VerifyHashedPassword(
      user,
      user.Password,
      loginRequestDto.Password
    );

    if (verificationResult == PasswordVerificationResult.Failed) {
      return null;
    }

    var token = _jwtService.GenerateToken(user);

    return new LoginResponseDto {
      Token = token,
      Uuid = user.Uuid,
      Name = user.Name,
      Email = user.Email
    };
  }

  public async Task<LoginResponseDto> RegisterAsync(RegisterRequestDto registerRequestDto) {
    if (registerRequestDto.Password != registerRequestDto.ConfirmPassword) {
      throw new InvalidOperationException("Las contraseñas no coinciden");
    }

    var existingUser = await _authRepository.GetByEmailAsync(registerRequestDto.Email);
    if (existingUser is not null) {
      throw new InvalidOperationException("El correo electrónico ya está registrado");
    }

    var user = new User {
      Uuid = Guid.NewGuid(),
      Name = registerRequestDto.Name,
      Email = registerRequestDto.Email,
      Password = string.Empty
    };

    user.Password = _passwordHasher.HashPassword(user, registerRequestDto.Password);

    await _authRepository.CreateAsync(user);

    var token = _jwtService.GenerateToken(user);

    return new LoginResponseDto {
      Token = token,
      Uuid = user.Uuid,
      Name = user.Name,
      Email = user.Email
    };
  }
}
