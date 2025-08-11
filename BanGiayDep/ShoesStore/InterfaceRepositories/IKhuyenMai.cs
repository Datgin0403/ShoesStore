using ShoesStore.Models;

namespace ShoesStore.InterfaceRepositories
{
	public interface IKhuyenMai
	{
		DateTime getNgayktKmToday();
		List<Khuyenmai> GetAllKhuyenMaiToday(string? searchString, string? maMau, int? sortGia, decimal? minPrice, decimal? maxPrice,int Phantramgiam);
		int GetKmProductToday(Dongsanpham dsp);
	}
}
