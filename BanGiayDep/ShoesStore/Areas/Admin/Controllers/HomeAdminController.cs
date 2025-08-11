using Microsoft.AspNetCore.Mvc;
using ShoesStore.Areas.Admin.InterfaceRepositories;
using ShoesStore.Areas.Admin.ViewModels;
using ShoesStore.Models.Authentication;
using ShoesStore.Models;
using System;
using System.Linq;

namespace ShoesStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthenticationM_S]
    public class HomeAdminController : Controller
    {
        private readonly IReportRepository _reportRepository;
        private readonly ISanPhamAdmin _sanPhamAdmin;
        private readonly IPhieuMuaAdmin _phieuMuaAdmin;
        private readonly ShoesDbContext _dbContext;

        public HomeAdminController(IReportRepository reportRepository, ISanPhamAdmin sanPhamAdmin, IPhieuMuaAdmin phieuMuaAdmin, ShoesDbContext dbContext)
        {
            _reportRepository = reportRepository;
            _sanPhamAdmin = sanPhamAdmin;
            _phieuMuaAdmin = phieuMuaAdmin;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            // Lấy tháng hiện tại
            int currentMonth = DateTime.Now.Month;

            // Tổng doanh thu (tháng hiện tại)
            //decimal totalRevenue = _reportRepository.GetTotalRevenue(currentMonth);

            // Tổng sản phẩm đã bán (tháng hiện tại)
            int totalProductsSold = _reportRepository.GetTotalProductsSold(currentMonth);

            // Tổng đơn hàng (tháng hiện tại)
            int totalOrders = _phieuMuaAdmin.GetAllPhieuMua().Count();

            // Số lượng nhân viên (chỉ admin mới xem được)
            int totalEmployees = 0;
            if (HttpContext.Session.GetString("Loaitk") == "2") // Kiểm tra nếu là admin
            {
                totalEmployees = _dbContext.Nhanviens.Count();
            }

            // Số lượng khách hàng (tổng)
            int totalCustomers = _dbContext.Khachhangs.Count();

            // Truyền dữ liệu vào ViewBag để sử dụng trong view
            //ViewBag.TotalRevenue = totalRevenue;
            ViewBag.TotalProductsSold = totalProductsSold;
            ViewBag.TotalOrders = totalOrders;
            ViewBag.TotalEmployees = totalEmployees;
            ViewBag.TotalCustomers = totalCustomers;

            return View();
        }
    }
}
