
using GAMEZONE.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace GAMEZONE.Services
{
    public class GamesService : IGamesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imgesPath;
        public GamesService(ApplicationDbContext context , IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imgesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagePath}";

        }

        public IEnumerable<Game> GetAll()
        {
            return _context.Games
                .Include(g => g.Category)
                .Include(d=>d.Devices)
                .ThenInclude(I=>I.Device)
                .AsNoTracking()
                .ToList();
        }

        public Game? GetById(int id)
        {
            return _context.Games
                 .Include(g => g.Category)
                 .Include(d => d.Devices)
                 .ThenInclude(I => I.Device)
                 .AsNoTracking()
                 .SingleOrDefault(g=>g.Id==id);
        }

        public async Task Create(CreateGameFormViewModel model)
        {
            var coverName = await SaveCover(model.Cover);

            Game game = new Game()
            {
                Name = model.Name,
                CategoryId = model.CategoryId,
                Devices = model.SelectedDevices.Select(d => new GameDevice{DeviceId =d}).ToList(),
                Description = model.Description,
                Cover = coverName,  
            };

            _context.Games.Add(game);
            _context.SaveChanges();

        }

        public async Task<Game?> Update(EditGameFormViewModel model)
        {
            var game = _context.Games.
                Include(g => g.Devices).
                SingleOrDefault(g =>g.Id == model.Id);

            if (game is null)
                return null;

            var hasNewCover = model.Cover is not null;
            var oldCover = game.Cover;

            game.Name = model.Name;
            game.CategoryId = model.CategoryId; 
            game.Description = model.Description;
            game.Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();

            if(hasNewCover)
            {
                game.Cover = await SaveCover(model.Cover!);
            }

            var EffectedRows = _context.SaveChanges();
            if(EffectedRows>0)
            {
                if(hasNewCover)
                {
                    var cover = Path.Combine(_imgesPath, oldCover);
                    File.Delete(cover);
                }
                return game;
            }
            else
            {
                if (hasNewCover)
                {
                    var cover = Path.Combine(_imgesPath, game.Cover);
                    File.Delete(cover);
                }
                return null;
            }

        }

        private async Task<string> SaveCover (IFormFile Cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension( Cover.FileName)}";
            var path = Path.Combine(_imgesPath, coverName);

            using var stream = File.Create(path);
            await Cover.CopyToAsync(stream);
            return coverName;
        }

        public bool Delete(int id)
        {
            bool isDeleted = false;
            var game = _context.Games.Find(id);
            if(game is not null)
            {
                _context.Games.Remove(game);
                var EffectedRows = _context.SaveChanges();
                if (EffectedRows > 0)
                {
                    isDeleted = true;
                    var cover = Path.Combine(_imgesPath, game.Cover);
                    File.Delete(cover);
                }
                   
            }
            return isDeleted;
        }
    }
}
