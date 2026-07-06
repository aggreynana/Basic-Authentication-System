using BasicAuth.Entities;
using BasicAuth.Model.ProductDto;
using BasicAuth.Model.UserDto;
using BasicAuth.Models;
using BasicAuth.Services.Interfaces;
using BasicAuth.Storage.Repository.Interfaces;

namespace BasicAuth.Services.Providers;


public class ProductService : IProductService
{
    private readonly IProductEntityRepository _productRepository;

    public ProductService(IProductEntityRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<ApiResponse<GetProductDto>> CreateProductAsync(CreateProductDto product, AuthData userData)
    {
        try
        {
            var productEntity = new ProductEntity()
            {
                Name = product.Name,
                UserId = userData.Id,
                UnitPrice = product.UnitPrice,
                Quality = product.Quality
            };


            var isProductSaved = await _productRepository.AddProductAsync(productEntity);

            if (!isProductSaved)
            {
                return ApiResponse<GetProductDto>.FailedDependency();
            }

            var responseData = new GetProductDto()
            {
                Id = productEntity.Id,
                CreatedOn = productEntity.CreatedOn,
                ModifiedOn = productEntity.ModifiedOn,
                Name = productEntity.Name,
                Quality = productEntity.Quality,
                UnitPrice = productEntity.UnitPrice,
                UserId = productEntity.UserId
            };


            return ApiResponse<GetProductDto>.CreatedResponse("product", responseData);
        }
        catch(Exception)
        {
            return ApiResponse<GetProductDto>.InternalServerError();
        }
    }



    public async Task<ApiResponse<GetProductDto>> GetProductByUserIdAndIdAsync(string userId, string id)
    {
        try
        {
            var productEntity = await _productRepository.GetProductByUserIdAndIdAsync(userId, id);

            if (productEntity == null)
            {
                return ApiResponse<GetProductDto>.NotFound("Product not found");
            }


            var responseData = new GetProductDto()
            {
                Id = productEntity.Id,
                CreatedOn = productEntity.CreatedOn,
                ModifiedOn = productEntity.ModifiedOn,
                Name = productEntity.Name,
                Quality = productEntity.Quality,
                UnitPrice = productEntity.UnitPrice,
                UserId = productEntity.UserId
            };

            return ApiResponse<GetProductDto>.OkResponse("product successfully retrieved", responseData);
        }
        catch(Exception)
        {
            return ApiResponse<GetProductDto>.InternalServerError();
        }
    }
}

