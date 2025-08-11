using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoesStore.InterfaceRepositories;
using ShoesStore.Models;
using ShoesStore.Repositories;
using ShoesStore.ViewModels;
using X.PagedList;

namespace ShoesStore.Controllers
{
	public class KhuyenMaiController : Controller
	{
		IKhuyenMai kmRepo;IMau mauRepo; IHttpContextAccessor httpcontextAcc;
		public KhuyenMaiController(IKhuyenMai kmRepo, IMau mauRepo, IHttpContextAccessor httpcontextAcc)
		{
			this.kmRepo = kmRepo;
			this.mauRepo = mauRepo;
			this.httpcontextAcc = httpcontextAcc;
		}

		public IActionResult SaleIndex(string? searchStringKm, string? maMau, int? sortGia, decimal? minPrice, decimal? maxPrice,int phantramgiam)
		{
			CreateData();
			ViewBag.phantramgiam = phantramgiam;
			ViewBag.sortGia1 = sortGia;
			ViewBag.maMau1 = maMau;
			ViewBag.searchString1 = searchStringKm;
			ViewBag.minPrice1 = minPrice;
			ViewBag.maxPrice1 = maxPrice;
			ViewBag.Ngaykt = kmRepo.getNgayktKmToday();
			List<DongsanphamViewModel> dongspView = new List<DongsanphamViewModel>();

			List<Khuyenmai> kmList = kmRepo.GetAllKhuyenMaiToday(searchStringKm, maMau, sortGia, minPrice, maxPrice, phantramgiam);
			foreach (Khuyenmai km in kmList)
			{
				List<DongsanphamViewModel> dongspViewTemp = km.Madongsanphams.Select(x => new DongsanphamViewModel
				{
					dongsp = x,
					Phantramgiam = phantramgiam > 1 ? phantramgiam : km.Phantramgiam,
				}).ToList();
				dongspView = dongspView.Concat(dongspViewTemp).ToList();
			}


			dongspView = dongspView.Where(x =>  x.dongsp.Sanphams != null).ToList();
			if (IsAjaxRequest())
			{
				return PartialView("_PartialSanPhamTheoLoai", dongspView);
			}

			else
			{
				return View(dongspView);
			}

		}

		public IActionResult FindSaleProduct(string? searchStringKm, string? maMau, int? sortGia, decimal? minPrice, decimal? maxPrice, int phantramgiam)
		{
			CreateData();
			ViewBag.phantramgiam = phantramgiam;
			ViewBag.sortGia1 = sortGia;
			ViewBag.maMau1 = maMau;
			ViewBag.searchString1 = searchStringKm;
			ViewBag.minPrice1 = minPrice;
			ViewBag.maxPrice1 = maxPrice;
			ViewBag.Ngaykt = kmRepo.getNgayktKmToday();
			List<DongsanphamViewModel> dongspView = new List<DongsanphamViewModel>();

			List<Khuyenmai> kmList = kmRepo.GetAllKhuyenMaiToday(searchStringKm, maMau, sortGia, minPrice, maxPrice, phantramgiam);
			foreach (Khuyenmai km in kmList)
			{
				List<DongsanphamViewModel> dongspViewTemp = km.Madongsanphams.Select(x => new DongsanphamViewModel
				{
					dongsp = x,
					Phantramgiam = phantramgiam > 1 ? phantramgiam : km.Phantramgiam,
				}).ToList();
				dongspView = dongspView.Concat(dongspViewTemp).ToList();
			}


			dongspView = dongspView.Where(x =>  x.dongsp.Sanphams != null).ToList();
			return PartialView("_PartialSanPhamTheoLoai", dongspView);
		}

		private bool IsAjaxRequest()
		{
			var request = httpcontextAcc.HttpContext.Request;
			return request.Headers["X-Requested-With"] == "XMLHttpRequest";
		}
		public void CreateData()
		{
			List<SelectListItem> MauList = mauRepo.GetMauList().Select(x => new SelectListItem
			{
				Value = x.Mamau,
				Text = x.Tenmau
			}).ToList();
			ViewBag.MauList = MauList;
		}
	}
}
