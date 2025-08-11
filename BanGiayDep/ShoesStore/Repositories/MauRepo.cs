using ShoesStore.InterfaceRepositories;
using ShoesStore.Models;

namespace ShoesStore.Repositories
{
	public class MauRepo : IMau
	{
		ShoesDbContext context;
		public MauRepo(ShoesDbContext context)
		{
			this.context = context;
		}

		public Mau GetMau(string mamau)
		{
			Mau m = context.Maus.FirstOrDefault(x => x.Mamau == mamau);
			return m;
		}

		public List<Mau> GetMauList()
		{
			return context.Maus.ToList();
		}
	}
}
