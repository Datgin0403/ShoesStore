using System;
using System.Collections.Generic;

namespace ShoesStore.Models;

public partial class Phieumua
{
    public int Mapm { get; set; }

    public DateTime Ngaydat { get; set; }

    public int? Makh { get; set; }

    public int? Manv { get; set; }

    public string? Mavoucher { get; set; }

    public string Tinhtrang { get; set; } = null!;

    public int Mapttt { get; set; }

    public string? Ghichu { get; set; }

    public string? Lydohuydon { get; set; }

    public decimal? Tongtien { get; set; }

    public string Diachinguoinhan { get; set; } = null!;

    public string Emailnguoinhan { get; set; } = null!;

    public string Sdtnguoinhan { get; set; } = null!;

    public string Tennguoinhan { get; set; } = null!;

    public virtual ICollection<Chitietphieumua> Chitietphieumuas { get; set; } = new List<Chitietphieumua>();

    public virtual Khachhang? MakhNavigation { get; set; }

    public virtual Nhanvien? ManvNavigation { get; set; }

    public virtual Phuongthucthanhtoan MaptttNavigation { get; set; } = null!;

    public virtual Voucher? MavoucherNavigation { get; set; }
}
