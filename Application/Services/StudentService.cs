using Application.DTOs.Students;
using Application.Entities;
using Application.Enums;
using Application.Interfaces;

namespace Application.Services;

public interface IStudentService
{
  Task CreateNew(CreateStudentCommand command);
  Task<IEnumerable<StudentsResponse>> GetAll();
  Task<bool> Delete(string id);
  Task<StudentsResponse?> GetDetail(string id);
  Task<bool> Update(EditStudentCommand command);
  Task<IEnumerable<StudentsResponse>> Filter(string input);
}

public class StudentService : IStudentService
{
  private readonly IApplicationContext _context;

  public StudentService(IApplicationContext context)
  {
    _context = context;
  }

  public async Task CreateNew(CreateStudentCommand command)
  {
    var student = new Student
    {
      Name = command.Name,
      Address = command.Address,
      Gender = command.Gender,
      Birthday = command.Birthday
    };

    await _context.Students.AddAsync(student);
    await _context.Save();
  }

  public Task<IEnumerable<StudentsResponse>> GetAll()
  {
    var query = _context.Students.Select(e => new StudentsResponse
    {
      Name = e.Name,
      Id = e.Id,
      Gender = e.Gender == Gender.Male ? "Nam" : "Nữ",
      Address = e.Address,
      Birthday = e.Birthday
    }).AsEnumerable();

    return Task.FromResult(query);
  }

  public Task<IEnumerable<StudentsResponse>> Filter(string input)
  {
    var query = _context.Students
    .Where(e => e.Name.StartsWith(input))
    .Select(e => new StudentsResponse
    {
      Name = e.Name,
      Id = e.Id,
      Gender = e.Gender == Gender.Male ? "Nam" : "Nữ",
      Address = e.Address,
      Birthday = e.Birthday
    }).AsEnumerable();

    return Task.FromResult(query);
  }

  public async Task<StudentsResponse?> GetDetail(string id)
  {
    var student = await _context.Students.FindAsync(id);

    if (student == null)
      return null;

    return new StudentsResponse
    {
      Id = student.Id,
      Name = student.Name,
      Birthday = student.Birthday,
      Gender = student.Gender == Gender.Male ? "Nam" : "Nữ",
      Address = student.Address
    };
  }

  public async Task<bool> Update(EditStudentCommand command)
  {
    var student = await _context.Students.FindAsync(command.Id);
    if (student == null)
      return false;

    student.Name = command.Name;
    student.Birthday = command.Birthday;
    student.Address = command.Address;
    student.Gender = command.Gender;

    _context.Students.Update(student);
    await _context.Save();

    return true;
  }

  public async Task<bool> Delete(string id)
  {
    var student = await _context.Students.FindAsync(id);

    if (student == null)
      return false;

    _context.Students.Remove(student);
    await _context.Save();

    return true;
  }
}