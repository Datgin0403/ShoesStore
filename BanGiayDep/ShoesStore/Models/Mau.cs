using System;
using System.Collections.Generic;

namespace ShoesStore.Models;

public partial class Mau
{
    public string Mamau { get; set; } = null!;

    public string? Tenmau { get; set; }

    public virtual ICollection<Sanpham> Sanphams { get; set; } = new List<Sanpham>();
}
