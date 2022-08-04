using Microsoft.Net.Http.Headers;
using System.Text;
using Application.DTOs.Subjects;
using Application.Extensions;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

public class SubjectController : Controller
{
  private readonly IUnitOfWork _worker;

  public SubjectController(IUnitOfWork worker)
  {
    _worker = worker;
  } 

  [HttpGet]
  public IActionResult Index() => View();

  public async Task<IActionResult> GetAll()
  {
    var response = await _worker.SubjectService.GetAll();

    return Json(new { data = response});
  }

  [HttpGet]
  public IActionResult Create() => View();

  [HttpPost]
  public async Task<IActionResult> Create(CreateSubject request)
  {
    if(!ModelState.IsValid)
      return View(request);

    await _worker.SubjectService.CreateNew(request);

    return RedirectToAction("Index");
  }

  [HttpGet]
  [Route("Detail/{id}")]
  public async Task<IActionResult> Detail([FromRoute] string id)
  {
    var response = await _worker.SubjectService.GetDetail(id);

    if (response == null)
      return NotFound();

    var editModel = new EditSubject
    {
      Id = response.Id,
      Name = response.Name
    };

    return View(response);
  }

  [HttpPost]
  public async Task<IActionResult> Edit(EditSubject subject)
  {
    if(!ModelState.IsValid)
      return View(subject);

    var response = await _worker.SubjectService.Update(subject);

    if (response)
      return RedirectToAction("Index", "Subject");
    
    return BadRequest();
  }

  [HttpDelete]
  public async Task<IActionResult> Remove(string id)
  {
    var response = await _worker.SubjectService.Remove(id);

    if (response)
      return NoContent();

    return BadRequest();
  }

  [Route("Export")]
  public async Task<IActionResult> Export()
  {
    var json = await _worker.ClassService.ExportToJson();
    var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
    return new FileStreamResult(stream, new MediaTypeHeaderValue("text/plain"))
    {
      FileDownloadName = "subjects.json"
    };
  }
}