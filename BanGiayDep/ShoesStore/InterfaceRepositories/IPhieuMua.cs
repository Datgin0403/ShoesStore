using ShoesStore.Models;
using ShoesStore.ViewModels;

namespace ShoesStore.InterfaceRepositories
{
    public interface IPhieuMua
    {
        int AddPhieuMua(PhieuMuaViewModel phieuMua);
        List<Phieumua> GetOrderHistoryByEmail(string email);
        Phieumua GetOrderById(int id);
    }
}
