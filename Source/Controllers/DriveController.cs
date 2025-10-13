using Microsoft.AspNetCore.Mvc;
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
}

