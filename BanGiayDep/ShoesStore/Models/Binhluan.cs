using System;
using System.Collections.Generic;

namespace ShoesStore.Models;

public partial class Binhluan
{
    public int Mabl { get; set; }

    public string Noidungbl { get; set; } = null!;

    public DateTime Ngaybl { get; set; }

    public int Madongsanpham { get; set; }

    public int Makh { get; set; }

    public int Rating { get; set; }

    public virtual Dongsanpham MadongsanphamNavigation { get; set; } = null!;

    public virtual Khachhang MakhNavigation { get; set; } = null!;
}
