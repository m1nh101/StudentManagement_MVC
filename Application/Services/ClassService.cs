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
  Task<bool> Delete(string id);
  Task<bool> Update(EditClass command);
  Task<string> FindClassHaveHighestMale();
  Task<ClassDetailResponse?> GetDetail(string id);
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

    if(command.Students.Length > 0)
    {
      foreach(var id in command.Students)
      {
        var student = await _context.Students.FindAsync(id);

        if (student == null)
          continue;
        
        _class.AddStudent(student);
      }
    } 

    if(command.Subjects.Length > 0)
    {
      foreach(var id in command.Subjects)
      {
        var subject = await _context.Subjects.FindAsync(id);

        if (subject == null)
          continue;
        
        _class.AddSubject(subject);
      }
    }
   
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
      .Include(e => e.Students)
      .Include(e => e.Subjects)
      .Include(e => e.ClassRoles)!
      .ThenInclude(e => e.Student)
      .Select(e => new ClassResponse
      {
        Name = e.Name,
        Id = e.Id,
        Glosbe = e.Students.Count,
        MaleGlosbe = e.Students.Count(e => e.Gender == Enums.Gender.Male),
        FemaleGlosbe = e.Students.Count(e => e.Gender == Enums.Gender.Female),
        Monitor = e.ClassRoles!.First(e => e.RoleId == monitor!.Id)!.Student!.Name,
        Secretary = e.ClassRoles!.First(e => e.RoleId == secretary!.Id)!.Student!.Name,
        Subjects = e.Subjects!.Select(e => e.Name).ToArray()
      }).AsEnumerable();

      var test = query.ToList();

    return query;

  }

  public async Task<ClassDetailResponse?> GetDetail(string id)
  {
    var monitor = await _context.Roles.FirstOrDefaultAsync(e => e.Name == "monitor");
    var secretary = await _context.Roles.FirstOrDefaultAsync(e => e.Name == "secretary");

    var result = await _context.Classes
      .Include(e => e.Students)
      .Include(e => e.Subjects)
      .Include(e => e.ClassRoles)!
      .ThenInclude(e => e.Student)
      .Select(e => new ClassDetailResponse{
        Name = e.Name,
        Id = e.Id,
        Monitor = e.ClassRoles!.First(e => e.RoleId == monitor!.Id)!.Student!.Name,
        Secretary = e.ClassRoles!.First(e => e.RoleId == secretary!.Id)!.Student!.Name,
        Subjects = e.Subjects!.Select(e => e.Name).ToArray(),
        Students = e.Students!.Select(e => e.Name).ToArray()
      }).FirstOrDefaultAsync(e => e.Id == id);

      return result;
  }

  public async Task<bool> Update(EditClass command)
  {

    var exClass = await _context.Classes.FindAsync(command.Id);

    if (exClass == null)
      return false;

    var createClass = new CreateClass()
    {
      Name = command.Name,
      Subjects = command.Subjects,
      Students = command.Students,
      Monitor = command.Monitor,
      Secretary = command.Secretary
    };

    var classCreated = await CreateNew(createClass);

    if (classCreated)
      _context.Classes.Remove(exClass);
      await _context.Save();

    return true;

  }

  public async Task<bool> Delete(string id)
  {
    var _class = await _context.Classes.FindAsync(id);

    if (_class == null)
      return false;

    _context.Classes.Remove(_class);
    await _context.Save();

    return true;
  }

  public Task<string> FindClassHaveHighestMale()
  {
    return _context.Classes.FindClassHaveHighestMale();
  }
}