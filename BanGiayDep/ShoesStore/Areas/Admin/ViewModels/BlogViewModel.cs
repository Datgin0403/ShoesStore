using Microsoft.AspNetCore.Mvc;
using ShoesStore.Models;

namespace ShoesStore.Areas.Admin.ViewModels
{
    public class BlogViewModel
    {
        public int Mablog { get; set; }

        public int Manv { get; set; }

        public string? Noidung { get; set; }

        public string? Theloai { get; set; }
        public IFormFile BlogImage { get; set; }
  
    }
}
