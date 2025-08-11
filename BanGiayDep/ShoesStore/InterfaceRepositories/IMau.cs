using ShoesStore.Models;

namespace ShoesStore.InterfaceRepositories
{
	public interface IMau
	{
		Mau GetMau(string mamau);
		List<Mau> GetMauList();
	}
}
