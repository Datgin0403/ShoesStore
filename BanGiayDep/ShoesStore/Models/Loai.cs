using System;
using System.Collections.Generic;

namespace ShoesStore.Models;

public partial class Loai
{
    public int Maloai { get; set; }

    public string Tenloai { get; set; } = null!;

    public virtual ICollection<Dongsanpham> Dongsanphams { get; set; } = new List<Dongsanpham>();
}
