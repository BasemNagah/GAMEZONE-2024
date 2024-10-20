using GAMEZONE.Attributes;
using GAMEZONE.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GAMEZONE.ViewModel
{
    public class EditGameFormViewModel:GameFormViewMiodel
    {
        public int Id { get; set; }
        public string? CurrentCover {  get; set; }

        [AllowedExtension(FileSettings.AllowedExtensions),
           MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Cover { get; set; } = default!;
    }
}
