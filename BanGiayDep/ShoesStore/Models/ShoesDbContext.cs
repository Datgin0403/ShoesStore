using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ShoesStore.Models;

public partial class ShoesDbContext : DbContext
{
    public ShoesDbContext()
    {
    }

    public ShoesDbContext(DbContextOptions<ShoesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Banner> Banners { get; set; }

    public virtual DbSet<Binhluan> Binhluans { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Chitietphieumua> Chitietphieumuas { get; set; }

    public virtual DbSet<Dongsanpham> Dongsanphams { get; set; }

    public virtual DbSet<Khachhang> Khachhangs { get; set; }

    public virtual DbSet<Khuyenmai> Khuyenmais { get; set; }

    public virtual DbSet<Loai> Loais { get; set; }

    public virtual DbSet<Mau> Maus { get; set; }

    public virtual DbSet<Nhanvien> Nhanviens { get; set; }

    public virtual DbSet<Phieumua> Phieumuas { get; set; }

    public virtual DbSet<Phuong> Phuongs { get; set; }

    public virtual DbSet<Phuongthucthanhtoan> Phuongthucthanhtoans { get; set; }

    public virtual DbSet<Quan> Quans { get; set; }

    public virtual DbSet<Sanpham> Sanphams { get; set; }

    public virtual DbSet<Sanphamsize> Sanphamsizes { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<Sodiachi> Sodiachis { get; set; }

    public virtual DbSet<Taikhoan> Taikhoans { get; set; }

    public virtual DbSet<Tinh> Tinhs { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=ShoesStore.mssql.somee.com;Database=ShoesStore;User Id=phucquantang_SQLLogin_1;Password=hcw2fkq9xp;TrustServerCertificate=True");//Data Source=MICHAEL;Initial Catalog=ShoesStore2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Banner>(entity =>
        {
            entity.HasKey(e => e.Mabanner);

            entity.ToTable("Banner");

            entity.Property(e => e.Mabanner).HasColumnName("MABANNER");
            entity.Property(e => e.Hoatdong).HasColumnName("HOATDONG");
            entity.Property(e => e.Link).HasColumnName("LINK");
            entity.Property(e => e.Slogan)
                .HasMaxLength(255)
                .HasDefaultValueSql("(N'')")
                .HasColumnName("SLOGAN");
            entity.Property(e => e.Tenbanner)
                .HasMaxLength(255)
                .HasColumnName("TENBANNER");
            entity.Property(e => e.Vitri).HasColumnName("VITRI");
        });

        modelBuilder.Entity<Binhluan>(entity =>
        {
            entity.HasKey(e => e.Mabl);

            entity.ToTable("BINHLUAN");

            entity.Property(e => e.Mabl).HasColumnName("MABL");
            entity.Property(e => e.Madongsanpham).HasColumnName("MADONGSANPHAM");
            entity.Property(e => e.Makh).HasColumnName("MAKH");
            entity.Property(e => e.Ngaybl)
                .HasColumnType("datetime")
                .HasColumnName("NGAYBL");
            entity.Property(e => e.Noidungbl).HasColumnName("NOIDUNGBL");
            entity.Property(e => e.Rating).HasColumnName("RATING");

            entity.HasOne(d => d.MadongsanphamNavigation).WithMany(p => p.Binhluans)
                .HasForeignKey(d => d.Madongsanpham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BINHLUAN_DONGSANPHAM");

            entity.HasOne(d => d.MakhNavigation).WithMany(p => p.Binhluans)
                .HasForeignKey(d => d.Makh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BINHLUAN_KHACHHANG");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.Mablog);

            entity.ToTable("BLOG");

            entity.HasIndex(e => e.Manv, "IX_BLOG_MANV");

            entity.Property(e => e.Mablog).HasColumnName("MABLOG");
            entity.Property(e => e.Manv).HasColumnName("MANV");
            entity.Property(e => e.Noidung).HasColumnName("NOIDUNG");
            entity.Property(e => e.Theloai)
                .HasMaxLength(255)
                .HasColumnName("THELOAI");

            entity.HasOne(d => d.ManvNavigation).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.Manv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BLOG_NHANVIEN");
        });

        modelBuilder.Entity<Chitietphieumua>(entity =>
        {
            entity.HasKey(e => new { e.Mapm, e.Maspsize });

            entity.ToTable("CHITIETPHIEUMUA");

            entity.HasIndex(e => e.Maspsize, "IX_CHITIETPHIEUMUA_IDCHITIETSP");

            entity.Property(e => e.Mapm).HasColumnName("MAPM");
            entity.Property(e => e.Maspsize).HasColumnName("MASPSIZE");
            entity.Property(e => e.Dongia)
                .HasColumnType("money")
                .HasColumnName("DONGIA");
            entity.Property(e => e.Soluong).HasColumnName("SOLUONG");

            entity.HasOne(d => d.MapmNavigation).WithMany(p => p.Chitietphieumuas)
                .HasForeignKey(d => d.Mapm)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETPHIEUMUA_PHIEUMUA");

            entity.HasOne(d => d.MaspsizeNavigation).WithMany(p => p.Chitietphieumuas)
                .HasForeignKey(d => d.Maspsize)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETPHIEUMUA_SANPHAMSIZE");
        });

        modelBuilder.Entity<Dongsanpham>(entity =>
        {
            entity.HasKey(e => e.Madongsanpham).HasName("PK__SANPHAM__B87C02A5C6299161");

            entity.ToTable("DONGSANPHAM");

            entity.HasIndex(e => e.Maloai, "IX_DONGSANPHAM_MALOAI");

            entity.Property(e => e.Madongsanpham).HasColumnName("MADONGSANPHAM");
            entity.Property(e => e.Giagoc)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("GIAGOC");
            entity.Property(e => e.Maloai).HasColumnName("MALOAI");
            entity.Property(e => e.Mota).HasColumnName("MOTA");
            entity.Property(e => e.Tendongsp)
                .HasMaxLength(255)
                .HasColumnName("TENDONGSP");

            entity.HasOne(d => d.MaloaiNavigation).WithMany(p => p.Dongsanphams)
                .HasForeignKey(d => d.Maloai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SANPHAM__IDLOAI__4222D4EF");
        });

        modelBuilder.Entity<Khachhang>(entity =>
        {
            entity.HasKey(e => e.Makh).HasName("PK__KHACHHAN__B87DC1A73C57F8C5");

            entity.ToTable("KHACHHANG");

            entity.HasIndex(e => e.Email, "IX_KHACHHANG_EMAIL").IsUnique();

            entity.Property(e => e.Makh).HasColumnName("MAKH");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Gioitinh).HasColumnName("GIOITINH");
            entity.Property(e => e.Ngaysinh)
                .HasColumnType("datetime")
                .HasColumnName("NGAYSINH");
            entity.Property(e => e.Sdt)
                .HasMaxLength(255)
                .HasColumnName("SDT");
            entity.Property(e => e.Tenkh)
                .HasMaxLength(255)
                .HasColumnName("TENKH");
            entity.Property(e => e.Tongxu)
                .HasColumnType("money")
                .HasColumnName("TONGXU");
            entity.HasOne(d => d.EmailNavigation).WithOne(p => p.Khachhang)
                .HasForeignKey<Khachhang>(d => d.Email)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__KHACHHANG__EMAIL__398D8EEE");
        });

        modelBuilder.Entity<Khuyenmai>(entity =>
        {
            entity.HasKey(e => e.Makm);

            entity.ToTable("KHUYENMAI");

            entity.Property(e => e.Makm).HasColumnName("MAKM");
            entity.Property(e => e.Ngaybd)
                .HasColumnType("datetime")
                .HasColumnName("NGAYBD");
            entity.Property(e => e.Ngaykt)
                .HasColumnType("datetime")
                .HasColumnName("NGAYKT");
            entity.Property(e => e.Phantramgiam).HasColumnName("PHANTRAMGIAM");

            entity.HasMany(d => d.Madongsanphams).WithMany(p => p.Makms)
                .UsingEntity<Dictionary<string, object>>(
                    "Chitietkhuyenmai",
                    r => r.HasOne<Dongsanpham>().WithMany()
                        .HasForeignKey("Madongsanpham")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CHITIETKHUYENMAI_DONGSANPHAM"),
                    l => l.HasOne<Khuyenmai>().WithMany()
                        .HasForeignKey("Makm")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CHITIETKHUYENMAI_KHUYENMAI"),
                    j =>
                    {
                        j.HasKey("Makm", "Madongsanpham");
                        j.ToTable("CHITIETKHUYENMAI");
                        j.HasIndex(new[] { "Madongsanpham" }, "IX_CHITIETKHUYENMAI_MADONGSANPHAM");
                        j.HasIndex(new[] { "Makm" }, "IX_CHITIETKHUYENMAI_MAKM");
                        j.IndexerProperty<int>("Makm").HasColumnName("MAKM");
                        j.IndexerProperty<int>("Madongsanpham").HasColumnName("MADONGSANPHAM");
                    });
        });

        modelBuilder.Entity<Loai>(entity =>
        {
            entity.HasKey(e => e.Maloai).HasName("PK__LOAI__0CDEF5199DDB5F30");

            entity.ToTable("LOAI");

            entity.HasIndex(e => e.Tenloai, "UQ__LOAI__BF45B996B528FC06").IsUnique();

            entity.Property(e => e.Maloai).HasColumnName("MALOAI");
            entity.Property(e => e.Tenloai)
                .HasMaxLength(255)
                .HasColumnName("TENLOAI");
        });

        modelBuilder.Entity<Mau>(entity =>
        {
            entity.HasKey(e => e.Mamau).HasName("PK__MAU__94185D28760C1F74");

            entity.ToTable("MAU");

            entity.Property(e => e.Mamau)
                .HasMaxLength(255)
                .HasColumnName("MAMAU");
            entity.Property(e => e.Tenmau)
                .HasMaxLength(255)
                .HasColumnName("TENMAU");
        });

        modelBuilder.Entity<Nhanvien>(entity =>
        {
            entity.HasKey(e => e.Manv).HasName("PK__NHANVIEN__B87DC9B274B84C76");

            entity.ToTable("NHANVIEN");

            entity.HasIndex(e => e.Email, "IX_NHANVIEN_EMAIL").IsUnique();

            entity.Property(e => e.Manv).HasColumnName("MANV");
            entity.Property(e => e.Diachi)
                .HasMaxLength(255)
                .HasColumnName("DIACHI");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Gioitinh).HasColumnName("GIOITINH");
            entity.Property(e => e.Ngaysinh)
                .HasColumnType("datetime")
                .HasColumnName("NGAYSINH");
            entity.Property(e => e.Sdt)
                .HasMaxLength(255)
                .HasColumnName("SDT");
            entity.Property(e => e.Tennv)
                .HasMaxLength(255)
                .HasColumnName("TENNV");

            entity.HasOne(d => d.EmailNavigation).WithOne(p => p.Nhanvien)
                .HasForeignKey<Nhanvien>(d => d.Email)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NHANVIEN__EMAIL__3C69FB99");
        });

        modelBuilder.Entity<Phieumua>(entity =>
        {
            entity.HasKey(e => e.Mapm).HasName("PK__PHIEUDAT__B87C5B0D30DC315E");

            entity.ToTable("PHIEUMUA");

            entity.HasIndex(e => e.Makh, "IX_PHIEUMUA_MAKH");

            entity.HasIndex(e => e.Manv, "IX_PHIEUMUA_MANV");

            entity.HasIndex(e => e.Mapttt, "IX_PHIEUMUA_MAPTTT");

            entity.Property(e => e.Mapm).HasColumnName("MAPM");
            entity.Property(e => e.Diachinguoinhan)
                .HasMaxLength(255)
                .HasDefaultValueSql("(N'')")
                .HasColumnName("DIACHINGUOINHAN");
            entity.Property(e => e.Emailnguoinhan)
                .HasMaxLength(255)
                .HasDefaultValueSql("(N'')")
                .HasColumnName("EMAILNGUOINHAN");
            entity.Property(e => e.Ghichu).HasColumnName("GHICHU");
            entity.Property(e => e.Lydohuydon)
                .HasMaxLength(255)
                .HasColumnName("LYDOHUYDON");
            entity.Property(e => e.Makh).HasColumnName("MAKH");
            entity.Property(e => e.Manv).HasColumnName("MANV");
            entity.Property(e => e.Mapttt).HasColumnName("MAPTTT");
            entity.Property(e => e.Mavoucher)
                .HasMaxLength(255)
                .HasColumnName("MAVOUCHER");
            entity.Property(e => e.Ngaydat)
                .HasColumnType("datetime")
                .HasColumnName("NGAYDAT");
            entity.Property(e => e.Sdtnguoinhan)
                .HasMaxLength(255)
                .HasDefaultValueSql("(N'')")
                .HasColumnName("SDTNGUOINHAN");
            entity.Property(e => e.Tennguoinhan)
                .HasMaxLength(255)
                .HasDefaultValueSql("(N'')")
                .HasColumnName("TENNGUOINHAN");
            entity.Property(e => e.Tinhtrang)
                .HasMaxLength(255)
                .HasColumnName("TINHTRANG");
            entity.Property(e => e.Tongtien)
                .HasColumnType("money")
                .HasColumnName("TONGTIEN");

            entity.HasOne(d => d.MakhNavigation).WithMany(p => p.Phieumuas)
                .HasForeignKey(d => d.Makh)
                .HasConstraintName("FK_PHIEUMUA_KHACHHANG");

            entity.HasOne(d => d.ManvNavigation).WithMany(p => p.Phieumuas)
                .HasForeignKey(d => d.Manv)
                .HasConstraintName("FK__PHIEUDAT__IDNV__5441852A");

            entity.HasOne(d => d.MaptttNavigation).WithMany(p => p.Phieumuas)
                .HasForeignKey(d => d.Mapttt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PHIEUMUA_PHUONGTHUCTHANHTOAN");

            entity.HasOne(d => d.MavoucherNavigation).WithMany(p => p.Phieumuas)
                .HasForeignKey(d => d.Mavoucher)
                .HasConstraintName("FK_PHIEUMUA_VOUCHER");
        });

        modelBuilder.Entity<Phuong>(entity =>
        {
            entity.HasKey(e => e.Maphuong);

            entity.ToTable("PHUONG");

            entity.HasIndex(e => e.Maquan, "IX_PHUONG_MAQUAN");

            entity.Property(e => e.Maphuong)
                .ValueGeneratedNever()
                .HasColumnName("MAPHUONG");
            entity.Property(e => e.Maquan).HasColumnName("MAQUAN");
            entity.Property(e => e.Tenphuong)
                .HasMaxLength(500)
                .HasColumnName("TENPHUONG");

            entity.HasOne(d => d.MaquanNavigation).WithMany(p => p.Phuongs)
                .HasForeignKey(d => d.Maquan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUAN_PHUONG");
        });

        modelBuilder.Entity<Phuongthucthanhtoan>(entity =>
        {
            entity.HasKey(e => e.Mapttt);

            entity.ToTable("PHUONGTHUCTHANHTOAN");

            entity.Property(e => e.Mapttt).HasColumnName("MAPTTT");
            entity.Property(e => e.Tenphuongthuc)
                .HasMaxLength(255)
                .HasColumnName("TENPHUONGTHUC");
        });

        modelBuilder.Entity<Quan>(entity =>
        {
            entity.HasKey(e => e.Maquan);

            entity.ToTable("QUAN");

            entity.HasIndex(e => e.Matinh, "IX_QUAN_MATINH");

            entity.Property(e => e.Maquan)
                .ValueGeneratedNever()
                .HasColumnName("MAQUAN");
            entity.Property(e => e.Matinh).HasColumnName("MATINH");
            entity.Property(e => e.Tenquan)
                .HasMaxLength(500)
                .HasColumnName("TENQUAN");

            entity.HasOne(d => d.MatinhNavigation).WithMany(p => p.Quans)
                .HasForeignKey(d => d.Matinh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUAN_TINH");
        });

        modelBuilder.Entity<Sanpham>(entity =>
        {
            entity.HasKey(e => e.Masp);

            entity.ToTable("SANPHAM");

            entity.HasIndex(e => new { e.Madongsanpham, e.Mamau }, "idx_unique_DongSp_Mau").IsUnique();

            entity.Property(e => e.Masp).HasColumnName("MASP");
            entity.Property(e => e.Anhdaidien).HasColumnName("ANHDAIDIEN");
            entity.Property(e => e.Anhdegiay).HasColumnName("ANHDEGIAY");
            entity.Property(e => e.Anhmattren).HasColumnName("ANHMATTREN");
            entity.Property(e => e.Madongsanpham).HasColumnName("MADONGSANPHAM");
            entity.Property(e => e.Mamau)
                .HasMaxLength(255)
                .HasColumnName("MAMAU");
            entity.Property(e => e.TrangThai).HasColumnName("TRANGTHAI");
            entity.Property(e => e.Video).HasColumnName("VIDEO");

            entity.HasOne(d => d.MadongsanphamNavigation).WithMany(p => p.Sanphams)
                .HasForeignKey(d => d.Madongsanpham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SANPHAM_DONGSANPHAM");

            entity.HasOne(d => d.MamauNavigation).WithMany(p => p.Sanphams)
                .HasForeignKey(d => d.Mamau)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SANPHAM_MAU");
        });

        modelBuilder.Entity<Sanphamsize>(entity =>
        {
            entity.HasKey(e => e.Maspsize);

            entity.ToTable("SANPHAMSIZE");

            entity.HasIndex(e => new { e.Masp, e.Masize }, "idx_unique_Sanpham_size").IsUnique();

            entity.Property(e => e.Maspsize).HasColumnName("MASPSIZE");
            entity.Property(e => e.Masize).HasColumnName("MASIZE");
            entity.Property(e => e.Masp).HasColumnName("MASP");
            entity.Property(e => e.Slton).HasColumnName("SLTON");

            entity.HasOne(d => d.MasizeNavigation).WithMany(p => p.Sanphamsizes)
                .HasForeignKey(d => d.Masize)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SANPHAMSIZE_SIZE");

            entity.HasOne(d => d.MaspNavigation).WithMany(p => p.Sanphamsizes)
                .HasForeignKey(d => d.Masp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SANPHAMSIZE_SANPHAM");
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => e.Masize).HasName("PK__SIZE__8DA14C4EB0779537");

            entity.ToTable("SIZE");

            entity.Property(e => e.Masize).HasColumnName("MASIZE");
            entity.Property(e => e.Tensize)
                .HasMaxLength(255)
                .HasColumnName("TENSIZE");
        });

        modelBuilder.Entity<Sodiachi>(entity =>
        {
            entity.HasKey(e => e.Masodiachi);

            entity.ToTable("SODIACHI");

            entity.HasIndex(e => e.Makh, "IX_SODIACHI_MAKH");

            entity.Property(e => e.Masodiachi).HasColumnName("MASODIACHI");
            entity.Property(e => e.Diachi)
                .HasMaxLength(500)
                .HasColumnName("DIACHI");
            entity.Property(e => e.Makh).HasColumnName("MAKH");
            entity.Property(e => e.Sdtnguoinhan)
                .HasMaxLength(255)
                .HasColumnName("SDTNGUOINHAN");
            entity.Property(e => e.Tennguoinhan)
                .HasMaxLength(500)
                .HasColumnName("TENNGUOINHAN");

            entity.HasOne(d => d.MakhNavigation).WithMany(p => p.Sodiachis)
                .HasForeignKey(d => d.Makh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__KHACHHAG__SODIACHI__123213");
        });

        modelBuilder.Entity<Taikhoan>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__TAIKHOAN__161CF7258466B7F4");

            entity.ToTable("TAIKHOAN");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Loaitk).HasColumnName("LOAITK");
            entity.Property(e => e.Matkhau)
                .HasMaxLength(255)
                .HasColumnName("MATKHAU");
        });

        modelBuilder.Entity<Tinh>(entity =>
        {
            entity.HasKey(e => e.Matinh);

            entity.ToTable("TINH");

            entity.Property(e => e.Matinh)
                .ValueGeneratedNever()
                .HasColumnName("MATINH");
            entity.Property(e => e.Tentinh)
                .HasMaxLength(500)
                .HasColumnName("TENTINH");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.Mavoucher);

            entity.ToTable("VOUCHER");

            entity.Property(e => e.Mavoucher)
                .HasMaxLength(255)
                .HasColumnName("MAVOUCHER");
            entity.Property(e => e.Giamtoida)
                .HasColumnType("money")
                .HasColumnName("GIAMTOIDA");
            entity.Property(e => e.Giatoithieu)
                .HasColumnType("money")
                .HasColumnName("GIATOITHIEU");
            entity.Property(e => e.Ngayhethan)
                .HasColumnType("datetime")
                .HasColumnName("NGAYHETHAN");
            entity.Property(e => e.Ngaytao)
                .HasColumnType("datetime")
                .HasColumnName("NGAYTAO");
            entity.Property(e => e.Soluong).HasColumnName("SOLUONG");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
