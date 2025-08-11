using System;
using System.Collections.Generic;

namespace ShoesStore.Models;

public partial class Quan
{
    public int Maquan { get; set; }

    public string Tenquan { get; set; } = null!;

    public int Matinh { get; set; }

    public virtual Tinh MatinhNavigation { get; set; } = null!;

    public virtual ICollection<Phuong> Phuongs { get; set; } = new List<Phuong>();
}
