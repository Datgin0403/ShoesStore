using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoesStore.Models;

public partial class Khachhang
{
    public int Makh { get; set; }

    [Required(ErrorMessage = "Tên khách hàng không được để trống")]
    public string Tenkh { get; set; } = null!;

    [Required(ErrorMessage = "Email không được để trống")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Số điện thoại không được để trống")]
    public string Sdt { get; set; } = null!;

    public bool? Gioitinh { get; set; }

    public DateTime? Ngaysinh { get; set; }

    public decimal Tongxu { get; set; }

    public virtual ICollection<Binhluan> Binhluans { get; set; } = new List<Binhluan>();

    public virtual Taikhoan EmailNavigation { get; set; } = null!;

    public virtual ICollection<Phieumua> Phieumuas { get; set; } = new List<Phieumua>();

    public virtual ICollection<Sodiachi> Sodiachis { get; set; } = new List<Sodiachi>();
}
