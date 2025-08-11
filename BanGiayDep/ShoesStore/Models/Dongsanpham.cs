using System;
using System.Collections.Generic;

namespace ShoesStore.Models;

public partial class Dongsanpham
{
    public int Madongsanpham { get; set; }

    public int Maloai { get; set; }

    public string Tendongsp { get; set; } = null!;

    public decimal Giagoc { get; set; }

    public string Mota { get; set; } = null!;

    public virtual ICollection<Binhluan> Binhluans { get; set; } = new List<Binhluan>();

    public virtual Loai MaloaiNavigation { get; set; } = null!;

    public virtual ICollection<Sanpham> Sanphams { get; set; } = new List<Sanpham>();

    public virtual ICollection<Khuyenmai> Makms { get; set; } = new List<Khuyenmai>();
}
