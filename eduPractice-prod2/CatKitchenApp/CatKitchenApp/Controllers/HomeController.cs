using System.Diagnostics;
using CatKitchenApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatKitchenApp.Controllers
{
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Получаем информацию об ошибке
            var exceptionHandlerPathFeature = HttpContext.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature?.Error != null)
            {
                // Логируем ошибку (по умолчанию пишется в консоль приложения)
                _logger.LogError(exceptionHandlerPathFeature.Error, "Произошла необработанная ошибка по пути: {Path}", exceptionHandlerPathFeature.Path);
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
