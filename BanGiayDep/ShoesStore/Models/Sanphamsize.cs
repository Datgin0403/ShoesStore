using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ShoesStore.Models;

public partial class Sanphamsize
{
    public int Maspsize { get; set; }

    public int Masp { get; set; }

    public int Masize { get; set; }

    public int Slton { get; set; }

    public virtual ICollection<Chitietphieumua> Chitietphieumuas { get; set; } = new List<Chitietphieumua>();
    [JsonIgnore]

    public virtual Size MasizeNavigation { get; set; } = null!;
    [JsonIgnore]

    public virtual Sanpham MaspNavigation { get; set; } = null!;
}