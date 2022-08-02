using Application.DTOs.Students;
using Application.Entities;
using Application.Enums;
using Application.Interfaces;

namespace Application.Services;

public interface IStudentService
{
    Task CreateNew(CreateStudentCommand command);
    Task<IEnumerable<StudentsResponse>> GetAll();
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
            Address = e.Address
        }).AsEnumerable();

        return Task.FromResult(query);
    }
}