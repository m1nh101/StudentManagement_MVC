using Application.Enums;

namespace Application.Entities;

public class Student
{
  public Student()
  {
    Id = Guid.NewGuid().ToString();
  }
  
  public string Id { get; }
  public string Name { get; set; } = string.Empty;
  public Gender Gender { get; set; }
  public DateTime Birthday { get; set; }
  public string Address { get; set; } = string.Empty;

  public virtual ICollection<ClassStudentSubject>? ClassStudentSubjects { get; set; }
  public virtual ICollection<ClassRole>? ClassRoles { get; set; }

}