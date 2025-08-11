using Microsoft.AspNetCore.Mvc;
using ShoesStore.InterfaceRepositories;
using ShoesStore.Models;

namespace ShoesStore.Areas.Admin.InterfaceRepositories
{
    public interface IPhieuMuaAdmin 
    {
        Phieumua GetPhieuMuaById(int id);
        IQueryable<Phieumua> GetAllPhieuMua();
        void UpdatePhieuMua(Phieumua pm, int id,string oldState);
       // void DeletePhieuMua(int Id);
    }
}
