namespace Application.Entities;

public class Class
{
  public Class(string name)
  {
    Id = Guid.NewGuid().ToString();
    Name = name;
  }
  
  public string Id { get; }
  public string Name { get; private set; }
  public virtual ICollection<ClassStudentSubject>? ClassStudentSubjects { get; set; }
  public virtual ICollection<ClassRole>? ClassRoles { get; set; }
}