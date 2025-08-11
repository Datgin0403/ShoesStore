using ShoesStore.Models;

namespace ShoesStore.InterfaceRepositories
{
    public interface IKhachhang
    {
        Khachhang GetCurrentKh(string emailkh);
        void UpdateKh(Khachhang khachhang);
    }
}
