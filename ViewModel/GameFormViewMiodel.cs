using Microsoft.AspNetCore.Mvc.Rendering;

namespace GAMEZONE.ViewModel
{
    public class GameFormViewMiodel
    {
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();

        [Display(Name = "Supported Devices")]
        public List<int> SelectedDevices { get; set; } = default!;

        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;
    }
}
