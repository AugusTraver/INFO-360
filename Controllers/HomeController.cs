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


    public IActionResult DTareas()
    {
        string IDu = HttpContext.Session.GetString("juego");
        if (IDu == null)
        {

            return View("Login");
        }
        return View("Tareas");
    }
    public IActionResult CrearTarea(string Titulo, bool Finalizada, string Descripcion, int Duracion, int IDusuario)
    {
        string x = HttpContext.Session.GetString("juego");
        Usuario usuario = Objeto.StringToObject<Usuario>(x);
        Tarea TareaCrear = new Tarea(Titulo, Finalizada, Descripcion, Duracion, IDusuario);
        usuario.CrearTarea(TareaCrear);
        HttpContext.Session.SetString("usuario", usuario.ToString());
        return View("Tareas");
    }
    public IActionResult ActualizarTarea(string Titulo, bool Finalizada, string Descripcion, int Duracion, int IDusuario)
    {
        string x = HttpContext.Session.GetString("juego");
        Usuario usuario = Objeto.StringToObject<Usuario>(x);
        Tarea TareaCrear = new Tarea(Titulo, Finalizada, Descripcion, Duracion, IDusuario);
        usuario.ActualizarTarea(TareaCrear);
        HttpContext.Session.SetString("usuario", usuario.ToString());
        return View("Tareas");
    }
    public IActionResult BorrarTarea(int idTarea)
    {
        string x = HttpContext.Session.GetString("juego");
        Usuario usuario = Objeto.StringToObject<Usuario>(x);
        usuario.BorrarTarea(idTarea);
        HttpContext.Session.SetString("usuario", usuario.ToString());
        return View("Tareas");
    }
    public IActionResult CrearAlarma(string Tipo, string Nombre, DateTime Dia, int Duracion, int IDusuario, bool Activo)
    {
        string? x = HttpContext?.Session.GetString("juego");
        Usuario? usuario = Objeto.StringToObject<Usuario>(x);
        Alarmas? AlarmaCrear = new Alarmas(Tipo, Nombre, Dia, Duracion, Activo, IDusuario);
        usuario?.CrearAlarma(AlarmaCrear);
        HttpContext.Session.SetString("usuario", usuario.ToString());
        return View("Alarmas");
    }
    public IActionResult ActualizarAlarma(string Tipo, string Nombre, DateTime Dia, int Duracion, int IDusuario, bool Activo)
    {
        string? x = HttpContext?.Session.GetString("juego");
        Usuario? usuario = Objeto.StringToObject<Usuario>(x);
        Alarmas? AlarmaCrear = new Alarmas(Tipo, Nombre, Dia, Duracion, Activo, IDusuario);
        usuario?.ActualizarAlarma(AlarmaCrear);
        HttpContext.Session.SetString("usuario", usuario.ToString());
        return View("Alarmas");
    }
    public IActionResult BorrarAlarma(int IDA)
    {
        string? x = HttpContext?.Session.GetString("juego");
        Usuario? usuario = Objeto.StringToObject<Usuario>(x);
        usuario?.BorrarAlarma(IDA);
        HttpContext.Session.SetString("usuario", usuario.ToString());
        return View("Alarmas");
    }
    public IActionResult DAlarmas()
    {
        return View("Alarmas");
    }
    public IActionResult DHorasLibres()
    {
        return View("HorasLibres");
    }
    public IActionResult DPerfil()
    {
        return View("Perfil");
    }
   
}
