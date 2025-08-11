using Microsoft.AspNetCore.Mvc;
using ShoesStore.InterfaceRepositories;
using ShoesStore.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using System.Diagnostics.Eventing.Reader;
using ShoesStore.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShoesStore.Controllers
{
	public class HomeController : Controller
	{
		ShoesDbContext _context; IKhuyenMai kmRepo; 
		public HomeController(ShoesDbContext context, IKhuyenMai km)
		{
			_context = context;
			this.kmRepo = km;
		}
		public IActionResult Index()
		{
            var banners = _context.Banners.Where(b => b.Hoatdong).ToList();
			int checkKm = kmRepo.GetAllKhuyenMaiToday("","",0,0,0, -1).Count == 0 ? 0 : 1;

			HttpContext.Session.SetInt32("Khuyenmai", checkKm);

			return View(banners);
		}

	
		public IActionResult Zalo()
		{
			return View();
		}

	}
}