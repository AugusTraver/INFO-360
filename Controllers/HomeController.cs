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

    public IActionResult Verificar(string us, string cn)
    {

        string direccion = "Login";

        if (cn == BD.ObtenerContraseña(us))
        {
            direccion = "Tareas";
        }
        return RedirectToAction(direccion);     
    }
}
