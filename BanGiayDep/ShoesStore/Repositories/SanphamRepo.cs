using Microsoft.EntityFrameworkCore;
using ShoesStore.InterfaceRepositories;
using ShoesStore.Models;
using ShoesStore.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace ShoesStore.Repositories
{
	public class SanphamRepo : ISanpham
	{
		ShoesDbContext _context;
		public SanphamRepo(ShoesDbContext context)
		{
			_context = context;
		}

		public Sanpham Getsanpham(int masp)
		{
			Sanpham sp = _context.Sanphams.FirstOrDefault(x => x.Masp == masp);
			return sp;

		}
		public SanphamViewModel HienThiSanpham(int madongsanpham, int masp)
		{
			DateTime today = DateTime.Now.Date;
			Dongsanpham dongsp = _context.Dongsanphams.FirstOrDefault(x => x.Madongsanpham == madongsanpham);

			Khuyenmai KmTodayThatdsp = _context.Khuyenmais.Include(x => x.Madongsanphams)
											.ThenInclude(d => d.Makms)
											.FirstOrDefault(a => a.Ngaybd <= today && today < a.Ngaykt && a.Madongsanphams.Any(k => k.Madongsanpham == madongsanpham));

			dongsp.Makms.Add(KmTodayThatdsp);

			List<Sanpham> sp = _context.Sanphams.
				Where(x => x.Masp != masp && x.Madongsanpham == madongsanpham)
				.ToList();
			Sanpham ctFirst = _context.Sanphams.FirstOrDefault(x => x.Masp == masp);

			ctFirst.MamauNavigation = _context.Maus.FirstOrDefault(x => x.Mamau == ctFirst.Mamau);
			sp.Insert(0, ctFirst);

			SanphamViewModel pDetail = new SanphamViewModel
			{
				dongsanpham = dongsp,
				sanphams = sp,
				tenSize = _context.Sizes
					.Select(x => x.Tensize).ToList(),
				slton = _context.Sanphamsizes
						.Where(x => x.Masp == masp)
						.Select(x => x.Slton).ToList()
			};

			return pDetail;
		}

		public List<SanPhamHomeViewModel> HomeSanPham(int trangthai)
		{
			DateTime today = DateTime.Now.Date;
			List<Khuyenmai> allkmToday = _context.Khuyenmais.Include(x => x.Madongsanphams)
											.ThenInclude(d => d.Sanphams).Where(x => x.Ngaybd >= today && x.Ngaykt <= today)
											.ToList();
			List<Sanpham> sp = _context.Sanphams.Where(x => x.TrangThai == (Sanpham.TrangThaiEnum)trangthai)
											.Include(x => x.MadongsanphamNavigation)
											.Include(x => x.MamauNavigation).ToList();
			List<SanPhamHomeViewModel> spViewHome = new List<SanPhamHomeViewModel>();

			foreach (var sanpham in sp)
			{

				bool check = true;
				foreach (Khuyenmai km in allkmToday)
				{
					if (km.Madongsanphams.FirstOrDefault(x => x.Sanphams.Contains(sanpham)) != null)
					{
						check = false;
						break;
					}
				}
				if (check)
				{
					spViewHome.Add(new SanPhamHomeViewModel
					{
						sp = sanpham,
						phantramgiam = 0
					});
				}
				//Only add product has hot and new without sale, because when it sale that is in slider sale



			}

			return spViewHome;

		}

		public FavouriteProductsItem GetFavProById(int id)
		{
			Sanpham sp = _context.Sanphams.FirstOrDefault(x => x.Masp == id);
			Dongsanpham dsp = _context.Dongsanphams.FirstOrDefault(x => x.Madongsanpham == sp.Madongsanpham);
			DateTime today = DateTime.Now;
			int phantramgiam = _context.Khuyenmais.FirstOrDefault(x => x.Madongsanphams.Contains(dsp) && x.Ngaybd <= today && x.Ngaykt >= today)?.Phantramgiam ?? 0;
			FavouriteProductsItem favPro = new FavouriteProductsItem
			{
				Id= id,
				Madongsp = dsp.Madongsanpham,
				Tensp =dsp.Tendongsp,
				Hinhanh = sp.Anhdaidien,
				Gia = dsp.Giagoc,
				Phantramgiam = phantramgiam
			};

			return favPro;
		}
	}
}
