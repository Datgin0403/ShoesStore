using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ShoesStore.Models;
using ShoesStore.InterfaceRepositories;
using X.PagedList;
using ShoesStore.ViewModels;

namespace ShoesStore.ViewComponents
{
	public class ShowCommentViewComponent : ViewComponent
	{
		IBinhLuan blRepo;
		public ShowCommentViewComponent(IBinhLuan blRepo)
		{
			this.blRepo = blRepo;
		}

		public IViewComponentResult Invoke()
		{
			int Masp = HttpContext.Session.GetInt32("Masp") ?? 0;
			CommentViewModel cmtView = blRepo.GetBlList(Masp);
			
			TempData["Comment"] = "Vui lòng đăng nhập trước khi comment";
			return View(cmtView);
		}
	}
}
