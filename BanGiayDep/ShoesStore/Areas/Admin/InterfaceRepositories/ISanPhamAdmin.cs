using ShoesStore.Models;

namespace ShoesStore.Areas.Admin.InterfaceRepositories
{
	public interface ISanPhamAdmin
	{
		List<Sanpham> GetCTSPList(int Madongsp);
		void AddChitietSp(Sanpham ctSp);
		Sanpham GetChitietSpById(int masp);
		void DeleteChitietSp(int masp);
		void UpdateChitietSp(Sanpham sp);
	}
}
