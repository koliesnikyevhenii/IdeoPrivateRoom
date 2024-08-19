namespace IdeoPrivateRoom.WebApi.Models.Requests;

public record UserRequest(
    string Email,
    string FirstName,
    string LastName,
    DateTime UpdatedDate,
    DateTime CreatedDate,
    string PasswordHash,
    bool IsEmailConfirmed);
