using Application.DTOs.Students;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

public class StudentController : Controller
{
    private readonly IUnitOfWork _worker;
    public StudentController(IUnitOfWork worker)
    {
        _worker = worker;
    }
    
    // GET
    public async Task<IActionResult> Index()
    {
        var response = await _worker.StudentService.GetAll();
        return View(response);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(CreateStudentCommand command)
    {
        await _worker.StudentService.CreateNew(command);
        return RedirectToAction("Index");
    }
}