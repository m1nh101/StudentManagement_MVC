using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Classes;

public class CreateClass
{
   [Display(Name = "Tên lớp"), Required(ErrorMessage = "Phải nhập tên lớp")]
   public string Name { get; set; } = string.Empty;
   [Display(Name = "Lớp trưởng"), Required(ErrorMessage = "Lớp phải có lớp trưởng")]
   public string Monitor { get; set; } = string.Empty;
   [Display(Name = "Bí thư"), Required(ErrorMessage = "Lớp phải có bí thư")]
   public string Secretary { get; set; } = string.Empty;
   [Display(Name = "Học sinh"), Required(ErrorMessage = "Lớp phải có tối thiểu 1 sinh viên")]
   public string[] Students { get; set; } = Array.Empty<string>();
   [Display(Name = "Môn học"), Required(ErrorMessage = "Lớp phải có tối thiểu 1 môn học")]
   public string[] Subjects { get; set; } = Array.Empty<string>();
}

public class EditClass : CreateClass
{
   public string Id { get; set; } = string.Empty;
}