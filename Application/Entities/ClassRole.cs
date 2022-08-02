namespace Application.Entities;

public class ClassRole
{
    public int Id { get; }
    public string RoleId { get; set; } = string.Empty;
    public string StudentId { get; set; } = string.Empty;
    public string ClassId { get; set; } = string.Empty;
    public virtual Student? Student { get; set; }
    public virtual Class? Class { get; set; }
    public virtual Role? Role { get; set; }
}