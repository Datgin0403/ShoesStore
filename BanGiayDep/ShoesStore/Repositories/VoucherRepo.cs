using ShoesStore.InterfaceRepositories;
using ShoesStore.Models;

namespace ShoesStore.Repositories
{
    public class VoucherRepo : IVoucher
    {
        ShoesDbContext context;
        public VoucherRepo(ShoesDbContext context)
        {
            this.context = context;
        }

        public List<Voucher> getAllVoucherToday()
        {
            DateTime today = DateTime.Now.Date;
            List<Voucher> AllVoucherToday = context.Vouchers.Where(x => x.Soluong > 0 && today >= x.Ngaytao.Date && today <= x.Ngayhethan).ToList();
            return AllVoucherToday;
        }

        public Voucher GetVoucherByCode(string id)
        {
            return context.Vouchers.FirstOrDefault(x => x.Mavoucher == id);
        }
    }
}
