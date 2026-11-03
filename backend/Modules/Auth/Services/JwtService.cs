using System.Buffers.Text;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using BackEnd.Modules.Auth.Entities;

namespace BackEnd.Modules.Auth.Services;

public class JwtService(IConfiguration configuration) {
  private readonly IConfiguration _configuration = configuration;

  public string GenerateToken(User user) {
    var key = _configuration["Jwt:Key"] ?? "SuperSecretKey_BookCaseApp_2026_SecureJwtTokenKey!99";
    var issuer = _configuration["Jwt:Issuer"] ?? "BackEnd";
    var audience = _configuration["Jwt:Audience"] ?? "FrontEnd";
    var expireMinutes = int.TryParse(_configuration["Jwt:ExpireMinutes"], out var minutes) ? minutes : 60;

    var now = DateTimeOffset.UtcNow;
    var header = new { alg = "HS256", typ = "JWT" };
    var payload = new {
      sub = user.Uuid.ToString(),
      email = user.Email,
      name = user.Name,
      jti = Guid.NewGuid().ToString(),
      iat = now.ToUnixTimeSeconds(),
      exp = now.AddMinutes(expireMinutes).ToUnixTimeSeconds(),
      iss = issuer,
      aud = audience
    };

    var headerEncoded = Base64Url.EncodeToString(JsonSerializer.SerializeToUtf8Bytes(header));
    var payloadEncoded = Base64Url.EncodeToString(JsonSerializer.SerializeToUtf8Bytes(payload));
    var unsignedToken = $"{headerEncoded}.{payloadEncoded}";

    var keyBytes = Encoding.UTF8.GetBytes(key);
    var signatureBytes = HMACSHA256.HashData(keyBytes, Encoding.UTF8.GetBytes(unsignedToken));
    var signatureEncoded = Base64Url.EncodeToString(signatureBytes);

    return $"{unsignedToken}.{signatureEncoded}";
  }
}
