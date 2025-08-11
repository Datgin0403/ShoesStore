using System;
using System.Collections.Generic;

namespace ShoesStore.Models;

public partial class Nhanvien
{
    public int Manv { get; set; }

    public string Tennv { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Diachi { get; set; }

    public string Sdt { get; set; } = null!;

    public bool Gioitinh { get; set; }

    public DateTime? Ngaysinh { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual Taikhoan EmailNavigation { get; set; } = null!;

    public virtual ICollection<Phieumua> Phieumuas { get; set; } = new List<Phieumua>();
}
