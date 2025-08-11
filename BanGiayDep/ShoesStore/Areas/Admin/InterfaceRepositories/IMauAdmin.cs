using ShoesStore.Models;

namespace ShoesStore.Areas.Admin.InterfaceRepositories
{
    public interface IMauAdmin
    {
        Mau GetColorsById(string id);
        IQueryable<Mau> GetAllColors();
        void AddColors(Mau color);
        void UpdateColors(Mau color, string id);
        void DeleteColors(string Id);
        List<string> GetDistinctColors();
        List<string> GetAllIdMau();
        List<Mau> GetAllMauNotUsedForSp(int madongsp);
    }
}
