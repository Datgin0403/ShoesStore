using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoesStore.Areas.Admin.InterfaceRepositories;
using ShoesStore.Areas.Admin.ViewModels;
using ShoesStore.InterfaceRepositories;
using ShoesStore.Models;
using ShoesStore.Models.Authentication;
using ShoesStore.ViewModels;

namespace ShoesStore.Areas.Admin.Controllers
{
	[Area("Admin")]
    [AuthenticationM_S]
    public class KhuyenMaiAdminController : Controller
	{
		private readonly IKhuyenMaiAdmin _kmrepo;
		private readonly ShoesDbContext _context;
		public KhuyenMaiAdminController(IKhuyenMaiAdmin kmrepo, ShoesDbContext context)
		{
			_kmrepo = kmrepo;
			_context = context;
		}

		public IActionResult Index()
		{
			return View(_kmrepo.GetAllKhuyenmai().ToList());
		}

		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Khuyenmai km)
		{
			if (!ModelState.IsValid)
			{
				return View(km);
			}

			_kmrepo.AddKhuyenmai(km);
			return RedirectToAction("Index");
		}

		public IActionResult Delete(int id)
		{
			_kmrepo.DeleteKhuyenmai(id);
			return RedirectToAction("Index");
		}


		public IActionResult AddDongSanPham(int makm)
		{
			var khuyenmai = _context.Khuyenmais
									.Include(k => k.Madongsanphams).ThenInclude(x => x.MaloaiNavigation)
									.FirstOrDefault(k => k.Makm == makm);

			if (khuyenmai == null)
			{
				// Handle the case where khuyenmai is not found
				return NotFound("Khuyenmai not found.");
			}

			var availableDongsanphams = _context.Dongsanphams
				.Select(d => new SelectListItem
				{
					Value = d.Madongsanpham.ToString(),
					Text = d.Tendongsp
				});

			var model = new KhuyenMaiViewModel
			{
				Makm = khuyenmai.Makm,
				Ngaybd = khuyenmai.Ngaybd,
				Ngaykt = khuyenmai.Ngaykt,
				Phantramgiam = khuyenmai.Phantramgiam,
				AvailableDongsanphams = availableDongsanphams
			};

			return View(model);
		}



		// POST: KhuyenMai/Create
		[HttpPost]
		public IActionResult AddDongSanPham(KhuyenMaiViewModel model)
		{
			var khuyenmai = _context.Khuyenmais
									.Include(k => k.Madongsanphams)
									.ThenInclude(x => x.MaloaiNavigation)
									.FirstOrDefault(k => k.Makm == model.Makm);

			if (khuyenmai == null)
			{
				return NotFound();
			}

			var selectedDongsanphams = _context.Dongsanphams
											   .Where(d => model.SelectedDongsanphams.Contains(d.Madongsanpham))
											   .ToList();

			foreach (var dongsanpham in selectedDongsanphams)
			{
				if (!khuyenmai.Madongsanphams.Contains(dongsanpham))
				{
					khuyenmai.Madongsanphams.Add(dongsanpham);
				}
			}

			_context.SaveChanges();

			return RedirectToAction("ListDongSanPham", new { makm = model.Makm });
		}

		public IActionResult ListDongSanPham(int makm)
		{

			var khuyenmai = _context.Khuyenmais
									.Include(k => k.Madongsanphams)
									.ThenInclude(m => m.MaloaiNavigation)
									.FirstOrDefault(k => k.Makm == makm);

			if (khuyenmai == null)
			{
				return NotFound();
			}

			return View(khuyenmai);
		}

	}
}
