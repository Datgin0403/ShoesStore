using ShoesStore.Areas.Admin.InterfaceRepositories;
using ShoesStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShoesStore.Areas.Admin.Repositories
{
	public class VoucherAdminRepo : IVoucherAdmin
	{
		private readonly ShoesDbContext context;

		public VoucherAdminRepo(ShoesDbContext context)
		{
			this.context = context;
		}

		public List<Voucher> GetAllVouchers()
		{
			return context.Vouchers.ToList();
		}

		public Voucher GetVoucherById(string id)
		{
			return context.Vouchers.FirstOrDefault(v => v.Mavoucher == id);
		}

		public void AddVoucher(Voucher voucher)
		{
			context.Vouchers.Add(voucher);
			context.SaveChanges();
		}

		public void UpdateVoucher(Voucher voucher)
		{
			context.Vouchers.Update(voucher);
			context.SaveChanges();
		}

		public void DeleteVoucher(string id)
		{
			var voucher = context.Vouchers.FirstOrDefault(v => v.Mavoucher == id);
			if (voucher != null)
			{
				context.Vouchers.Remove(voucher);
				context.SaveChanges();
			}
		}
	}
}
