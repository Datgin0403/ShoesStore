using Microsoft.AspNetCore.Mvc;
using ShoesStore.InterfaceRepositories;
using ShoesStore.Models;
using ShoesStore.ViewModels;

namespace ShoesStore.ViewComponents
{
    public class HomeProductSlider2ViewComponent : ViewComponent
    {
        ISanpham spRepo;
        public HomeProductSlider2ViewComponent(ISanpham spRepo)
        {
            this.spRepo = spRepo;
        }

        public IViewComponentResult Invoke(int trangthai)
        {
            List<SanPhamHomeViewModel> spViewHome = spRepo.HomeSanPham(trangthai);
            return View(spViewHome);
        }
    }
}
