using Microsoft.AspNetCore.Mvc;
using ShoesStore.Areas.Admin.ViewModels;
using ShoesStore.Models;
using ShoesStore.Models.Authentication;
using System.Diagnostics;
using System.Linq;

namespace ShoesStore.Areas.Admin.Controllers
{
	[Area("Admin")]
    [AuthenticationAdmin]

    public class StaffAdminController : Controller
	{
		private readonly ShoesDbContext _db;


		public StaffAdminController(ShoesDbContext db)
		{
			_db = db;
		}
		[HttpGet]
		public IActionResult ListEmployees()
		{
			// Lấy danh sách tất cả nhân viên từ cơ sở dữ liệu
			//var employees = _db.Nhanviens.ToList();

			return View(_db.Nhanviens.ToList());
		}
		public IActionResult CreateEmployee()
		{
			return View();
		}

		[HttpPost]
		public IActionResult CreateEmployee(RegisterAdminViewModel rgAdmin)
		{
			Taikhoan newTk = new Taikhoan
			{
				Email = rgAdmin.Nhanvien.Email,
				Matkhau = rgAdmin.Taikhoan.Matkhau,
				Loaitk = 1
			};
			rgAdmin.Taikhoan = newTk;
			rgAdmin.Nhanvien.EmailNavigation.Email = rgAdmin.Nhanvien.Email;
			rgAdmin.Nhanvien.EmailNavigation.Loaitk = 1;

            rgAdmin.Nhanvien.Diachi = rgAdmin.Nhanvien.Diachi == "123" ? "" : rgAdmin.Nhanvien.Diachi;
			//If use modelstate.isvalid need to make input for emailnavigation in create view
			//But we have already use view model

			// Kiểm tra xem đã có tài khoản với email này chưa
			var existingEmployee = _db.Taikhoans.FirstOrDefault(c => c.Email == rgAdmin.Taikhoan.Email);

			if (existingEmployee != null)
			{
				// Trả về thông báo lỗi nếu email đã tồn tại
				ModelState.AddModelError("Nhanvien.Email", "Email already exists.");
				return View(rgAdmin);
			}
			if (!ModelState.IsValid)
			{
				return View(rgAdmin);
			}

			_db.Nhanviens.Add(rgAdmin.Nhanvien);
			_db.SaveChanges();

			// Chuyển hướng đến trang quản lý nhân viên hoặc trang chính của ứng dụng
			return RedirectToAction("ListEmployees");
		}
		// Cập nhật nhân viên (Hiển thị form cập nhật)
		[HttpGet]
		public IActionResult EditEmployee(int id)
		{
			//Check xem email có phải email đã dùng chưa , nếu là email cũ của chính nhân viên đó thì accept
			//Còn email trùng với email của nhân viên khác thì từ chối
			//Chỉnh email bên nhân viên thì cũng chỉnh email bên tài khoản
			// Tìm nhân viên cần cập nhật trong cơ sở dữ liệu
			var employee = _db.Nhanviens.Find(id);
			employee.EmailNavigation = _db.Taikhoans.Find(employee.Email);

			if (employee == null)
			{
				return NotFound();
			}

			return View(employee);
		}

		// Xử lý cập nhật nhân viên (POST)
		[HttpPost]
		public IActionResult EditEmployee(Nhanvien employee)
		{
			if (ModelState.IsValid)
			{
				_db.Nhanviens.Update(employee);
				_db.SaveChanges();

				return RedirectToAction("ListEmployees");
			}

			return View(employee);
		}

		// Xóa nhân viên
		[HttpPost]
		public IActionResult DeleteEmployee(int id)
		{
			//Thêm filed kích hoạt tài khoản 
			//Nếu nhân viên nghỉ thì không kích hoạt tài khoản

			var employee = _db.Nhanviens.Find(id);

			if (employee == null)
			{
				return NotFound();
			}

			_db.Nhanviens.Remove(employee);
			_db.SaveChanges();

			return RedirectToAction("ListEmployees");
		}
	}
}
