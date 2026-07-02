using BasicAuth.Model.UserDto;
using BasicAuth.Models;
using BasicAuth.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BasicAuth.Controllers;


[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }


    [Route("api/users")]

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDto userPayload)
    {
        var userReponse = await _userService.CreateUserAsync(userPayload);
        return StatusCode(Response.StatusCode, userReponse);
    }
}