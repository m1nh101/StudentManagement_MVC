namespace Application.Entities;

public class ClassStudentSubject
{
    public int Id { get; }
    public string StudentId { get; set; } = string.Empty;
    public string ClassId { get; set; } = string.Empty;
    public string SubjectId { get; set; } = string.Empty;
    public virtual Student? Student { get; set; }
    public virtual Class? Class { get; set; }
    public virtual Subject? Subject { get; set; }
}