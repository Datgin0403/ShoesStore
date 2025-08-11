using Microsoft.AspNetCore.Mvc;
using ShoesStore.Models;

namespace ShoesStore.ViewComponents
{
	public class HotDealViewComponent : ViewComponent
	{
		ShoesDbContext _context;
		public HotDealViewComponent(ShoesDbContext context)
		{
			_context = context;
		}

		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
