using GameZone.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace GameZone.ViewModels
{
    public class CreateGameFormViewModel
    {
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;
       
       
        public int Categoryid { get; set; }
        public IEnumerable<SelectListItem> Categorise {  get; set; }  = Enumerable.Empty<SelectListItem>();

        public List<int> SelectedDevices { get; set; } = default!;
        public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();
        public string? CurrentCover { get; set; }
        public string Description { get; set; } = string.Empty;
        [AllowedExtentions(FileSettings.AllowedExtentions)]
        [MaxSizeeAttribute(FileSettings.MaxAllowedSizeInBytes)]
        public IFormFile? Cover { get; set; } = default!;
    }
}
