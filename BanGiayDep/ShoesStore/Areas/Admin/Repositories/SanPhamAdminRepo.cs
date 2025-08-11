using Microsoft.EntityFrameworkCore;
using ShoesStore.Areas.Admin.InterfaceRepositories;
using ShoesStore.Models;

namespace ShoesStore.Areas.Admin.Repositories
{
	public class SanPhamAdminRepo : ISanPhamAdmin
	{
		ShoesDbContext context;
		public SanPhamAdminRepo(ShoesDbContext _context)
		{
			this.context = _context;
		}

		public List<Sanpham> GetCTSPList(int Madongsp)
		{
			List<Sanpham> ctSp = context.Sanphams.Where(x => x.Madongsanpham == Madongsp).Select(x => new Sanpham
			{
				Madongsanpham = x.Madongsanpham,
				Mamau = x.Mamau,
				Masp = x.Masp,
				Anhdaidien = x.Anhdaidien,
				MamauNavigation = context.Maus.FirstOrDefault(m => m.Mamau == x.Mamau),
				MadongsanphamNavigation = context.Dongsanphams.FirstOrDefault(sp => sp.Madongsanpham == x.Madongsanpham),
				Sanphamsizes = context.Sanphamsizes.Where(tkho => tkho.Masp == x.Masp).ToList()
			}).ToList();
			return ctSp;
		}
		public void AddChitietSp(Sanpham ctSp)
		{
			context.Sanphams.Add(ctSp);
			context.SaveChanges();
			return;
		}

		public Sanpham GetChitietSpById(int masp)
		{
			Sanpham ctsp = context.Sanphams.FirstOrDefault(x => x.Masp == masp);
			ctsp.MamauNavigation = context.Maus.FirstOrDefault(x => x.Mamau == ctsp.Mamau);
			ctsp.Sanphamsizes = context.Sanphamsizes.Where(x => x.Masp == ctsp.Masp).ToList();
			ctsp.MadongsanphamNavigation = context.Dongsanphams.FirstOrDefault(x => x.Madongsanpham == ctsp.Madongsanpham);
			return ctsp;
		}

		public void DeleteChitietSp(int masp)
		{
			Sanpham ctsp = context.Sanphams.FirstOrDefault(x => x.Masp == masp);
			context.Sanphams.Remove(ctsp);
			context.SaveChanges();
		}

        public void UpdateChitietSp(Sanpham sp)
		{
			context.Sanphams.Update(sp);
			context.SaveChanges();
		}
    }
}
