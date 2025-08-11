using Microsoft.AspNetCore.Mvc;
using ShoesStore.Models;

namespace ShoesStore.Areas.Admin.InterfaceRepositories
{
    public interface IKhuyenMaiAdmin 
    {
        Khuyenmai GetKhuyenmaiById(int id);
        IQueryable<Khuyenmai> GetAllKhuyenmai();
        void AddKhuyenmai(Khuyenmai km);
        void DeleteKhuyenmai(int Id);
    }
}
