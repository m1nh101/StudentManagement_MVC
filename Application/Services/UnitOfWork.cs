using Application.Interfaces;

namespace Application.Services;

public interface IUnitOfWork
{
  public IClassService ClassService { get; }
  public ISubjectService SubjectService { get; }
  public IStudentService StudentService { get; }
  public IRoleService RoleService { get; }
}

public class UnitOfWork : IUnitOfWork
{
  private readonly IApplicationContext _context;
  public UnitOfWork(IApplicationContext context)
  {
    _context = context;
    ClassService = new ClassService(_context);
    SubjectService = new SubjectService(_context);
    StudentService = new StudentService(_context);
    RoleService = new RoleService(_context);
  }

  public IClassService ClassService { get; }
  public ISubjectService SubjectService { get; }
  public IStudentService StudentService { get; }
  public IRoleService RoleService { get; }
}