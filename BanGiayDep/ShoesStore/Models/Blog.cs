using System;
using System.Collections.Generic;

namespace ShoesStore.Models;

public partial class Blog
{
    public int Mablog { get; set; }

    public int Manv { get; set; }

    public string? Noidung { get; set; }

    public string? Theloai { get; set; }

    public string? Anhdaidien { get; set; }

    public virtual Nhanvien ManvNavigation { get; set; } = null!;
}
