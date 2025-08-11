using ShoesStore.Models;

namespace ShoesStore.Areas.Admin.InterfaceRepositories
{
	public interface ILoaiAdmin
	{
		Loai GetLoaiById(int id);
		IQueryable<Loai> GetAllLoai();
		void AddLoai(Loai loai);
		void UpdateLoai(Loai loai, int id);
		void DeleteLoai(int Id);
		List<string> GetDistinctLoai();
	}
}
