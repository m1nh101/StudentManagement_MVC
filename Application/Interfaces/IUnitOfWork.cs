using Application.Services;

namespace Application.Interfaces;

public interface IUnitOfWork
{
  public IClassService ClassService { get; }
  public ISubjectService SubjectService { get; }
  public IStudentService StudentService { get; }
}

public interface IService<T> where T : class
{
  
}