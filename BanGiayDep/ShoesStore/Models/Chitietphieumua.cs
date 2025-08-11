using System;
using System.Collections.Generic;

namespace ShoesStore.Models;

public partial class Chitietphieumua
{
    public int Mapm { get; set; }

    public int Maspsize { get; set; }

    public int Soluong { get; set; }

    public decimal Dongia { get; set; }

    public virtual Phieumua MapmNavigation { get; set; } = null!;

    public virtual Sanphamsize MaspsizeNavigation { get; set; } = null!;
}
