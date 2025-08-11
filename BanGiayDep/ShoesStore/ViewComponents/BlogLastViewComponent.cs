using Microsoft.AspNetCore.Mvc;
using ShoesStore.Models;

namespace ShoesStore.ViewComponents
{
	public class BlogLastViewComponent : ViewComponent
	{
		ShoesDbContext context;
		public BlogLastViewComponent(ShoesDbContext context)
		{
			this.context = context;
		}

		public IViewComponentResult Invoke(string theloai)
		{
			List<Blog> blogList = context.Blogs.Where(x=>x.Theloai == theloai).ToList();
			return View(blogList);
		}
	}
}
