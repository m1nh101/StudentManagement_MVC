using System.Text;
using Application.DTOs.Students;
using Application.Extensions;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace MVC.Controllers;

[Route("Student")]
public class StudentController : Controller
{
  private readonly IUnitOfWork _worker;
  public StudentController(IUnitOfWork worker)
  {
    _worker = worker;
  }

  // GET
  public async Task<IActionResult> Index(string input = "")
  {
    IEnumerable<StudentsResponse> _response;
    if (input == string.Empty || input == null)
      _response = await _worker.StudentService.GetAll();
    else
      _response = await _worker.StudentService.Filter(input);
    return View(_response);
  }

  [HttpGet]
  [Route("Detail/{id}")]
  public async Task<IActionResult> Detail([FromRoute] string id)
  {
    var response = await _worker.StudentService.GetDetail(id);

    if (response == null)
      return NotFound();

    var editModel = new EditStudentCommand
    {
      Id = response.Id,
      Name = response.Name,
      Birthday = response.Birthday,
      Gender = response.Gender == "nam" ? Application.Enums.Gender.Male : Application.Enums.Gender.Female,
      Address = response.Address
    };

    return View(editModel);
  }

  [HttpGet]
  [Route("Create")]
  public IActionResult Create() => View();

  [HttpPost]
  [Route("Create")]
  public async Task<IActionResult> Create(CreateStudentCommand command)
  {
    await _worker.StudentService.CreateNew(command);
    return RedirectToAction("Index");
  }

  [HttpPost]
  [Route("Edit")]
  public async Task<IActionResult> Edit(EditStudentCommand command)
  {
    var response = await _worker.StudentService.Update(command);

    if (response)
      return RedirectToAction("Index", "Student");
    return BadRequest();
  }

  [HttpPost]
  [Route("Delete")]
  public async Task<IActionResult> Remove(string id)
  {
    var response = await _worker.StudentService.Delete(id);

    if (response)
      return RedirectToAction("Index", "Student");
    return BadRequest();
  }

  [HttpGet]
  [Route("Statistic")]
  public async Task<IActionResult> Statistic()
  {
    var response = await _worker.StudentService.Statistic();
    return View(response);
  }

  [Route("Export")]
  public async Task<IActionResult> Export()
  {
    var json = await _worker.StudentService.ExportToJson();
    var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
    return new FileStreamResult(stream, new MediaTypeHeaderValue("text/plain"))
    {
      FileDownloadName = "students.json"
    };
  }
}