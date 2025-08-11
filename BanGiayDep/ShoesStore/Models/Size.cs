using System;
using System.Collections.Generic;

namespace ShoesStore.Models;

public partial class Size
{
    public int Masize { get; set; }

    public string Tensize { get; set; } = null!;

    public virtual ICollection<Sanphamsize> Sanphamsizes { get; set; } = new List<Sanphamsize>();
}
