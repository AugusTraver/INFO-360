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
        string IDu = HttpContext.Session.GetString("usuario");
        if (IDu == null)
        {

            return View("Login");
        }
        Usuario usuario = Objeto.StringToObject<Usuario>(IDu);
        usuario.ListaTareas = usuario.ObtenerTareas();

        ViewBag.Tareas = usuario.ListaTareas;
        ViewBag.IDUsuario = usuario.ID;
        return View("Tareas");
    }
    public IActionResult CrearTarea(string Titulo, bool Finalizado, string Descripcion, int Duracion, int IDusuario, DateTime fecha)
    {
        string x = HttpContext.Session.GetString("usuario");
        Usuario usuario = Objeto.StringToObject<Usuario>(x);
        Tarea TareaCrear = new Tarea(Titulo, Finalizado, Descripcion, Duracion, usuario.ID,fecha);
        usuario.CrearTarea(TareaCrear);


        usuario.ListaTareas = usuario.ObtenerTareas();

        HttpContext.Session.SetString("usuario", Objeto.ObjectToString(usuario));
        ViewBag.Tareas = usuario.ListaTareas;
        ViewBag.IDUsuario = usuario.ID;
        return View("Tareas");
    }
    public IActionResult ActualizarTarea(string Titulo, bool Finalizada, string Descripcion, int Duracion,int ID, int IDusuario, DateTime fecha)
    {
        string x = HttpContext.Session.GetString("usuario");
        Usuario usuario = Objeto.StringToObject<Usuario>(x);
        Tarea TareaCrear = new Tarea( Titulo, Finalizada, Descripcion, Duracion, usuario.ID,fecha);
        usuario.ActualizarTarea(TareaCrear, ID);


        usuario.ListaTareas = usuario.ObtenerTareas();

        HttpContext.Session.SetString("usuario", Objeto.ObjectToString(usuario));
        ViewBag.Tareas = usuario.ListaTareas;
        ViewBag.IDUsuario = usuario.ID;
        return View("Tareas");
    }
    public IActionResult BorrarTarea(int idTarea)
    {
        string x = HttpContext.Session.GetString("usuario");
        Usuario usuario = Objeto.StringToObject<Usuario>(x);
        usuario.BorrarTarea(idTarea);


        usuario.ListaTareas = usuario.ObtenerTareas();

        HttpContext.Session.SetString("usuario", Objeto.ObjectToString(usuario));
        ViewBag.Tareas = usuario.ListaTareas;
        ViewBag.IDUsuario = usuario.ID;
        return View("Tareas");
    }
    public IActionResult CrearAlarma(string Tipo, string Nombre, DateTime Dia, int Duracion, int IDusuario, bool Activo)
    {
        string? x = HttpContext?.Session.GetString("usuario");
        Usuario? usuario = Objeto.StringToObject<Usuario>(x);
        Alarmas? AlarmaCrear = new Alarmas(Tipo, Nombre, Dia, Duracion, Activo, IDusuario);
        usuario?.CrearAlarma(AlarmaCrear);
        HttpContext.Session.SetString("usuario", Objeto.ObjectToString(usuario));
        return View("Alarmas");
    }
    public IActionResult ActualizarAlarma(string Tipo, string Nombre, DateTime Dia, int Duracion, int IDusuario, bool Activo)
    {
        string? x = HttpContext?.Session.GetString("usuario");
        Usuario? usuario = Objeto.StringToObject<Usuario>(x);
        Alarmas? AlarmaCrear = new Alarmas(Tipo, Nombre, Dia, Duracion, Activo, IDusuario);
        usuario?.ActualizarAlarma(AlarmaCrear);
        HttpContext.Session.SetString("usuario", Objeto.ObjectToString(usuario));
        return View("Alarmas");
    }
    public IActionResult BorrarAlarma(int IDA)
    {
        string? x = HttpContext?.Session.GetString("usuario");
        Usuario? usuario = Objeto.StringToObject<Usuario>(x);
        usuario?.BorrarAlarma(IDA);
        HttpContext.Session.SetString("usuario", Objeto.ObjectToString(usuario));
        return View("Alarmas");
    }
    public IActionResult DAlarmas()
    {
        return View("Alarmas");
    }
    public IActionResult DHorasLibres()
    {
        string? x = HttpContext?.Session.GetString("usuario");
        Usuario? usuario = Objeto.StringToObject<Usuario>(x);


        ViewBag.DiaTiempoLibre = usuario.TiempoLibrexDia;
        ViewBag.TiempoLibreTotal = usuario.CalcularTiempoLibre();
        ViewBag.ListaTareas = usuario.ListaTareas;
        ViewBag.TiempoTareas = usuario.CalcularTiempoTareas();

        return View("HorasLibres");
    }
    public IActionResult DPerfil()
    {
        return View("Perfil");
    }
    public IActionResult OrganizarAgenda()
    {
        string x = HttpContext.Session.GetString("usuario");
        Usuario usuario = Objeto.StringToObject<Usuario>(x);

        string y = HttpContext.Session.GetString("temporales");
        Dictionary<double, Tarea> temporales = Objeto.StringToObject<Dictionary<double, Tarea>>(y);


        Dictionary<DateTime, Dictionary<double, Tarea>> AGENDA = usuario.OrganizarSemana(temporales);

        ViewBag.AGENDA = AGENDA;

        return View("Agenda");
    }
    public IActionResult DOrganizador()
    {
        string x = HttpContext.Session.GetString("temporales");
        Dictionary<double, Tarea> temporales = new Dictionary<double, Tarea>();

        if (!string.IsNullOrEmpty(x))
            temporales = Objeto.StringToObject<Dictionary<double, Tarea>>(x);

        ViewBag.Temporales = temporales;

        return View("Organizador");
    }

    [HttpPost]
    public IActionResult AgregarTemporal(string titulo, int duracion)
    {
        string x = HttpContext.Session.GetString("temporales");
        Dictionary<double, Tarea> temporales = new Dictionary<double, Tarea>();

        if (x != null)
        {
            temporales = Objeto.StringToObject<Dictionary<double, Tarea>>(x);

        }

        Tarea t = new Tarea(titulo, duracion);

        double clave = temporales.Count + 1;
        temporales[clave] = t;

        HttpContext.Session.SetString("temporales", Objeto.ObjectToString(temporales));

        return RedirectToAction("DOrganizador");
    }

    [HttpPost]
    public ActionResult LimpiarTemporales()
    {
        HttpContext.Session.Remove("temporales");
        return RedirectToAction("DOrganizador");
    }

}
