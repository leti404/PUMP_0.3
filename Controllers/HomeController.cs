using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PruebaMVC.Models;
using System.IO;

namespace PruebaMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    public static Producto ProductoForm;
    public static int TipoBUSQUEDA;
    public static string TextoBUSQUEDA;
    public static string CodigoBUSQUEDA;

    [HttpPost]
    public IActionResult Index(int tipoBusqueda, string textoBusqueda, string codiCorto, string fabricanteNomb, string ProduSele)
    {
        if(tipoBusqueda == 2 && codiCorto == null)
        {
            codiCorto = "%%";
        }
        ViewBag.ListaNombresProduBusqueda = BD.ListaBusquedaNombProdu(tipoBusqueda, "%" + textoBusqueda + "%", codiCorto);
        if(ProduSele != null)
        {
            var parts = ProduSele.Split('-');
            string secondLastPart = parts.Length >= 2 ? parts[^2].Trim() : null;
            string lastPart = parts.Length >= 1 ? parts[^1].Trim() : null;
            ViewBag.Producto = BD.ObtenerInfoCompletProdu(lastPart, secondLastPart);
            ProductoForm = ViewBag.Producto;
        }
        else if(ProduSele == null && ViewBag.ListaNombresProduBusqueda.Count > 0)
        {
            string pepe1 = ViewBag.ListaNombresProduBusqueda[0];
            var parts = pepe1.Split('-');
            string secondLastPart = parts.Length >= 2 ? parts[^2].Trim() : null;
            string lastPart = parts.Length >= 1 ? parts[^1].Trim() : null;
            ViewBag.Producto = BD.ObtenerInfoCompletProdu(lastPart, secondLastPart);
            ProductoForm = ViewBag.Producto;
        }
        else
        {
            ViewBag.Producto = null;
        }
        if(tipoBusqueda == 5)
        {
            ViewBag.ListaFabri = BD.ObtenerFabriListaBusqueda(textoBusqueda);
            ViewBag.ListaMiniCod = null;
            ViewBag.Fabri = fabricanteNomb;
            ViewBag.MiniCod = null;
        }
        else if(tipoBusqueda == 2)
        {
            ViewBag.ListaMiniCod = BD.ObtenerListaCodigos();
            ViewBag.ListaFabri = null;
            ViewBag.MiniCod = codiCorto;
            ViewBag.Fabri = null;
        }
        else
        {
            ViewBag.ListaFabri = null;
            ViewBag.ListaMiniCod = null;
        }
        
        ViewBag.CountResEncont = ViewBag.ListaNombresProduBusqueda.Count;
        ViewBag.TextoBusqueda = textoBusqueda;
        ViewBag.TipoBusqueda = tipoBusqueda;
        TipoBUSQUEDA = tipoBusqueda;
        TextoBUSQUEDA = textoBusqueda;
        CodigoBUSQUEDA = codiCorto;
        return View();
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Tabla()
    {
        ViewBag.ContTabla = BD.ObtenerItemsTabla(TipoBUSQUEDA, "%" + TextoBUSQUEDA + "%", CodigoBUSQUEDA);
        return View();
    }
    [HttpPost]
    public IActionResult Formula(int multiplicador, string formulaSele)
    {
        if(multiplicador == 0)
        {
            multiplicador = 1;
        }
        string firstPart;
        ViewBag.Producto = ProductoForm;
        ViewBag.FormulaProdu = BD.ObtenerListaFormula(ProductoForm.CODIGO, ProductoForm.TIPO);
        if(ViewBag.FormulaProdu.Count > 0 && formulaSele == null)
        {
            formulaSele = ViewBag.FormulaProdu[0];
            firstPart = formulaSele.Split('-')[0].Trim();
            ViewBag.FormulaDatos = BD.ObtenerComponentesFormula(ProductoForm.CODIGO, ProductoForm.TIPO, multiplicador, firstPart);

        }
        else if(ViewBag.FormulaProdu.Count > 0  && formulaSele != null)
        {
            firstPart = formulaSele.Split('-')[0].Trim();
            ViewBag.FormulaDatos = BD.ObtenerComponentesFormula(ProductoForm.CODIGO, ProductoForm.TIPO, multiplicador, firstPart);

        }
        return View();
    }
    public IActionResult Formula()
    {
        return View();
    }
    public IActionResult SeguimientoFormula()
    {
        ViewBag.FormulaProdu = BD.ObtenerListaFormula(ProductoForm.CODIGO, ProductoForm.TIPO);
        ViewBag.Producto = ProductoForm;
        ViewBag.SeguiFor = BD.SeguimientoFormula();
        return View();
    }




    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

