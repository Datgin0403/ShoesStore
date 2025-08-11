using ShoesStore.Areas.Admin.InterfaceRepositories;
using ShoesStore.Models;

namespace ShoesStore.Areas.Admin.Repositories
{
	public class LoaiAdminRepo : ILoaiAdmin
	{
		ShoesDbContext _context;
		public LoaiAdminRepo(ShoesDbContext context)
		{
			this._context = context;
		}

		public Loai GetLoaiById(int id)
		{
			Loai l1 = _context.Loais.FirstOrDefault(x => x.Maloai == id);
			return l1;
		}

		public IQueryable<Loai> GetAllLoai()
		{
			var loais = _context.Loais
			.Select(loai => new Loai
			{
				Maloai = loai.Maloai,
				Tenloai = loai.Tenloai
			});

			return loais;
		}

		public void AddLoai(Loai loai)
		{
			_context.Loais.Add(loai);
			_context.SaveChanges();
		}

		public void UpdateLoai(Loai loai, int id)
		{
			var existingLoai = _context.Loais.Find(id);
			if (existingLoai != null)
			{
				existingLoai.Tenloai = loai.Tenloai;

				_context.SaveChanges();
			}
		}

		public void DeleteLoai(int Id)
		{
			Loai loai = _context.Loais
						.FirstOrDefault(l => l.Maloai == Id);

			if (loai != null)
			{
				_context.Loais.Remove(loai);
				_context.SaveChanges();
			}
		}
		public List<string> GetDistinctLoai()
		{
			return _context.Loais.Select(l => l.Tenloai).Distinct().ToList();
		}
	}
}
