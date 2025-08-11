using ShoesStore.Models;

namespace ShoesStore.Areas.Admin.ViewModels
{
    public class RegisterAdminViewModel
    {
        public Nhanvien Nhanvien { get; set; } = new Nhanvien();
        public Taikhoan Taikhoan { get; set; } = new Taikhoan();
    }
}
