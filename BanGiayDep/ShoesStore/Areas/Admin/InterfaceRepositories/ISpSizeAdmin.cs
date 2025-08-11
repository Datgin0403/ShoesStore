using ShoesStore.Models;

namespace ShoesStore.Areas.Admin.InterfaceRepositories
{
    public interface ISpSizeAdmin
    {
        void AddListTonKho(List<Sanphamsize> tkhoList);
        void DeleteTonKhoListByCtSp(int idctsp);
        void UpdateListSpSize(List<Sanphamsize> spSize);
    }
}
