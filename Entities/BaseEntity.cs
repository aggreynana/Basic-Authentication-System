namespace BasicAuth.Entities;

public class BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedOn { get; set; }
}