using Microsoft.AspNetCore.Mvc;
using Source.Models;
using Source.Services;

namespace Source.Controllers;

public class DriveController : Controller
{
  private readonly DriveFileService _driveFileService;
  public DriveController(DriveFileService driveFileService)
  {
    _driveFileService = driveFileService;
  }

  public IActionResult Index()
  {
    return View();
  }
  [HttpGet]
  public IActionResult Create()
  {
    return View();
  }
  [HttpPost]
  public IActionResult Create(DriveFile obj)
  {
    _driveFileService.AddFile(obj);
    return Redirect("/Drive");
  }
}

