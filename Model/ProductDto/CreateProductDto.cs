using System.ComponentModel.DataAnnotations;

namespace BasicAuth.Model.ProductDto;

public class CreateProductDto
{
    [Required(ErrorMessage = "this field is required")]
    public string Name { get; set; } = string.Empty;


    [Required(ErrorMessage = "this field is required")]
    public decimal UnitPrice { get; set; }


    [Required(ErrorMessage = "this field is required")]
    public int Quality { get; set; }
}