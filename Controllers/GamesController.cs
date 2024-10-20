

using GAMEZONE.Models;
using GAMEZONE.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GAMEZONE.Controllers
{
    public class GamesController : Controller
    {
        private readonly ICategoriesServices _categoriesServices;
        private readonly IDevicesServices _devicesServices ;
        private readonly IGamesService _gamesServices ;

        public GamesController(ICategoriesServices categoriesServices, IDevicesServices devicesServices ,IGamesService gamesServices)
        {
            _categoriesServices = categoriesServices;
            _devicesServices = devicesServices;
            _gamesServices = gamesServices;
        }

        public IActionResult Index()
        {
            var games = _gamesServices.GetAll();
            return View(games);
        }

        public IActionResult Details(int id)
        {
            var game = _gamesServices.GetById(id);
            if (game is null)
                return NotFound();

            return View(game);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormViewModel ViewModel = new()
            {
                Categories = _categoriesServices.GetSelectListItems(),

                Devices = _devicesServices.GetSelectListItems()

            };
            return View(ViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>  Create(CreateGameFormViewModel model)
        {
            if(!ModelState.IsValid)
            {
                model.Categories = _categoriesServices.GetSelectListItems();

                model.Devices = _devicesServices.GetSelectListItems();

                return View(model);
            }
           
            await _gamesServices.Create(model);
                
                return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game = _gamesServices.GetById(id);
            if (game is null)
                return NotFound();

            EditGameFormViewModel viewModel = new()
            {
                Id = id,
                Name = game.Name,
                Description = game.Description,
                CategoryId = game.CategoryId,
                SelectedDevices = game.Devices.Select(d =>d.DeviceId).ToList(),
                Categories = _categoriesServices.GetSelectListItems(),
                Devices = _devicesServices.GetSelectListItems(),
                CurrentCover = game.Cover
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesServices.GetSelectListItems();

                model.Devices = _devicesServices.GetSelectListItems();

                return View(model);
            }

            var game =await _gamesServices.Update(model);
            if (game is null)
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
           bool isDeleted = _gamesServices.Delete(id);
            return isDeleted? Ok():BadRequest();
        }


    }
}
