using Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Extensions;

public static class ClassExtension
{
  public static async Task<string> FindClassHaveHighestMale(this DbSet<Class> source)
  {
    var _class = await source
      .Include(e => e.ClassStudentSubjects)!
      .ThenInclude(e => e.Student)
      .Select(e => new {
        e.Name,
        Number = e.ClassStudentSubjects!.Count(e => e.Student!.Gender == Enums.Gender.Male)
      }).OrderByDescending(e => e.Number).FirstAsync();

      return _class.Name;
  }
}