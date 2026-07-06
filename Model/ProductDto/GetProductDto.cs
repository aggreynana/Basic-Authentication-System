namespace BasicAuth.Model.ProductDto;


public class GetProductDto
{
    public string Id { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public int MyProperty { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quality { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }
}