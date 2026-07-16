using System.Text;
using BasicAuth.Entities;
using BasicAuth.Model.UserDto;
using BasicAuth.Models;
using BasicAuth.Services.Interfaces;
using BasicAuth.Storage.Repository.Interfaces;

namespace BasicAuth.Services.Providers;

public class UserService : IUserService
{

    private readonly IUserEntityRepository _userRepository;
    private readonly ITokenService _tokenService;

    public UserService(IUserEntityRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
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

        var token = await _tokenService.GenerateToken(user);


        var response = new AuthResponseDto()
        {
            Token = token
        };

        return ApiResponse<AuthResponseDto>.OkResponse("User created", response);
    }
}