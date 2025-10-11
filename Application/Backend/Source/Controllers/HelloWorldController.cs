using Microsoft.AspNetCore.Mvc;

namespace backend.Source.Controllers;

[Route("api/[Controller]")]
public class HelloWorldController : ControllerBase
{
  public IActionResult Index()
  {
    return Ok("Hello World");
  }
}

