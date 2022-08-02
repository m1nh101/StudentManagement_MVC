namespace Application.DTOs.Classes;

public class ClassResponse
{
  public string Id { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public string Monitor { get; set; } = string.Empty;
  public string Secretary { get; set; } = string.Empty;
  public int Glosbe { get; set; }
  public int MaleGlosbe { get; set; }
  public int FemaleGlosbe { get; set; }
  public string[] Subjects { get; set; } = Array.Empty<string>();
}