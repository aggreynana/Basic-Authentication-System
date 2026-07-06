using BasicAuth.Entities;

namespace BasicAuth.Storage.Repository.Interfaces;

public interface IProductEntityRepository
{
    Task<bool> AddProductAsync(ProductEntity product);

    Task<ProductEntity?> GetProductByUserIdAndIdAsync(string userId, string id);
}