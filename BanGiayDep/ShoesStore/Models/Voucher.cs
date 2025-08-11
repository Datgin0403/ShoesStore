using System;
using System.Collections.Generic;

namespace ShoesStore.Models;

public partial class Voucher
{
    public string Mavoucher { get; set; } = null!;

    public int Soluong { get; set; }

    public decimal Giatoithieu { get; set; }

    public decimal Giamtoida { get; set; }

    public DateTime Ngaytao { get; set; }

    public DateTime Ngayhethan { get; set; }

    public virtual ICollection<Phieumua> Phieumuas { get; set; } = new List<Phieumua>();
}
