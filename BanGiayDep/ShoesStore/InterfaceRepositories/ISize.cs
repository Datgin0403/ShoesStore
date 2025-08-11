using ShoesStore.Models;

namespace ShoesStore.InterfaceRepositories
{
    public interface ISize
    {
        Size GetSize(int masize);
		Size GetSizeByName(string name);
	}
}
