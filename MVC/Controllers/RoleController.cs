using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

public class RoleController : Controller
{
  private readonly IUnitOfWork _worker;

  public RoleController(IUnitOfWork worker)
  {
    _worker = worker;
  }

  [HttpGet]
  public async Task<IActionResult> Index()
  {
    var response = await _worker.RoleService.GetAll();

    return View(response);
  }

  [HttpGet]
  public IActionResult Create() => View();

  [HttpPost]
  public async Task<IActionResult> Create(string name)
  {
    await _worker.RoleService.CreateNew(name);
    return RedirectToAction("Index");
  }
}