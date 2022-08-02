using Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IApplicationContext
{
    DbSet<Student> Students { get; }
    DbSet<Subject> Subjects { get; }
    DbSet<Class> Classes { get; }
    DbSet<Role> Roles { get; }
    Task Save();
}