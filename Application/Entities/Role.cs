namespace Application.Entities;

public class Role
{
    public Role()
    {
        Id = Guid.NewGuid().ToString();
    }
    public string Id { get; }
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<ClassRole>? ClassRoles { get; set; }
}