using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShoesStore.Areas.Admin.ViewModels
{
    public class BannerViewModel
    {
        public int Mabanner { get; set; }

        public string Tenbanner { get; set; } = null!;

        public IFormFile FrontImage { get; set; }

        public string Link { get; set; } = null!;

        public bool Hoatdong { get; set; }

        public string Slogan {  get; set; }
      
    }
}
