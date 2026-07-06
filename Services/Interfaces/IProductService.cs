using BasicAuth.Model.ProductDto;
using BasicAuth.Model.UserDto;
using BasicAuth.Models;

namespace BasicAuth.Services.Interfaces;

public interface IProductService
{
    Task<ApiResponse<GetProductDto>> CreateProductAsync(CreateProductDto product, AuthData userData);

    Task<ApiResponse<GetProductDto>> GetProductByUserIdAndIdAsync(string userId, string id);
}