using Microsoft.AspNetCore.Mvc.Rendering;

namespace GAMEZONE.Services
{
    
    public class DevicesServices : IDevicesServices
    {
        private readonly ApplicationDbContext _context;

        public DevicesServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetSelectListItems()
        {
            return _context.Devices.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .OrderBy(d => d.Text).AsNoTracking().ToList();
        }
    }
}
