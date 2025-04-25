namespace Blog.Domain.Entity;

public class Category : RootEntity<int>
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public int? ParentCategoryId { get; private set; }
    public Category? ParentCategory { get; private set; }
    public List<Category> ChildCategories { get; private set; }
    public bool IsActive { get; private set; } = false;

    private Category() { }

    public Category(string name, string description, int? parentCategoryId)
    {
        Title = string.IsNullOrEmpty(name) ? throw new ArgumentException("Invalid input") : name;
        Description = string.IsNullOrEmpty(description) ? throw new ArgumentException("Invalid input") : description;
        ParentCategoryId = parentCategoryId;
    }

    public void DeActive()
    {
        IsActive = false;
    }

    public void Active()
    {
        IsActive = true;
    }

    public void Edit(string title, string description)
    {
        Title = title;
        Description = description;
    }
}
