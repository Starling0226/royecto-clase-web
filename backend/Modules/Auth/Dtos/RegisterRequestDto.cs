
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Modules.Auth.Dtos;

public class RegisterRequestDto {

  [Required(ErrorMessage = "El nombre es requerido")]
  [MinLength(3, ErrorMessage = "El nombre debe tener al menos 3 caracteres")]
  [MaxLength(100, ErrorMessage = "El nombre no debe superar los 100 caracteres")]
  public required string Name { get; set; }

  [Required(ErrorMessage = "El email es requerido")]
  [EmailAddress(ErrorMessage = "Debe proporcionar un email válido")]
  public required string Email { get; set; }

  [Required(ErrorMessage = "La contraseña es requerido")]
  [MinLength(8, ErrorMessage = "La contraseña debe tener mínimo 8 caracteres")]
  public required string Password { get; set; }

  [Required(ErrorMessage = "Debes confirmar la contraseña")]
  [MinLength(8, ErrorMessage = "La contraseña debe tener mínimo 8 caracteres")]
  public required string ConfirmPassword { get; set; }
}