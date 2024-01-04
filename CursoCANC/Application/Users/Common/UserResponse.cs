namespace Application.Users.Common;

public record UserResponse(
    Guid Id,
    string Username,
    string Email,
    string Password,
    bool Active
);