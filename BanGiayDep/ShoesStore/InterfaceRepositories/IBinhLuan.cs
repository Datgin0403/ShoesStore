using ShoesStore.Models;
using ShoesStore.ViewModels;

namespace ShoesStore.InterfaceRepositories
{
	public interface IBinhLuan
	{
		void AddBinhLuan(Binhluan bl);
		CommentViewModel GetBlList(int masp,int page=1);
	}
}
