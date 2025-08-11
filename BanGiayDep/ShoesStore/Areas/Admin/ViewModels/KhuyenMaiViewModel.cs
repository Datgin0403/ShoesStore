using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoesStore.Models;
using ShoesStore.ViewModels;

namespace ShoesStore.Areas.Admin.ViewModels
{
    public class KhuyenMaiViewModel
    {
        public int Makm { get; set; }
        public DateTime Ngaybd { get; set; }
        public DateTime Ngaykt { get; set; }
        public int Phantramgiam { get; set; }
        public List<int> SelectedDongsanphams { get; set; }
        public IEnumerable<SelectListItem> AvailableDongsanphams { get; set; }
    }
}
