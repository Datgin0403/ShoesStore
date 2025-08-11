using System;
using System.Collections.Generic;

namespace ShoesStore.Models;

public partial class Khuyenmai
{
    public int Makm { get; set; }

    public DateTime Ngaybd { get; set; }

    public DateTime Ngaykt { get; set; }

    public int Phantramgiam { get; set; }

    public virtual ICollection<Dongsanpham> Madongsanphams { get; set; } = new List<Dongsanpham>();
}
