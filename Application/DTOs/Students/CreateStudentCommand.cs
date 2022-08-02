using System.ComponentModel.DataAnnotations;
using Application.Enums;

namespace Application.DTOs.Students;

public class CreateStudentCommand
{
    [Required, Display(Name = "Họ và Tên")]
    public string Name { get; set; } = string.Empty;
    [Required, Display(Name = "Giới tính")]
    public Gender Gender { get; set; }
    [Required, Display(Name = "Địa chỉ")]
    public string Address { get; set; } = string.Empty;
    [Required, Display(Name = "Ngày sinh")]
    public DateTime Birthday { get; set; } = DateTime.UtcNow;
}