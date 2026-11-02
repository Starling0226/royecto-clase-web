using BackEnd.Modules.Auth.Repositories;

namespace BackEnd.Modules.Auth.Services;

public class AuthService(AuthRepository authRepository) {
  private readonly AuthRepository _authRepository = authRepository;
}