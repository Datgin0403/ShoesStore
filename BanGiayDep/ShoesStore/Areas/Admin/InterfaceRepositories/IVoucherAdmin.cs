using ShoesStore.Models;
using System.Collections.Generic;

namespace ShoesStore.Areas.Admin.InterfaceRepositories
{
	public interface IVoucherAdmin
	{
		List<Voucher> GetAllVouchers();
		Voucher GetVoucherById(string id);
		void AddVoucher(Voucher voucher);
		void UpdateVoucher(Voucher voucher);
		void DeleteVoucher(string id);
	}
}
