using ShoesStore.Models;

namespace ShoesStore.InterfaceRepositories
{
    public interface IVoucher
    {
        List<Voucher> getAllVoucherToday();
        Voucher GetVoucherByCode(string id);
    }
}
