namespace Application.Entities;

public class ClassSubject
{
  public string ClassId { get; set; } = string.Empty;
  public string SubjectId { get; set; } = string.Empty;

  public virtual Class? Class { get; private set; }
  public virtual Subject? Subject { get; private set; }
}