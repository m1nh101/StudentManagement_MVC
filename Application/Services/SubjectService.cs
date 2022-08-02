using Application.DTOs.Subjects;
using Application.Entities;
using Application.Interfaces;

namespace Application.Services;

public interface ISubjectService
{
  Task CreateNew(CreateSubject request);
  Task<IEnumerable<SubjectResponse>> GetAll();
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

  public Task<IEnumerable<SubjectResponse>> GetAll()
  {
    var query = _context.Subjects.Select(e => new SubjectResponse
    {
      Id = e.Id,
      Name = e.Name
    }).AsEnumerable();

    return Task.FromResult(query);
  }
}