using Microsoft.AspNetCore.Mvc.Rendering;

namespace GAMEZONE.Services
{
    public interface ICategoriesServices
    {
        IEnumerable<SelectListItem> GetSelectListItems();
    }
}
