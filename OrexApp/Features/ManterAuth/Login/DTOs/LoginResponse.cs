namespace OrexApp.Features.ManterAuth.Login.DTOs.LoginResponse;
public record LoginResponse(
    string Token,
    string UserId,
    string Nome,
    string Email,
    IList<string> Roles);