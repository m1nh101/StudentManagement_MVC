using Application.DTOs.Classes;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

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
  public async Task<IActionResult> Create()
  {
    var subjects = await _worker.SubjectService.GetAll();
    var students = await _worker.StudentService.GetAll();

    ViewData["subjects"] = subjects;
    ViewData["students"] = students;

    return View();
  }

  [HttpPost]
  public async Task<IActionResult> Create(CreateClass request)
  {
    _ = await _worker.ClassService.CreateNew(request); 
    return RedirectToAction("Index");
  }

  [HttpGet]
  public async Task<IActionResult> MaxMale()
  {
    var response = await _worker.ClassService.FindClassHaveHighestMale();
    ViewData["max"] = response;
    return View();
  }
}