namespace Application.Entities;

public class ClassStudent
{
  public string ClassId { get; set; } = string.Empty;
  public string StudentId { get; set; } = string.Empty;

  public virtual Class? Class { get; private set; }
  public virtual Student? Student { get; private set; }
}