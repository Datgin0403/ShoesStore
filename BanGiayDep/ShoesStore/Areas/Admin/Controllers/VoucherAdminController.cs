using Microsoft.AspNetCore.Mvc;
using ShoesStore.Areas.Admin.InterfaceRepositories;
using ShoesStore.Models;
using ShoesStore.Models.Authentication;
using System.Collections.Generic;

namespace ShoesStore.Areas.Admin.Controllers
{
	[Area("Admin")]
    [AuthenticationM_S]
    public class VoucherAdminController : Controller
	{
		private readonly IVoucherAdmin voucherRepo;

		public VoucherAdminController(IVoucherAdmin voucherRepo)
		{
			this.voucherRepo = voucherRepo;
		}

		public IActionResult Index()
		{
			List<Voucher> vouchers = voucherRepo.GetAllVouchers();
			return View(vouchers);
		}

		

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Voucher voucher)
		{
			if (ModelState.IsValid)
			{
				voucherRepo.AddVoucher(voucher);
				return RedirectToAction(nameof(Index));
			}
			return View(voucher);
		}

		public IActionResult Edit(string id)
		{
			Voucher voucher = voucherRepo.GetVoucherById(id);
			if (voucher == null)
			{
				return NotFound();
			}
			return View(voucher);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Voucher voucher)
		{
			if (ModelState.IsValid)
			{
				voucherRepo.UpdateVoucher(voucher);
				return RedirectToAction(nameof(Index));
			}
			return View(voucher);
		}

		public IActionResult Delete(string id)
		{
			Voucher voucher = voucherRepo.GetVoucherById(id);
			if (voucher == null)
			{
				return NotFound();
			}
			return View(voucher);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(string id)
		{
			TempData["Thanhcong"] = "Delete Success";
			voucherRepo.DeleteVoucher(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
