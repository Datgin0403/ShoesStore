using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoesStore.Areas.Admin.InterfaceRepositories;
using ShoesStore.Areas.Admin.ViewModels;
using ShoesStore.Models.Authentication;

namespace ShoesStore.Controllers
{
    [Area("Admin")]
    [AuthenticationM_S]
    public class ReportController : Controller
    {
        private readonly IReportRepository _reportRepository;

        public ReportController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

       public IActionResult ChangeMonth(int month)
        {
            ReportViewModel report = new ReportViewModel();
            report.saleByMonths = _reportRepository.GetSaleByMonth();
            report.saleByProducts = _reportRepository.GetSaleByProduct(month);

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
            ViewBag.ChoosenMonth = month;

            return PartialView("_PartialViewChart", report);
        }

        public string MonthTransfer(int month)
        {
            if (month == 1)
            {
                return "January";
            }
            if (month == 2)
            {
                return "February";
            }
            if (month == 3)
            {
                return "March";
            }
            if (month == 4)
            {
                return "April";
            }
            if (month == 5)
            {
                return "May";
            }
            if (month == 6)
            {
                return "June";
            }
            if (month == 7)
            {
                return "July";
            }
            if (month == 8)
            {
                return "August";
            }
            if (month == 9)
            {
                return "September";
            }
            if (month == 10)
            {
                return "October";
            }
            if (month == 11)
            {
                return "November";
            }

            return "December";
        }
    }
}
