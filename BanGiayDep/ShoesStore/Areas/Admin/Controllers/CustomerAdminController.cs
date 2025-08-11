using Microsoft.AspNetCore.Mvc;
using ShoesStore.Models;
using ShoesStore.Models.Authentication;
using System.Linq;

namespace ShoesStore.Areas.Admin.Controllers
{
	[Area("Admin")]
	[AuthenticationAdmin]
	public class CustomerAdminController : Controller
	{
		private readonly ShoesDbContext _db;

		public CustomerAdminController(ShoesDbContext db)
		{
			_db = db;
		}
		public IActionResult ListCustomers()
		{
			// Lấy danh sách tất cả khách hàng từ cơ sở dữ liệu
			var customers = _db.Khachhangs.ToList();
			return View(customers);
		}

		[HttpGet]
		public IActionResult EditCustomer(int id)
		{
			// Tìm khách hàng cần cập nhật trong cơ sở dữ liệu
			var customer = _db.Khachhangs.Find(id);

			if (customer == null)
			{
				return NotFound();
			}

			return View(customer);
		}

		// Xử lý cập nhật khách hàng (POST)
		[HttpPost]
		public IActionResult EditCustomer(Khachhang customer)
		{
			if (ModelState.IsValid)
			{
				_db.Khachhangs.Update(customer);
				_db.SaveChanges();

				return RedirectToAction("ListCustomers");
			}

			return View(customer);
		}



	}
}
