using GAMEZONE.Attributes;
using GAMEZONE.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GAMEZONE.ViewModel
{
    public class CreateGameFormViewModel:GameFormViewMiodel
    {
        

        [AllowedExtension(FileSettings.AllowedExtensions),
            MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile Cover { get; set; } = default!;
    }
}
