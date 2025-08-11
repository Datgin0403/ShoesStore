using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShoesStore.Areas.Admin.InterfaceRepositories;
using ShoesStore.Areas.Admin.ViewModels;

namespace ShoesStore.Areas.Admin.ViewComponents
{
	public class SaleChart : ViewComponent
	{
		IReportRepository reportRepo;
		public SaleChart(IReportRepository reportRepo)
		{
			this.reportRepo = reportRepo;
		}

		public IViewComponentResult Invoke()
		{
			ReportViewModel report = new ReportViewModel();
			report.saleByMonths = reportRepo.GetSaleByMonth();
			report.saleByProducts = reportRepo.GetSaleByProduct();

            int maxMonth = report.saleByMonths.Select(x => x.month).Max();

            List<SelectListItem> listMonth = new List<SelectListItem>();
            for (int i = 1; i <= maxMonth; ++i)
            {
                listMonth.Add(new SelectListItem
                {
                    Value = i.ToString(),
                    Text = MonthTransfer(i)
                });
            }

            ViewBag.MonthList = listMonth;
            ViewBag.ChoosenMonth = maxMonth;
            return View(report);
		}

		public string MonthTransfer(int month)
		{
			if (month == 1)
			{
				return "Tháng 1";
			}
			if(month == 2)
			{
                return "Tháng 2";
            }
            if (month == 3)
            {
                return "Tháng 3";
            }
            if (month == 4)
            {
                return "Tháng 4";
            }
            if (month == 5)
            {
                return "Tháng 5";
            }
            if (month == 6)
            {
                return "Tháng 6";
            }
            if (month == 7)
            {
                return "Tháng 7";
            }
            if (month == 8)
            {
                return "Tháng 8";
            }
            if (month == 9)
            {
                return "Tháng 9";
            }
            if (month == 10)
            {
                return "Tháng 10";
            }
            if (month == 11)
            {
                return "Tháng 11";
            }

            return "Tháng 12";
        }
	}
}
