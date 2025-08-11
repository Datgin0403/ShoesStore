using System.Collections.Generic;
using ShoesStore.Areas.Admin.ViewComponents;
using ShoesStore.Areas.Admin.ViewModels;

namespace ShoesStore.Areas.Admin.InterfaceRepositories
{
    public interface IReportRepository
    {
        List<SalesByMonthViewModel> GetSaleByMonth();
        List<SaleByProductViewModel> GetSaleByProduct(int month = 0);
        decimal GetTotalRevenue(int month);
        int GetTotalProductsSold(int month);
    }
}
