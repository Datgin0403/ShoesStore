using Microsoft.AspNetCore.Mvc;
using ShoesStore.InterfaceRepositories;
using ShoesStore.Models;

namespace ShoesStore.ViewComponents
{
	public class KhuyenMaiViewComponent : ViewComponent
	{
		IKhuyenMai kmRepo;
		public KhuyenMaiViewComponent(IKhuyenMai kmRepo)
		{
			this.kmRepo = kmRepo;
		}

		public IViewComponentResult Invoke()
		{
			List<Khuyenmai> kmToday = kmRepo.GetAllKhuyenMaiToday("","",0,0,0,-1);
			return View(kmToday);
		}
	}
}
