namespace BasicAuth.Model.UserDto;


public class GetUserResponseDto
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
    
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}