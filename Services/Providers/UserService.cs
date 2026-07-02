using System.Text;
using BasicAuth.Entities;
using BasicAuth.Model.UserDto;
using BasicAuth.Models;
using BasicAuth.Services.Interfaces;
using BasicAuth.Storage.Repository.UserRepository.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BasicAuth.Services.Providers;

public class UserService : IUserService
{

    private readonly IUserEntityRepository _userRepository;

    public UserService(IUserEntityRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<ApiResponse<AuthResponseDto>> CreateUserAsync(CreateUserRequestDto userDto)
    {
        var user = new UserEntity()
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName
        };

        var isUserSaved = await _userRepository.AddUserAsync(user);

        if (!isUserSaved)
        {
            return ApiResponse<AuthResponseDto>.FailedDependency();
        }


        var basicAuth = $"{user.Id}:Password";

        var encodeAuth = Encoding.UTF8.GetBytes(basicAuth);

        var base64String = Convert.ToBase64String(encodeAuth);


        var response = new AuthResponseDto()
        {
            Base64EncodedString = base64String
        };

        return ApiResponse<AuthResponseDto>.OkResponse("User created", response);
    }


}