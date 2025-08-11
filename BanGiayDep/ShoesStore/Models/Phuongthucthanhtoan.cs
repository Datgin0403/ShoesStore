using System;
using System.Collections.Generic;

namespace ShoesStore.Models;

public partial class Phuongthucthanhtoan
{
    public int Mapttt { get; set; }

    public string Tenphuongthuc { get; set; } = null!;

    public virtual ICollection<Phieumua> Phieumuas { get; set; } = new List<Phieumua>();
}
