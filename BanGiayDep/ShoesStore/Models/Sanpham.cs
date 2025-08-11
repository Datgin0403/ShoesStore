using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ShoesStore.Models;

public partial class Sanpham
{
    public int Masp { get; set; }

    public int Madongsanpham { get; set; }

    public string Mamau { get; set; } = null!;

    public string Anhdaidien { get; set; } = null!;

    public string Anhmattren { get; set; } = null!;

    public string Anhdegiay { get; set; } = null!;

    public string? Video { get; set; }

    [JsonIgnore]
    public virtual Dongsanpham MadongsanphamNavigation { get; set; } = null!;
    [JsonIgnore]

    public virtual Mau MamauNavigation { get; set; } = null!;

    public virtual ICollection<Sanphamsize> Sanphamsizes { get; set; } = new List<Sanphamsize>();
    public enum TrangThaiEnum
    {
        DangBan = 1,
        NgungBan = 2,
        Hot = 3,
        New = 4
    }

    public TrangThaiEnum TrangThai { get; set; }
}
