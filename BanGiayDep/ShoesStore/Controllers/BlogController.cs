using Microsoft.AspNetCore.Mvc;
using ShoesStore.Areas.Admin.InterfaceRepositories;
using System.Linq;

namespace ShoesStore.Controllers
{
	public class BlogController : Controller
	{
		private readonly IBlogAdmin _blogRepository;

		public BlogController(IBlogAdmin blogRepository)
		{
			_blogRepository = blogRepository;
		}

		public IActionResult Index()
		{
			var blogs = _blogRepository.GetBlogs();
			return View(blogs);
		}

		public IActionResult Detail(int id)
		{
			var blog = _blogRepository.GetBlog(id);
			if (blog == null)
			{
				return NotFound();
			}

			return View(blog);
		}
	}
}
