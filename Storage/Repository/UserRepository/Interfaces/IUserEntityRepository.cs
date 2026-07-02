using BasicAuth.Entities;

namespace BasicAuth.Storage.Repository.UserRepository.Interfaces;

public interface IUserEntityRepository
{
    Task<bool> AddUserAsync(UserEntity userDto);

    Task<UserEntity?> GetUserByIdAsync(string id);
}