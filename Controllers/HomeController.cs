using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using INFO_360.Models;

namespace INFO_360.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View("LandingPage");
    }
}
