using System.Text;
using Application.DTOs.Classes;
using Application.Extensions;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace MVC.Controllers;

[Route("Class")]
public class ClassController : Controller
{
  private readonly IUnitOfWork _worker;

  public ClassController(IUnitOfWork worker)
  {
    _worker = worker;
  }

  [HttpGet]
  public async Task<IActionResult> Index()
  {
    var response = await _worker.ClassService.GetAll();
    return View(response);
  }

  [HttpGet]
  [Route("Create")]
  public async Task<IActionResult> Create()
  {
    var subjects = await _worker.SubjectService.GetAll();
    var students = await _worker.StudentService.GetAll();

    ViewData["subjects"] = subjects;
    ViewData["students"] = students;

    return View();
  }

  [HttpGet]
  [Route("Detail/{id}")]
  public async Task<IActionResult> Detail([FromRoute] string id)
  {
    var detail = await _worker.ClassService.GetDetail(id);

    if (detail == null)
      return NotFound();

    var subjects = await _worker.SubjectService.GetAll();
    var students = await _worker.StudentService.GetAll();

    ViewData["subjects"] = subjects;
    ViewData["students"] = students;
    ViewData["detail"] = detail;

    return View();
  }

  public async Task<IActionResult> Edit(EditClass request)
  {
    var response = await _worker.ClassService.Update(request);

    if (response)
      return RedirectToAction("Index");
    
    return BadRequest();
  }

  [HttpPost]
  [Route("Create")]
  public async Task<IActionResult> Create(CreateClass request)
  {
    _ = await _worker.ClassService.CreateNew(request); 
    return RedirectToAction("Index");
  }

  [HttpPost]
  [Route("Remove")]
  public async Task<IActionResult> Remove(string id)
  {
    var response = await _worker.ClassService.Delete(id);
    if (response)
      return RedirectToAction("Index");
    
    return BadRequest();
  }

  [HttpGet]
  [Route("Max")]
  public async Task<IActionResult> MaxMale()
  {
    var response = await _worker.ClassService.FindClassHaveHighestMale();
    ViewData["max"] = response;
    return View();
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