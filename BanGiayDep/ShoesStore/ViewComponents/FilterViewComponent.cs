using Microsoft.AspNetCore.Mvc;
using ShoesStore.InterfaceRepositories;
using ShoesStore.Models;

namespace ShoesStore.ViewComponents
{
	public class FilterViewComponent : ViewComponent
	{
		IMau mauRepo;
		public FilterViewComponent(IMau m)
		{
			this.mauRepo = m;
		}

		public IViewComponentResult Invoke()
		{
			List<Mau> mList = mauRepo.GetMauList();
			return View(mList);
		}
	}
}
