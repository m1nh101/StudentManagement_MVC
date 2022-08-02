using Application.DTOs.Classes;
using Application.Entities;
using Application.Extensions;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public interface IClassService
{
  Task<IEnumerable<ClassResponse>> GetAll();
  Task<bool> CreateNew(CreateClass command);
  Task<string> FindClassHaveHighestMale();
}

public class ClassService : IClassService
{
  private readonly IApplicationContext _context;

  public ClassService(IApplicationContext context)
  {
    _context = context;
  }

  public async Task<bool> CreateNew(CreateClass command)
  {
    var _class = new Class(command.Name);
    var _classRole = new List<ClassRole>();
    var _classStudent = new List<ClassStudentSubject>();
    
    if (command.Monitor != string.Empty)
    {
      var monitor = await _context.Students.FindAsync(command.Monitor);

      if (monitor == null)
        return false;

      var role = await _context.Roles.FirstOrDefaultAsync(e => e.Name == "monitor");

      _classRole.Add(new ClassRole
      {
        RoleId = role!.Id,
        StudentId = command.Monitor
      });
    }

    if (command.Secretary != string.Empty)
    {
      var secretary = await _context.Students.FindAsync(command.Secretary);

      if (secretary == null)
        return false;

      var role = await _context.Roles.FirstOrDefaultAsync(e => e.Name == "secretary");

      _classRole.Add(new ClassRole
      {
        RoleId = role!.Id,
        StudentId = command.Secretary
      });
    }

    if (command.Students.Length > 0 || command.Subjects.Length > 0)
    {
      foreach (var subjectId in command.Subjects)
      {
        var subject = await _context.Subjects.FindAsync(subjectId);

        if (subject == null)
          continue;

        foreach (var id in command.Students)
        {
          var student = await _context.Students.FindAsync(id);

          if (student == null)
            continue;

          _classStudent.Add(new ClassStudentSubject
          {
            SubjectId = subjectId,
            StudentId = id
          });
        }
      }
    }

    _class.ClassStudentSubjects = _classStudent;
    _class.ClassRoles = _classRole;

    await _context.Classes.AddAsync(_class);
    await _context.Save();

    return true;
  }

  public async Task<IEnumerable<ClassResponse>> GetAll()
  {
    var monitor = await _context.Roles.FirstOrDefaultAsync(e => e.Name == "monitor");
    var secretary = await _context.Roles.FirstOrDefaultAsync(e => e.Name == "secretary");

    var query = _context.Classes
      .Include(e => e.ClassStudentSubjects)!
      .ThenInclude(e => e.Subject)
      .Include(e => e.ClassRoles)!
      .ThenInclude(e => e.Student)
      .Select(e => new ClassResponse{
        Id = e.Id,
        Name = e.Name,
        Monitor = e.ClassRoles!.First(e => e.RoleId == monitor!.Id).Student!.Name,
        Secretary = e.ClassRoles!.First(e => e.RoleId == secretary!.Id).Student!.Name,
        Glosbe = e.ClassStudentSubjects!.Select(e => e.StudentId).Distinct().Count(),
        MaleGlosbe = e.ClassStudentSubjects!.Select(e => new { e.Student!.Id, e.Student.Gender}).Distinct().Count(e => e.Gender == Enums.Gender.Male),
        FemaleGlosbe = e.ClassStudentSubjects!.Select(e => new { e.Student!.Id, e.Student.Gender}).Distinct().Count(e => e.Gender == Enums.Gender.Female),
        Subjects = e.ClassStudentSubjects!.Select(e => e.Subject!.Name).Distinct().ToArray()
      }).AsEnumerable();


    return query;

  }

  public Task<string> FindClassHaveHighestMale()
  {
    return _context.Classes.FindClassHaveHighestMale();
  }
}