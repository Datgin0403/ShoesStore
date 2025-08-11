using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShoesStore.Areas.Admin.InterfaceRepositories;
using ShoesStore.Areas.Admin.ViewComponents;
using ShoesStore.Areas.Admin.ViewModels;
using ShoesStore.Models;

namespace ShoesStore.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly ShoesDbContext _context;

        public ReportRepository(ShoesDbContext context)
        {
            _context = context;
        }

        public List<SalesByMonthViewModel> GetSaleByMonth()
        {
            List<SalesByMonthViewModel> salebyMonth = new List<SalesByMonthViewModel>();
            int largestMonth = _context.Phieumuas.Select(x => x.Ngaydat.Month).Max();
            DateTime datenow = DateTime.Now;
            for (int i = 1; i <= largestMonth; ++i)
            {
                decimal moneyInMonth = _context.Phieumuas.Where(x => x.Tinhtrang == "Confirm" && x.Ngaydat.Month == i && x.Ngaydat.Year == datenow.Year).Sum(x => x.Tongtien) ?? 0;

                salebyMonth.Add(new SalesByMonthViewModel
                {
                    month = i,
                    sale = moneyInMonth
                });
            }

            return salebyMonth;
        }

        public List<SaleByProductViewModel> GetSaleByProduct(int month = 0)
        {
            DateTime datenow = DateTime.Now;
            if (month == 0)
            {
                month = _context.Phieumuas.Select(x => x.Ngaydat.Month).Max();
            }
            List<SaleByProductViewModel> salebyProduct = new List<SaleByProductViewModel>();
            List<int> madspInMonth = _context.Chitietphieumuas.Where(x => x.MapmNavigation.Ngaydat.Month == month && x.MapmNavigation.Ngaydat.Year == datenow.Year)
                                                   .Select(x => x.MaspsizeNavigation.MaspNavigation.Madongsanpham)
                                                   .Distinct().ToList();
            foreach (int madongsp in madspInMonth)
            {
                decimal money = _context.Chitietphieumuas.Where(x => x.MapmNavigation.Ngaydat.Month == month && x.MapmNavigation.Ngaydat.Year == datenow.Year
                                                    && x.MaspsizeNavigation.MaspNavigation.Madongsanpham == madongsp).Sum(x => x.Dongia * x.Soluong);

                Dongsanpham dsp = _context.Dongsanphams.Find(madongsp);
                Sanpham sp = _context.Sanphams.FirstOrDefault(x => x.Madongsanpham == madongsp);

                salebyProduct.Add(new SaleByProductViewModel
                {
                    ProductName = dsp.Tendongsp,
                    Sales = money,
                });

            }
            salebyProduct = salebyProduct.OrderByDescending(x => x.Sales).Take(5).ToList();
            return salebyProduct;

        }
        public decimal GetTotalRevenue(int month)
        {
            // Implement logic to calculate total revenue for the given month
            // For example:
            var totalRevenue = _context.Phieumuas
                .Where(pm => pm.Ngaydat.Month == month && pm.Tinhtrang == "Confirm")
                .Sum(pm => (decimal?)pm.Tongtien) ?? 0; // Provide default value if null
            return totalRevenue;
        }


        public int GetTotalProductsSold(int month)
        {
            // Implement logic to calculate total products sold for the given month
            // For example:
            var totalProductsSold = _context.Chitietphieumuas
                .Where(ctpm => ctpm.MapmNavigation.Ngaydat.Month == month)
                .Sum(ctpm => ctpm.Soluong);
            return totalProductsSold;
        }


    }
}
