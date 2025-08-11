using Microsoft.AspNetCore.Mvc.Rendering;
using ShoesStore.Models;
using System.ComponentModel.DataAnnotations;

namespace ShoesStore.ViewModels
{
    public class PhieuMuaViewModel
    {
        public List<ShoppingCartItem> listcartItem { get; set; }
        public Khachhang? khInfo { get; set; }
        public string? GhiChu { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn phương thức thanh toán")]
        public int Mapttt { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Sdt { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string Diachi { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn tỉnh/thành phố")]
        public int? maTinh { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn quận/huyện")]
        public int? maQuan { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn phường/xã")]
        public int? maPhuong { get; set; }

        //[Required(ErrorMessage = "Vui lòng chọn địa chỉ nhận hàng")]
        //[MinLength(1, ErrorMessage = "Phải có ít nhất một địa chỉ nhận hàng")]
        public List<Sodiachi> sodiachis { get; set; }

        //[Required(ErrorMessage = "Vui lòng chọn voucher")]
        //[MinLength(1, ErrorMessage = "Phải có ít nhất một voucher")]
        public List<Voucher> voucherList { get; set; }

        //[Required(ErrorMessage = "Vui lòng chọn phường")]
        //[MinLength(1, ErrorMessage = "Phải có ít nhất một phường")]
        public List<SelectListItem> selectPhuong { get; set; }

        //[Required(ErrorMessage = "Vui lòng chọn quận")]
        //[MinLength(1, ErrorMessage = "Phải có ít nhất một quận")]
        public List<SelectListItem> selectQuan { get; set; }

       
        public Voucher? Choosenvoucher { get; set; }
        public decimal totalCost { get; set; }
        public decimal tempCost { get; set; }
        public decimal coinGet { get; set; }
        public decimal discountMoney { get; set; }
        public decimal coinChoosen { get; set; }
        public decimal coinApply { get; set; }
    }
}