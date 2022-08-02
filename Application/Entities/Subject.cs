namespace Application.Entities;

public class Subject
{
  public Subject()
  {
    Id = Guid.NewGuid().ToString();
  }
  
  public string Id { get; }
  public string Name { get; set; } = string.Empty;
  public string ShortName { get; set; } = string.Empty;
  public virtual ICollection<ClassStudentSubject>? ClassStudentSubjects { get; set; }
}
