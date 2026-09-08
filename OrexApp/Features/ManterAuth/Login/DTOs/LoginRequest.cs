using System.ComponentModel.DataAnnotations;

namespace OrexApp.Features.ManterAuth.Login.DTOs.LoginRequest;

public record LoginRequest(
    [Required]
    [EmailAddress]
    string Email,

    [Required]
    string Password);