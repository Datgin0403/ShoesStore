using Microsoft.AspNetCore.Mvc;
using ShoesStore.Models;

namespace ShoesStore.Areas.Admin.InterfaceRepositories
{
    public interface IDongsanphamAdmin
    {
        Dongsanpham GetDongsanphamById(int id);
        IQueryable<Dongsanpham> GetAllDongsanpham();
        void AddDongsanpham(Dongsanpham dsp);
        void UpdateDongsanpham(Dongsanpham dsp, int id);
        void DeleteDongsanpham(int Id);
        List<string> GetDistinctDongsanpham();
    }
}
