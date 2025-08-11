using System;
using System.Collections.Generic;

namespace ShoesStore.Models;

public partial class Phuong
{
    public int Maphuong { get; set; }

    public string Tenphuong { get; set; } = null!;

    public int Maquan { get; set; }

    public virtual Quan MaquanNavigation { get; set; } = null!;
}
