using Microsoft.EntityFrameworkCore;
using ShoesStore.InterfaceRepositories;
using ShoesStore.Models;
using System.Diagnostics;

namespace ShoesStore.Repositories
{
	public class KhuyenMaiRepo : IKhuyenMai
	{

		ShoesDbContext context;
		public KhuyenMaiRepo(ShoesDbContext context)
		{
			this.context = context;
		}
		
		public DateTime getNgayktKmToday()
		{
			DateTime today = DateTime.Now.Date;
			Khuyenmai km = context.Khuyenmais.FirstOrDefault(x => x.Ngaybd <= today && today < x.Ngaykt);
			
			return km.Ngaykt;
		}

		public List<Khuyenmai> GetAllKhuyenMaiToday(string? searchString, string? maMau, int? sortGia, decimal? minPrice, decimal? maxPrice,int Phantramgiam)
		{
			DateTime today = DateTime.Now.Date;
			List<Khuyenmai> kmList = context.Khuyenmais
				.Include(k => k.Madongsanphams)
					.ThenInclude(d => d.Sanphams)
				.Where(x => x.Ngaybd <= today && today < x.Ngaykt)
				.ToList();

			if (!string.IsNullOrEmpty(maMau))
			{
				kmList = kmList.Select(x => new Khuyenmai
				{
					Madongsanphams = x.Madongsanphams.Select(d => new Dongsanpham
					{
						Madongsanpham = d.Madongsanpham,
						Maloai = d.Maloai,
						Tendongsp = d.Tendongsp,
						Mota = d.Mota,
						Giagoc = d.Giagoc,
						Sanphams = context.Sanphams.Where(a => a.Madongsanpham == d.Madongsanpham && a.Mamau == maMau).ToList(),
						MaloaiNavigation = context.Loais.FirstOrDefault(a => a.Maloai == d.Maloai)
					}).ToList(),
					Makm = x.Makm,
					Phantramgiam = x.Phantramgiam,
					Ngaybd = x.Ngaybd,
					Ngaykt = x.Ngaykt
				}).ToList();
			}

			kmList.ForEach(km => km.Madongsanphams = km.Madongsanphams.Where(d => d.Sanphams.Count != 0 ).ToList());

			if (sortGia != null && sortGia != 0)
			{
				kmList.ForEach(km => km.Madongsanphams = sortGia == 1 ? km.Madongsanphams.OrderBy(d => d.Giagoc-(d.Giagoc*km.Phantramgiam/100)).ToList() : km.Madongsanphams.OrderByDescending(d => d.Giagoc - (d.Giagoc * km.Phantramgiam / 100)).ToList());
			}

			if (!string.IsNullOrEmpty(searchString))
			{
				kmList.ForEach(km => km.Madongsanphams = km.Madongsanphams.Where(d => d.Tendongsp.ToLower().Contains(searchString.ToLower())).ToList());
			}

			if (minPrice != null && maxPrice != 0 && maxPrice != null)
			{
				kmList.ForEach(km => km.Madongsanphams = km.Madongsanphams.Where(d => d.Giagoc - (d.Giagoc * km.Phantramgiam / 100) >= minPrice && d.Giagoc - (d.Giagoc * km.Phantramgiam / 100) <= maxPrice).ToList());
			}
			if(Phantramgiam > 0)
			{
				kmList = kmList.Where(x => x.Ngaybd <= today && today < x.Ngaykt && x.Phantramgiam == Phantramgiam).ToList();
			}
			return kmList;
		}


		//public Khuyenmai GetKmByPercent(int percent)
		//{
		//	DateTime today = DateTime.Now.Date;

		//	Khuyenmai km2 = context.Khuyenmais.Include(k => k.Madongsanphams)// Include Dongsanpham entities related to Khuyenmai
		//		.ThenInclude(d => d.Makms) // Include Khuyenmai entities related to Dongsanpham
		//		.FirstOrDefault(x => x.Ngaybd <= today && today < x.Ngaykt && x.Phantramgiam == percent);
			
		//	for(int i=0;i<km2.Madongsanphams.Count();++i)
		//	{
		//		int madong = km2.Madongsanphams.ElementAt(i).Madongsanpham;
		//		km2.Madongsanphams.ElementAt(i).Sanphams = context.Sanphams.Where(x=>x.Madongsanpham== madong).ToList();
		//	}
		//	Debug.WriteLine("Dong sp: ", km2.Madongsanphams);
		//	return km2;
		//}

		public int GetKmProductToday(Dongsanpham dsp)
		{
			DateTime today = DateTime.Now.Date;
			Khuyenmai km2 = context.Khuyenmais.Include(k => k.Madongsanphams)// Include Dongsanpham entities related to Khuyenmai
				.ThenInclude(d => d.Makms) // Include Khuyenmai entities related to Dongsanpham
				.FirstOrDefault(x => x.Ngaybd <= today && today < x.Ngaykt && x.Madongsanphams.Contains(dsp));
			
			if(km2 != null)
			{
				return km2.Phantramgiam;
			}
			return 1;
		}
	}
}
