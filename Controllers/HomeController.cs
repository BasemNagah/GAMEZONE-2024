using GAMEZONE.Models;
using GAMEZONE.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GAMEZONE.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGamesService _gamesService;

        public HomeController(IGamesService gamesService)
        {
            _gamesService = gamesService ;
        }

        public IActionResult Index()
        {
            return View(_gamesService.GetAll());
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
