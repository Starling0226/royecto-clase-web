using BackEnd.Modules.Auth.Repositories;
using BackEnd.Modules.Auth.Services;

namespace BackEnd.Modules.Auth;

public static class AddModule {
  public static IServiceCollection AddAuthModule(this IServiceCollection services) {

    services.AddScoped<AuthService>();
    services.AddScoped<AuthRepository>();

    return services;
  }
}