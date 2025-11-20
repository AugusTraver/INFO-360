using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using INFO_360.Models;
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
        //string msj = "";
        if (usuario == null)
        {
           // msj = "Usuario o Cotnrasñea incorrectos ";
            //ViewBag.mensaje = msj;
            return View("Login");

        }
        else
        {
            HttpContext.Session.SetString("usuario", Objeto.ObjectToString(usuario));

            return RedirectToAction("DTareas", "Home");
        }
    }
    public IActionResult DRegistrarse()
    {
        return View("Registrarse");
    }

    [HttpPost]
    public IActionResult Registrarse(string Email, string username, string nombre, string contraseña, IFormFile fotoFile)
    {
        string Foto = null;
         bool existe = BD.VerificarUsuarioExiste(Email, username);
        // string msj = "";
         if (existe)
         {
        //string.msj = "Este usuario o email ya existe.";
        //ViewBag.msj = msj;
        return View("Registrarse"); 
        }



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

        Usuario usuario = new Usuario(Email, username, contraseña, nombre, Foto);

        bool pudo = BD.Registrarse(usuario);
        HttpContext.Session.SetString("usuario", Objeto.ObjectToString(usuario));
        if (pudo)
        {
            return RedirectToAction("DLogin", "Account");
        }
        else
        {
            ViewBag.pudo = pudo;
            return View("Registrarse", "Account");
        }
    }
}
