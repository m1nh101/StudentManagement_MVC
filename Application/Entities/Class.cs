namespace Application.Entities;

public class Class
{
  private readonly List<Subject> _subjects = new();
  private readonly List<Student> _students = new();

  public Class(string name)
  {
    Id = Guid.NewGuid().ToString();
    Name = name;
  }
  
  public string Id { get; }
  public string Name { get; private set; }

  public IReadOnlyCollection<Subject> Subjects => _subjects.AsReadOnly();
  public IReadOnlyCollection<Student> Students => _students.AsReadOnly();
  public virtual ICollection<ClassSubject>? ClassSubjects { get; private set; }
  public virtual ICollection<ClassStudent>? ClassStudents { get; private set; }
  public virtual ICollection<ClassRole>? ClassRoles { get; set; }

  public void AddStudent(Student student)
  {
    var checkClassHasStudent = _students.Any(e => e.Id == student.Id);

    if (checkClassHasStudent)
      return;
    
    _students.Add(student);
  }

  public void AddStudent(IEnumerable<Student> students)
  {
    foreach(var student in students)
    {
      var checkClassHasStudent = _students.Any(e => e.Id == student.Id);

      if (checkClassHasStudent)
        continue;

      _students.Add(student);
    }
  }

  public void AddSubject(Subject subject)
  {
    var checkClassHasSubject = _subjects.Any(e => e.Id == subject.Id);
    if (checkClassHasSubject)
      return;
    _subjects.Add(subject);
  }

  public void AddSubject(IEnumerable<Subject> subjects)
  {
    foreach(var subject in subjects)
    {
      var checkClassHasSubject = _subjects.Any(e => e.Id == subject.Id);

      if (checkClassHasSubject)
        continue;

      _subjects.Add(subject);
    }
  }
}