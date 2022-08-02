using Application.DTOs.Subjects;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

public class SubjectController : Controller
{
  private readonly ISubjectService _subject;

  public SubjectController(ISubjectService subject)
  {
    _subject = subject;
  } 

  [HttpGet]
  public async Task<IActionResult> Index()
  {
    var response = await _subject.GetAll();
    return View(response);
  }

  [HttpGet]
  public IActionResult Create() => View();

  [HttpPost]
  public async Task<IActionResult> Create(CreateSubject request)
  {
    await _subject.CreateNew(request);

    return RedirectToAction("Index");
  }
}