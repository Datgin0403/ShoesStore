using System;
using System.Collections.Generic;

namespace ShoesStore.Models;

public partial class Banner
{
    public int Mabanner { get; set; }

    public string Tenbanner { get; set; } = null!;

    public string Vitri { get; set; } = null!;

    public string Link { get; set; } = null!;

    public bool Hoatdong { get; set; }

    public string Slogan { get; set; } = null!;
}
