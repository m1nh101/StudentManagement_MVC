using Application.Services;
using Newtonsoft.Json;

namespace Application.Extensions;

public static class SubjectExtension
{
  public static async Task<string> ExportToJson(this ISubjectService service)
  {
    var subjects = await service.GetAll();
    return JsonConvert.SerializeObject(subjects);
  }
}