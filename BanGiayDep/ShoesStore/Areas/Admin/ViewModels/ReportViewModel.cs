namespace ShoesStore.Areas.Admin.ViewModels
{
    public class ReportViewModel
    {
        public List<SalesByMonthViewModel> saleByMonths { get; set; }
        public List<SaleByProductViewModel> saleByProducts { get; set;}
    }
}
