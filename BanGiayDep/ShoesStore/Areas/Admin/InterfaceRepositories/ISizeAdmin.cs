

using ShoesStore.Models;

namespace ShoesStore.Areas.Admin.InterfaceRepositories
{
    public interface ISizeAdmin
    {
        List<string> GetSizeNameList();
        List<int> GetMasizeList();

        Size GetSizesById(int id);
        IQueryable<Size> GetAllSizes();
        void AddSizes(Size size);
        void UpdateSizes(Size size, int id);
        void DeleteSizes(int Id);
        List<string> GetDistinctSizes();
    }
}
