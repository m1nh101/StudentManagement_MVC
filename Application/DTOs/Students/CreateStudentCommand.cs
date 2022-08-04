using System.ComponentModel.DataAnnotations;
using Application.Enums;

namespace Application.DTOs.Students;

public class CreateStudentCommand
{
    [Required(ErrorMessage = "Phải nhập tên"), Display(Name = "Họ và Tên")]
    [RegularExpression(@"^[A-ZÀÁẠẢÃÂẦẤẬẨẪĂẰẮẶẲẴÈÉẸẺẼÊỀẾỆỂỄÌÍỊỈĨÒÓỌỎÕÔỒỐỘỔỖƠỜỚỢỞỠÙÚỤỦŨƯỪỨỰỬỮỲÝỴỶỸĐ][a-zàáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđ]*(?:[ ][A-ZÀÁẠẢÃÂẦẤẬẨẪĂẰẮẶẲẴÈÉẸẺẼÊỀẾỆỂỄÌÍỊỈĨÒÓỌỎÕÔỒỐỘỔỖƠỜỚỢỞỠÙÚỤỦŨƯỪỨỰỬỮỲÝỴỶỸĐ][a-zàáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđ]*)*$", ErrorMessage ="Tên không đúng định dạng")]
    public string Name { get; set; } = string.Empty;
    [Required, Display(Name = "Giới tính")]
    public Gender Gender { get; set; }
    [Required(ErrorMessage = "Phải nhập địa chỉ"), Display(Name = "Địa chỉ")]
    public string Address { get; set; } = string.Empty;
    [Required(ErrorMessage = "Phải nhập ngày sinh"), Display(Name = "Ngày sinh")]
    public DateTime Birthday { get; set; } = DateTime.UtcNow;
}

public class EditStudentCommand : CreateStudentCommand
{
    public string Id { get; set; } = string.Empty;
}