using Microsoft.AspNetCore.Mvc;
using ShoesStore.InterfaceRepositories;
using ShoesStore.Models;
using ShoesStore.ViewModels;

namespace ShoesStore.ViewComponents
{
    public class PdSliderSaleViewComponent : ViewComponent
    {
        IKhuyenMai kmRepo;
        public PdSliderSaleViewComponent(IKhuyenMai kmRepo)
        {
            this.kmRepo = kmRepo;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.Ngaykt = kmRepo.getNgayktKmToday();
            List<DongsanphamViewModel> dongspView = new List<DongsanphamViewModel>();

			List<Khuyenmai> kmList = kmRepo.GetAllKhuyenMaiToday("","",0,0,0,-1).OrderByDescending(x=>x.Phantramgiam).ToList();
			foreach (Khuyenmai km in kmList)
			{
				List<DongsanphamViewModel> dongspViewTemp = km.Madongsanphams.Select(x => new DongsanphamViewModel
				{
					dongsp = x,
					Phantramgiam =  km.Phantramgiam
				}).ToList();
				dongspView = dongspView.Concat(dongspViewTemp).ToList();
			}
			return View(dongspView);
        }
    }
}
