using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Subjects;

public class CreateSubject
{
  [Display(Name = "Tên môn học")]
  public string Name { get; set; } = string.Empty;
}

public class EditSubject : CreateSubject
{
  public string Id { get; set;} = string.Empty;
}