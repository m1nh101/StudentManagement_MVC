using Application.DTOs.Subjects;
using Application.Entities;
using Application.Interfaces;

namespace Application.Services;

public interface ISubjectService
{
  Task CreateNew(CreateSubject request);
  Task<IEnumerable<SubjectResponse>> GetAll();
  Task<bool> Update(EditSubject request);
  Task<bool> Remove(string id);
  Task<SubjectResponse?> GetDetail(string id);
}

public class SubjectService : ISubjectService
{
  private readonly IApplicationContext _context;

  public SubjectService(IApplicationContext context)
  {
    _context = context;
  }

  public async Task CreateNew(CreateSubject request)
  {
    var subject = new Subject() { Name = request.Name };

    await _context.Subjects.AddAsync(subject);
    await _context.Save();
  }

  public async Task<SubjectResponse?> GetDetail(string id)
  {
    var subject = await _context.Subjects.FindAsync(id);

    if (subject == null)
      return null;

    return new SubjectResponse
    {
      Id = subject.Id,
      Name = subject.Name
    };
  }

  public Task<IEnumerable<SubjectResponse>> GetAll()
  {
    var query = _context.Subjects.Select(e => new SubjectResponse
    {
      Id = e.Id,
      Name = e.Name
    }).AsEnumerable();

    return Task.FromResult(query);
  }

  public async Task<bool> Update(EditSubject request)
  {
    var subject = await _context.Subjects.FindAsync(request.Id);

    if (subject == null)
      return false;

    subject.Name = request.Name;
    _context.Subjects.Update(subject);
    await _context.Save();

    return true;
  }

  public async Task<bool> Remove(string id)
  {
    var subject = await _context.Subjects.FindAsync(id);

    if (subject == null)
      return false;

    _context.Subjects.Remove(subject);
    await _context.Save();

    return true;
  }
}