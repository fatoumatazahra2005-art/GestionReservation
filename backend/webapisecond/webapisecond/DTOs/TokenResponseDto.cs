namespace webapisecond.DTOs
{
    public record TokenResponseDto(
        string Token,
        DateTime Expiration,
        string Email,
        string Role
    );
}