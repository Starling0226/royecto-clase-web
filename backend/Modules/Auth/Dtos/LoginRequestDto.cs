using System.ComponentModel.DataAnnotations;

namespace BackEnd.Modules.Auth.Dtos;

public class LoginRequestDto {

  [Required(ErrorMessage = "El email es requerido")]
  [EmailAddress(ErrorMessage = "Debe proporcionar un email válido")]
  public required string Email { get; set; }


  [Required(ErrorMessage = "La contraseña es requerido")]
  [MinLength(8, ErrorMessage = "La contraseña debe tener mínimo 8 caracteres")]
  public required string Password { get; set; }
}