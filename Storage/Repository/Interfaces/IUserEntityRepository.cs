using BasicAuth.Entities;

namespace BasicAuth.Storage.Repository.Interfaces;

public interface IUserEntityRepository
{
    Task<bool> AddUserAsync(UserEntity userDto);

    Task<UserEntity?> GetUserByIdAsync(string id);
}