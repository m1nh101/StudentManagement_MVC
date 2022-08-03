using Application.DTOs.Students;
using Application.Services;
using Newtonsoft.Json;

namespace Application.Extensions;

public static class StudentExtension
{
  public static async Task<StatisticStudent> Statistic(this IStudentService service)
  {
    var students = await service.GetAll();
    var ordered = students.OrderBy(e => e.Birthday);

    return new StatisticStudent
    {
      YoungestStudent = students.First(),
      OldestStudent = students.Last()
    };
  }
  
  public static async Task<string> ExportToJson(this IStudentService service)
  {
    var students = await service.GetAll();
    return JsonConvert.SerializeObject(students);
  }
}