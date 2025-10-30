using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using  INFO_360.Models;
 namespace INFO_360.Controllers;


 public class AccountController : Controller
{
    public IActionResult DLogin()
    {
        return View("Login");
    }
     [HttpPost]
    public IActionResult Login(string username, string contraseña)
    {
        Usuario usuario = BD.IniciarSesion(username, contraseña);
        if (usuario == null)
        {
            return View("Login");
        }
        else{
        HttpContext.Session.SetString("usuario", usuario.ID.ToString());

        return RedirectToAction("LandingPage","Home");
        }
    }
     [HttpPost]
    public IActionResult DRegistrarse()
    {
        return View("Registrar");
    }

    [HttpPost]
    public IActionResult Registrarse(
    string username,
    string contraseña,
    string nombre,
    string apellido,
    IFormFile fotoFile)
  {
    string Foto = null;

    if (fotoFile != null && fotoFile.Length > 0)
    {
        string carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Imagenes");
        Directory.CreateDirectory(carpeta);

        Foto = Path.GetFileName(fotoFile.FileName);
        string ruta = Path.Combine(carpeta, Foto);

        using (var stream = new FileStream(ruta, FileMode.Create))
        {
            fotoFile.CopyTo(stream);
        }
    }

    Usuario usu = new Usuario(username, contraseña, nombre, Foto);

    bool pudo = BD.Registrarse(usu);

    if (pudo)
    {
        return RedirectToAction("DLogin","Account");
    }
    else
    {
        ViewBag.pudo = pudo;
        return View("Registrar","Account");
    }
  }





}
 