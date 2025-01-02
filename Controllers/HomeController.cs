using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PruebaMVC.Models;

namespace PruebaMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Index(int tipoBusqueda, string textoBusqueda, string codiCorto, string fabricanteNomb)
    {
        ViewBag.ListaNombresProduBusqueda = BD.ListaBusquedaNombProdu(tipoBusqueda, "%" + textoBusqueda + "%", codiCorto);
        ViewBag.Producto = null; //BD.ObtenerInfoCompletProdu(codigo); //obtener el codigo del orto de js
        ViewBag.CountResEncont = null;
        return View();
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Tabla()
    {
        
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
