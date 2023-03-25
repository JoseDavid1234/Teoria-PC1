using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using teoria_pc1.Models;

namespace teoria_pc1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost]
    public ActionResult CalcularPago()
    {
        // Obtener valores del formulario
        string numeroTarjeta = Request.Form["numeroTarjeta"];
        DateTime fechaVencimiento = DateTime.Parse(Request.Form["fechaVencimiento"]);
        decimal montoPagar = Decimal.Parse(Request.Form["montoPagar"]);

        System.Console.WriteLine(numeroTarjeta);

        // Calcular intereses por mora
        int diasMora = Math.Max((int)(DateTime.Now - fechaVencimiento).TotalDays,0);
        decimal interesMora = diasMora * montoPagar * 0.00005M;

        // Calcular total a pagar
        decimal totalPagar = montoPagar + interesMora;

        // Enviar resultados al archivo .cshtml a través del objeto ViewBag
        ViewBag.MontoPagar = montoPagar.ToString("0.00");
        ViewBag.DiasMora = diasMora;
        ViewBag.InteresMora = interesMora.ToString("0.00");
        ViewBag.TotalPagar = totalPagar.ToString("0.00");

        // Redirigir a la vista de resultados
        return View("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
