namespace Catalog.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public Category? Parent { get; set; }
}

