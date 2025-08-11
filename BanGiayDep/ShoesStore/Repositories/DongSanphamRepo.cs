using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesStore.InterfaceRepositories;
using ShoesStore.Models;
using ShoesStore.ViewModels;
using System.Diagnostics;

namespace ShoesStore.Repositories
{
    public class DongSanphamRepo : IDongSanpham
    {
        ShoesDbContext context;
        public DongSanphamRepo(ShoesDbContext context)
        {
            this.context = context;
        }

        public Dongsanpham GetDongSanpham(int madong)
        {
            Dongsanpham dongsp = context.Dongsanphams.FirstOrDefault(x => x.Madongsanpham == madong);
            return dongsp;
        }

        public List<DongsanphamViewModel> GetDongspView(string? maMau,int? sortGia,string? searchString, decimal? minPrice, decimal? maxPrice,int maLoai)
        {
			List<Dongsanpham> dongsp;

			if(maMau!= null)
			{
				dongsp = context.Dongsanphams.Select(x => new Dongsanpham
				{
					Madongsanpham = x.Madongsanpham,
					Maloai = x.Maloai,
					Tendongsp = x.Tendongsp,
					Mota = x.Mota,
					Giagoc = x.Giagoc,
					Sanphams = context.Sanphams.Where(a => a.Madongsanpham == x.Madongsanpham && a.Mamau==maMau).ToList(),
					MaloaiNavigation = context.Loais.FirstOrDefault(a => a.Maloai == x.Maloai)
				}).ToList();
			}
			else
			{
				dongsp = context.Dongsanphams
					.Select(x => new Dongsanpham
					{
						Madongsanpham = x.Madongsanpham,
						Maloai = x.Maloai,
						Tendongsp = x.Tendongsp,
						Mota = x.Mota,
						Giagoc = x.Giagoc,
						Sanphams = context.Sanphams.Where(a => a.Madongsanpham == x.Madongsanpham).ToList(),
						MaloaiNavigation = context.Loais.FirstOrDefault(a => a.Maloai == x.Maloai)
					}).ToList();
			}
			dongsp = dongsp.Where(x=>x.Sanphams.Count()!=0 ).ToList();

			if (maLoai >= 1)
			{
				dongsp = dongsp.Where(x => x.Maloai == maLoai).ToList();
			}
			
			DateTime today = DateTime.Now.Date;
			List<Khuyenmai> khuyenmais = context.Khuyenmais.Include(x => x.Madongsanphams)
												.ThenInclude(d => d.Makms)
												.Where(x => x.Ngaybd <= today && today < x.Ngaykt).ToList();

			List<DongsanphamViewModel> dspView = new List<DongsanphamViewModel>();
		
			foreach (Dongsanpham dsp in dongsp)
			{
				int percent = 0;
				foreach (Khuyenmai dspKm in khuyenmais)
				{
					if (dspKm.Madongsanphams.Any(x=>x.Madongsanpham == dsp.Madongsanpham))
					{
						percent = dspKm.Phantramgiam;
					}
				}

				dspView.Add(new DongsanphamViewModel
				{
					dongsp = dsp,
					Phantramgiam = percent,
				}) ;
				//Debug.WriteLine("Phan tram: "+percent);
			}
			if (sortGia != null && sortGia != 0)
			{

				if (sortGia == 1)
				{
					dspView = dspView.OrderBy(x => x.dongsp.Giagoc-(x.dongsp.Giagoc*x.Phantramgiam/100)).ToList();
				}
				if (sortGia == 2)
				{
					dspView = dspView.OrderByDescending(x => x.dongsp.Giagoc - (x.dongsp.Giagoc * x.Phantramgiam / 100)).ToList();
				}

			}
			if (minPrice != null && maxPrice != 0 && maxPrice != null)
			{
				dspView = dspView.Where(x => x.dongsp.Giagoc - (x.dongsp.Giagoc * x.Phantramgiam / 100) >= minPrice && 
						x.dongsp.Giagoc - (x.dongsp.Giagoc * x.Phantramgiam / 100) <= maxPrice).ToList();
			}

			if (!string.IsNullOrEmpty(searchString))
			{
				dspView = dspView.Where(x => x.dongsp.Tendongsp.ToLower().Contains(searchString.ToLower())).ToList();
			}

			return dspView;
		}
	}
}
