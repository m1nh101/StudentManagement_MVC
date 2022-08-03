namespace Application.DTOs.Classes;

public class ClassDetailResponse
{
  public string Id { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public string Monitor { get; set; } = string.Empty;
  public string Secretary { get; set; } = string.Empty;
  public string[] Subjects { get; set; } = Array.Empty<string>();
  public string[] Students { get; set; } = Array.Empty<string>();
}