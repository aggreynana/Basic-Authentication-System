using System.ComponentModel.DataAnnotations;

namespace BasicAuth.Model.UserDto;

public class CreateUserRequestDto
{
    [Required(ErrorMessage = "This field is required")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "This field is require")]
    public string LastName { get; set; } = string.Empty;

    
}