using BasicAuth.Context;
using BasicAuth.Entities;
using BasicAuth.Storage.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BasicAuth.Storage.Repository.Providers;

public class ProductEntityRepository : IProductEntityRepository
{
    private readonly ApplicationDbContext _context;

    public ProductEntityRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<bool> AddProductAsync(ProductEntity product)
    {
        await _context.AddAsync(product);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<ProductEntity?> GetProductByUserIdAndIdAsync(string userId, string id)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.UserId == userId && p.Id == id);
    }
}