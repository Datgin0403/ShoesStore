using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace ShoesStore.Models;

public partial class Taikhoan
{
    [Required(ErrorMessage = "Email không được để trống")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Mật khẩu không được để trống")]
    public string Matkhau { get; set; } = null!;

    public int Loaitk { get; set; }

    public virtual Khachhang? Khachhang { get; set; }

    public virtual Nhanvien? Nhanvien { get; set; }
}
