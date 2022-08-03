using Application.Entities;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Application.Extensions;

public static class ClassExtension
{
  public static async Task<string> FindClassHaveHighestMale(this DbSet<Class> source)
  {
    var _class = await source
      .Include(e => e.Students)!
      .Select(e => new {
        e.Name,
        Number = e.Students!.Count(e => e.Gender == Enums.Gender.Male)
      }).OrderByDescending(e => e.Number).FirstAsync();

      return _class.Name;
  }

  public static async Task<string> ExportToJson(this IClassService service)
  {
    var classes = await service.GetAll();
    return JsonConvert.SerializeObject(classes);
  }
}