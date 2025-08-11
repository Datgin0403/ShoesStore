using Microsoft.AspNetCore.Mvc;
using ShoesStore.InterfaceRepositories;
using ShoesStore.Models;

namespace ShoesStore.ViewComponents
{
    public class VoucherViewComponent :ViewComponent
    {
        IVoucher voucherRepo;

        public VoucherViewComponent(IVoucher voucherRepo)
        {
            this.voucherRepo = voucherRepo;
        }

        public IViewComponentResult Invoke()
        {
            List<Voucher> AllvoucherToday = voucherRepo.getAllVoucherToday();
            return View(AllvoucherToday);
        }

    }
}
