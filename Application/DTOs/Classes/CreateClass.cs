using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Classes;

public class CreateClass
{
   [Display(Name = "Tên lớp")]
   public string Name { get; set; } = string.Empty;
   [Display(Name = "Lớp trưởng")]
   public string Monitor { get; set; } = string.Empty;
   [Display(Name = "Bí thư")]
   public string Secretary { get; set; } = string.Empty;
   [Display(Name = "Học sinh")]
   public string[] Students { get; set; } = Array.Empty<string>();
   [Display(Name = "Môn học")]
   public string[] Subjects { get; set; } = Array.Empty<string>();
}