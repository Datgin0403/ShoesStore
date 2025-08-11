using Microsoft.EntityFrameworkCore;
using ShoesStore.InterfaceRepositories;
using ShoesStore.Models;
using ShoesStore.ViewModels;

namespace ShoesStore.Repositories
{
    public class PhieuMuaRepo : IPhieuMua
    {
        ShoesDbContext context;
        public PhieuMuaRepo(ShoesDbContext context)
        {
            this.context = context;
        }

        public int AddPhieuMua(PhieuMuaViewModel phieuMua)
        {
            // Kiểm tra các trường bắt buộc
            if (!phieuMua.maTinh.HasValue || !phieuMua.maQuan.HasValue || !phieuMua.maPhuong.HasValue)
            {
                throw new ArgumentException("Vui lòng chọn đầy đủ Tỉnh, Quận, Phường.");
            }

            // Lấy thông tin tỉnh, quận, phường
            var tinh = context.Tinhs.FirstOrDefault(x => x.Matinh == phieuMua.maTinh);
            var quan = context.Quans.FirstOrDefault(x => x.Maquan == phieuMua.maQuan);
            var phuong = context.Phuongs.FirstOrDefault(x => x.Maphuong == phieuMua.maPhuong);

            // Kiểm tra null
            if (tinh == null || quan == null || phuong == null)
            {
                throw new ArgumentException("Thông tin Tỉnh, Quận hoặc Phường không hợp lệ.");
            }

            string tentinh = tinh.Tentinh;
            string tenquan = quan.Tenquan;
            string tenphuong = phuong.Tenphuong;

            // Xử lý khách hàng
            if (phieuMua.khInfo != null)
            {
                Khachhang kh = context.Khachhangs.FirstOrDefault(x => x.Makh == phieuMua.khInfo.Makh);
                if (kh != null)
                {
                    kh.Tongxu -= phieuMua.coinApply;
                    context.Khachhangs.Update(kh);
                    context.SaveChanges();
                }
            }

            // Xử lý voucher
            if (phieuMua.Choosenvoucher?.Mavoucher != null)
            {
                Voucher vc = context.Vouchers.FirstOrDefault(x => x.Mavoucher == phieuMua.Choosenvoucher.Mavoucher);
                if (vc != null)
                {
                    vc.Soluong -= 1;
                    context.Vouchers.Update(vc);
                    context.SaveChanges();
                }
            }

            // Tạo địa chỉ
            string Diachi = $"{phieuMua.Diachi}, {tentinh}, {tenquan}, {tenphuong}";

            // Tạo phiếu mua
            Phieumua newpm = new Phieumua()
            {
                Ghichu = phieuMua.GhiChu,
                Makh = phieuMua.khInfo?.Makh,
                Ngaydat = DateTime.Now,
                Tinhtrang = "Pending",
                Mapttt = phieuMua.Mapttt,
                MaptttNavigation = context.Phuongthucthanhtoans.Find(phieuMua.Mapttt),
                Tongtien = phieuMua.totalCost,
                Tennguoinhan = phieuMua.HoTen,
                Sdtnguoinhan = phieuMua.Sdt,
                Emailnguoinhan = phieuMua.Email,
                Diachinguoinhan = Diachi,
                Mavoucher = phieuMua.Choosenvoucher?.Mavoucher
            };

            context.Phieumuas.Add(newpm);
            context.SaveChanges();

            // Tạo chi tiết phiếu mua
            List<Chitietphieumua> ctpmList = new List<Chitietphieumua>();
            foreach (ShoppingCartItem cartItem in phieuMua.listcartItem)
            {
                ctpmList.Add(new Chitietphieumua()
                {
                    Mapm = newpm.Mapm,
                    Maspsize = cartItem.Maspsize,
                    Soluong = cartItem.Quantity,
                    Dongia = cartItem.PhanTramGiam > 1 ? (cartItem.GiaGoc - cartItem.GiaGoc * cartItem.PhanTramGiam / 100) : cartItem.GiaGoc,
                });
            }

            context.Chitietphieumuas.AddRange(ctpmList);
            context.SaveChanges();
            return newpm.Mapm;
        }
        public List<Phieumua> GetOrderHistoryByEmail(string email)
        {
            var khachhang = context.Khachhangs.FirstOrDefault(kh => kh.Email == email);
            if (khachhang == null) return new List<Phieumua>();
            return context.Phieumuas.Where(pm => pm.Makh == khachhang.Makh)
                .Include(x=>x.MavoucherNavigation)
                .Include(p => p.Chitietphieumuas)
                .ThenInclude(c => c.MaspsizeNavigation)
                .ThenInclude(s => s.MaspNavigation)
                .ThenInclude(s => s.MadongsanphamNavigation)
                .Include(p => p.Chitietphieumuas)
                .ThenInclude(c => c.MaspsizeNavigation)
                .ThenInclude(s => s.MasizeNavigation)
                .Include(p => p.Chitietphieumuas)
                .ThenInclude(c => c.MaspsizeNavigation)
                .ThenInclude(s => s.MaspNavigation)
                .ThenInclude(s => s.MamauNavigation).OrderByDescending(x=>x.Mapm).ToList();
        }
        public Phieumua GetOrderById(int id)
        {
            return context.Phieumuas
                .Include(p => p.Chitietphieumuas)
                .ThenInclude(c => c.MaspsizeNavigation)
                .ThenInclude(s => s.MaspNavigation)
                .ThenInclude(s => s.MadongsanphamNavigation)
                .Include(p => p.Chitietphieumuas)
                .ThenInclude(c => c.MaspsizeNavigation)
                .ThenInclude(s => s.MasizeNavigation)
                .Include(p => p.Chitietphieumuas)
                .ThenInclude(c => c.MaspsizeNavigation)
                .ThenInclude(s => s.MaspNavigation)
                .ThenInclude(s => s.MamauNavigation)
                .FirstOrDefault(p => p.Mapm == id);

        }
    }
}
