using ShoesStore.Models;
using ShoesStore.ViewModels;

namespace ShoesStore.InterfaceRepositories
{
	public interface ISanphamSize
	{
		Sanphamsize GetSLTon(int masp, int masize);
		 void MinusSanPhamSize(PhieuMuaViewModel pm);
		int GetMaspsize(int masp, string tensize);
	}
}
