using Microsoft.AspNetCore.Mvc.Rendering;

namespace GAMEZONE.Services
{
    public interface IDevicesServices
    {
        IEnumerable<SelectListItem> GetSelectListItems();
    }
}
