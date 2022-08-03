namespace Application.DTOs.Students;

public class StatisticStudent
{
  public StudentsResponse YoungestStudent { get; set; } = new();
  public StudentsResponse OldestStudent { get; set; } = new();
}