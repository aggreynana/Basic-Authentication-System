using BasicAuth.Extensions;
using BasicAuth.Model.ProductDto;
using BasicAuth.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicAuth.Controllers;

[ApiController]
[Route("api/products")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ProductController : ControllerBase
{

    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }


    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto payload)
    {
        var user = User.GetAuthData();
        var response = await _productService.CreateProductAsync(payload, user);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(string id)
    {
        var user = User.GetAuthData();
        var response = await _productService.GetProductByUserIdAndIdAsync(user.Id, id);
        return StatusCode(response.StatusCode, response);
    }
}