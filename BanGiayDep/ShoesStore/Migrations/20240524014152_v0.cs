using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoesStore.Migrations
{
    /// <inheritdoc />
    public partial class v0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banner",
                columns: table => new
                {
                    MABANNER = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TENBANNER = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    VITRI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LINK = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HOATDONG = table.Column<bool>(type: "bit", nullable: false),
                    SLOGAN = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, defaultValueSql: "(N'')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banner", x => x.MABANNER);
                });

            migrationBuilder.CreateTable(
                name: "KHUYENMAI",
                columns: table => new
                {
                    MAKM = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NGAYBD = table.Column<DateTime>(type: "datetime", nullable: false),
                    NGAYKT = table.Column<DateTime>(type: "datetime", nullable: false),
                    PHANTRAMGIAM = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KHUYENMAI", x => x.MAKM);
                });

            migrationBuilder.CreateTable(
                name: "LOAI",
                columns: table => new
                {
                    MALOAI = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TENLOAI = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LOAI__0CDEF5199DDB5F30", x => x.MALOAI);
                });

            migrationBuilder.CreateTable(
                name: "MAU",
                columns: table => new
                {
                    MAMAU = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TENMAU = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MAU__94185D28760C1F74", x => x.MAMAU);
                });

            migrationBuilder.CreateTable(
                name: "PHUONGTHUCTHANHTOAN",
                columns: table => new
                {
                    MAPTTT = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TENPHUONGTHUC = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PHUONGTHUCTHANHTOAN", x => x.MAPTTT);
                });

            migrationBuilder.CreateTable(
                name: "SIZE",
                columns: table => new
                {
                    MASIZE = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TENSIZE = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SIZE__8DA14C4EB0779537", x => x.MASIZE);
                });

            migrationBuilder.CreateTable(
                name: "TAIKHOAN",
                columns: table => new
                {
                    EMAIL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MATKHAU = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LOAITK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TAIKHOAN__161CF7258466B7F4", x => x.EMAIL);
                });

            migrationBuilder.CreateTable(
                name: "TINH",
                columns: table => new
                {
                    MATINH = table.Column<int>(type: "int", nullable: false),
                    TENTINH = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TINH", x => x.MATINH);
                });

            migrationBuilder.CreateTable(
                name: "VOUCHER",
                columns: table => new
                {
                    MAVOUCHER = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SOLUONG = table.Column<int>(type: "int", nullable: false),
                    GIATOITHIEU = table.Column<decimal>(type: "money", nullable: false),
                    GIAMTOIDA = table.Column<decimal>(type: "money", nullable: false),
                    NGAYTAO = table.Column<DateTime>(type: "datetime", nullable: false),
                    NGAYHETHAN = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VOUCHER", x => x.MAVOUCHER);
                });

            migrationBuilder.CreateTable(
                name: "DONGSANPHAM",
                columns: table => new
                {
                    MADONGSANPHAM = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MALOAI = table.Column<int>(type: "int", nullable: false),
                    TENDONGSP = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    GIAGOC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MOTA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TRANGTHAI = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SANPHAM__B87C02A5C6299161", x => x.MADONGSANPHAM);
                    table.ForeignKey(
                        name: "FK__SANPHAM__IDLOAI__4222D4EF",
                        column: x => x.MALOAI,
                        principalTable: "LOAI",
                        principalColumn: "MALOAI");
                });

            migrationBuilder.CreateTable(
                name: "KHACHHANG",
                columns: table => new
                {
                    MAKH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TENKH = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    GIOITINH = table.Column<bool>(type: "bit", nullable: true),
                    NGAYSINH = table.Column<DateTime>(type: "datetime", nullable: true),
                    NGAYXEPHANG = table.Column<DateTime>(type: "datetime", nullable: true),
                    XEPHANG = table.Column<int>(type: "int", nullable: false),
                    TONGXU = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KHACHHAN__B87DC1A73C57F8C5", x => x.MAKH);
                    table.ForeignKey(
                        name: "FK__KHACHHANG__EMAIL__398D8EEE",
                        column: x => x.EMAIL,
                        principalTable: "TAIKHOAN",
                        principalColumn: "EMAIL");
                });

            migrationBuilder.CreateTable(
                name: "NHANVIEN",
                columns: table => new
                {
                    MANV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TENNV = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DIACHI = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SDT = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    GIOITINH = table.Column<bool>(type: "bit", nullable: false),
                    NGAYSINH = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NHANVIEN__B87DC9B274B84C76", x => x.MANV);
                    table.ForeignKey(
                        name: "FK__NHANVIEN__EMAIL__3C69FB99",
                        column: x => x.EMAIL,
                        principalTable: "TAIKHOAN",
                        principalColumn: "EMAIL");
                });

            migrationBuilder.CreateTable(
                name: "QUAN",
                columns: table => new
                {
                    MAQUAN = table.Column<int>(type: "int", nullable: false),
                    TENQUAN = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MATINH = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QUAN", x => x.MAQUAN);
                    table.ForeignKey(
                        name: "FK_QUAN_TINH",
                        column: x => x.MATINH,
                        principalTable: "TINH",
                        principalColumn: "MATINH");
                });

            migrationBuilder.CreateTable(
                name: "CHITIETKHUYENMAI",
                columns: table => new
                {
                    MAKM = table.Column<int>(type: "int", nullable: false),
                    MADONGSANPHAM = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHITIETKHUYENMAI", x => new { x.MAKM, x.MADONGSANPHAM });
                    table.ForeignKey(
                        name: "FK_CHITIETKHUYENMAI_DONGSANPHAM",
                        column: x => x.MADONGSANPHAM,
                        principalTable: "DONGSANPHAM",
                        principalColumn: "MADONGSANPHAM");
                    table.ForeignKey(
                        name: "FK_CHITIETKHUYENMAI_KHUYENMAI",
                        column: x => x.MAKM,
                        principalTable: "KHUYENMAI",
                        principalColumn: "MAKM");
                });

            migrationBuilder.CreateTable(
                name: "SANPHAM",
                columns: table => new
                {
                    MASP = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MADONGSANPHAM = table.Column<int>(type: "int", nullable: false),
                    MAMAU = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ANHDAIDIEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ANHMATTREN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ANHDEGIAY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VIDEO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TRANGTHAI = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SANPHAM", x => x.MASP);
                    table.ForeignKey(
                        name: "FK_SANPHAM_DONGSANPHAM",
                        column: x => x.MADONGSANPHAM,
                        principalTable: "DONGSANPHAM",
                        principalColumn: "MADONGSANPHAM");
                    table.ForeignKey(
                        name: "FK_SANPHAM_MAU",
                        column: x => x.MAMAU,
                        principalTable: "MAU",
                        principalColumn: "MAMAU");
                });

            migrationBuilder.CreateTable(
                name: "BINHLUAN",
                columns: table => new
                {
                    MABL = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOIDUNGBL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NGAYBL = table.Column<DateTime>(type: "datetime", nullable: false),
                    MADONGSANPHAM = table.Column<int>(type: "int", nullable: false),
                    MAKH = table.Column<int>(type: "int", nullable: false),
                    RATING = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BINHLUAN", x => x.MABL);
                    table.ForeignKey(
                        name: "FK_BINHLUAN_DONGSANPHAM",
                        column: x => x.MADONGSANPHAM,
                        principalTable: "DONGSANPHAM",
                        principalColumn: "MADONGSANPHAM");
                    table.ForeignKey(
                        name: "FK_BINHLUAN_KHACHHANG",
                        column: x => x.MAKH,
                        principalTable: "KHACHHANG",
                        principalColumn: "MAKH");
                });

            migrationBuilder.CreateTable(
                name: "SODIACHI",
                columns: table => new
                {
                    MASODIACHI = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MAKH = table.Column<int>(type: "int", nullable: false),
                    TENNGUOINHAN = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SDTNGUOINHAN = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DIACHI = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SODIACHI", x => x.MASODIACHI);
                    table.ForeignKey(
                        name: "FK__KHACHHAG__SODIACHI__123213",
                        column: x => x.MAKH,
                        principalTable: "KHACHHANG",
                        principalColumn: "MAKH");
                });

            migrationBuilder.CreateTable(
                name: "BLOG",
                columns: table => new
                {
                    MABLOG = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MANV = table.Column<int>(type: "int", nullable: false),
                    NOIDUNG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    THELOAI = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Anhdaidien = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BLOG", x => x.MABLOG);
                    table.ForeignKey(
                        name: "FK_BLOG_NHANVIEN",
                        column: x => x.MANV,
                        principalTable: "NHANVIEN",
                        principalColumn: "MANV");
                });

            migrationBuilder.CreateTable(
                name: "PHIEUMUA",
                columns: table => new
                {
                    MAPM = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NGAYDAT = table.Column<DateTime>(type: "datetime", nullable: false),
                    MAKH = table.Column<int>(type: "int", nullable: true),
                    MANV = table.Column<int>(type: "int", nullable: true),
                    MAVOUCHER = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TINHTRANG = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MAPTTT = table.Column<int>(type: "int", nullable: false),
                    GHICHU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LYDOHUYDON = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TONGTIEN = table.Column<decimal>(type: "money", nullable: true),
                    DIACHINGUOINHAN = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, defaultValueSql: "(N'')"),
                    EMAILNGUOINHAN = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, defaultValueSql: "(N'')"),
                    SDTNGUOINHAN = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, defaultValueSql: "(N'')"),
                    TENNGUOINHAN = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, defaultValueSql: "(N'')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PHIEUDAT__B87C5B0D30DC315E", x => x.MAPM);
                    table.ForeignKey(
                        name: "FK_PHIEUMUA_KHACHHANG",
                        column: x => x.MAKH,
                        principalTable: "KHACHHANG",
                        principalColumn: "MAKH");
                    table.ForeignKey(
                        name: "FK_PHIEUMUA_PHUONGTHUCTHANHTOAN",
                        column: x => x.MAPTTT,
                        principalTable: "PHUONGTHUCTHANHTOAN",
                        principalColumn: "MAPTTT");
                    table.ForeignKey(
                        name: "FK_PHIEUMUA_VOUCHER",
                        column: x => x.MAVOUCHER,
                        principalTable: "VOUCHER",
                        principalColumn: "MAVOUCHER");
                    table.ForeignKey(
                        name: "FK__PHIEUDAT__IDNV__5441852A",
                        column: x => x.MANV,
                        principalTable: "NHANVIEN",
                        principalColumn: "MANV");
                });

            migrationBuilder.CreateTable(
                name: "PHUONG",
                columns: table => new
                {
                    MAPHUONG = table.Column<int>(type: "int", nullable: false),
                    TENPHUONG = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MAQUAN = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PHUONG", x => x.MAPHUONG);
                    table.ForeignKey(
                        name: "FK_QUAN_PHUONG",
                        column: x => x.MAQUAN,
                        principalTable: "QUAN",
                        principalColumn: "MAQUAN");
                });

            migrationBuilder.CreateTable(
                name: "SANPHAMSIZE",
                columns: table => new
                {
                    MASPSIZE = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MASP = table.Column<int>(type: "int", nullable: false),
                    MASIZE = table.Column<int>(type: "int", nullable: false),
                    SLTON = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SANPHAMSIZE", x => x.MASPSIZE);
                    table.ForeignKey(
                        name: "FK_SANPHAMSIZE_SANPHAM",
                        column: x => x.MASP,
                        principalTable: "SANPHAM",
                        principalColumn: "MASP");
                    table.ForeignKey(
                        name: "FK_SANPHAMSIZE_SIZE",
                        column: x => x.MASIZE,
                        principalTable: "SIZE",
                        principalColumn: "MASIZE");
                });

            migrationBuilder.CreateTable(
                name: "CHITIETPHIEUMUA",
                columns: table => new
                {
                    MAPM = table.Column<int>(type: "int", nullable: false),
                    MASPSIZE = table.Column<int>(type: "int", nullable: false),
                    SOLUONG = table.Column<int>(type: "int", nullable: false),
                    DONGIA = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHITIETPHIEUMUA", x => new { x.MAPM, x.MASPSIZE });
                    table.ForeignKey(
                        name: "FK_CHITIETPHIEUMUA_PHIEUMUA",
                        column: x => x.MAPM,
                        principalTable: "PHIEUMUA",
                        principalColumn: "MAPM");
                    table.ForeignKey(
                        name: "FK_CHITIETPHIEUMUA_SANPHAMSIZE",
                        column: x => x.MASPSIZE,
                        principalTable: "SANPHAMSIZE",
                        principalColumn: "MASPSIZE");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BINHLUAN_MADONGSANPHAM",
                table: "BINHLUAN",
                column: "MADONGSANPHAM");

            migrationBuilder.CreateIndex(
                name: "IX_BINHLUAN_MAKH",
                table: "BINHLUAN",
                column: "MAKH");

            migrationBuilder.CreateIndex(
                name: "IX_BLOG_MANV",
                table: "BLOG",
                column: "MANV");

            migrationBuilder.CreateIndex(
                name: "IX_CHITIETKHUYENMAI_MADONGSANPHAM",
                table: "CHITIETKHUYENMAI",
                column: "MADONGSANPHAM");

            migrationBuilder.CreateIndex(
                name: "IX_CHITIETKHUYENMAI_MAKM",
                table: "CHITIETKHUYENMAI",
                column: "MAKM");

            migrationBuilder.CreateIndex(
                name: "IX_CHITIETPHIEUMUA_IDCHITIETSP",
                table: "CHITIETPHIEUMUA",
                column: "MASPSIZE");

            migrationBuilder.CreateIndex(
                name: "IX_DONGSANPHAM_MALOAI",
                table: "DONGSANPHAM",
                column: "MALOAI");

            migrationBuilder.CreateIndex(
                name: "IX_KHACHHANG_EMAIL",
                table: "KHACHHANG",
                column: "EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__LOAI__BF45B996B528FC06",
                table: "LOAI",
                column: "TENLOAI",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NHANVIEN_EMAIL",
                table: "NHANVIEN",
                column: "EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PHIEUMUA_MAKH",
                table: "PHIEUMUA",
                column: "MAKH");

            migrationBuilder.CreateIndex(
                name: "IX_PHIEUMUA_MANV",
                table: "PHIEUMUA",
                column: "MANV");

            migrationBuilder.CreateIndex(
                name: "IX_PHIEUMUA_MAPTTT",
                table: "PHIEUMUA",
                column: "MAPTTT");

            migrationBuilder.CreateIndex(
                name: "IX_PHIEUMUA_MAVOUCHER",
                table: "PHIEUMUA",
                column: "MAVOUCHER");

            migrationBuilder.CreateIndex(
                name: "IX_PHUONG_MAQUAN",
                table: "PHUONG",
                column: "MAQUAN");

            migrationBuilder.CreateIndex(
                name: "IX_QUAN_MATINH",
                table: "QUAN",
                column: "MATINH");

            migrationBuilder.CreateIndex(
                name: "idx_unique_DongSp_Mau",
                table: "SANPHAM",
                columns: new[] { "MADONGSANPHAM", "MAMAU" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SANPHAM_MAMAU",
                table: "SANPHAM",
                column: "MAMAU");

            migrationBuilder.CreateIndex(
                name: "idx_unique_Sanpham_size",
                table: "SANPHAMSIZE",
                columns: new[] { "MASP", "MASIZE" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SANPHAMSIZE_MASIZE",
                table: "SANPHAMSIZE",
                column: "MASIZE");

            migrationBuilder.CreateIndex(
                name: "IX_SODIACHI_MAKH",
                table: "SODIACHI",
                column: "MAKH");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banner");

            migrationBuilder.DropTable(
                name: "BINHLUAN");

            migrationBuilder.DropTable(
                name: "BLOG");

            migrationBuilder.DropTable(
                name: "CHITIETKHUYENMAI");

            migrationBuilder.DropTable(
                name: "CHITIETPHIEUMUA");

            migrationBuilder.DropTable(
                name: "PHUONG");

            migrationBuilder.DropTable(
                name: "SODIACHI");

            migrationBuilder.DropTable(
                name: "KHUYENMAI");

            migrationBuilder.DropTable(
                name: "PHIEUMUA");

            migrationBuilder.DropTable(
                name: "SANPHAMSIZE");

            migrationBuilder.DropTable(
                name: "QUAN");

            migrationBuilder.DropTable(
                name: "KHACHHANG");

            migrationBuilder.DropTable(
                name: "PHUONGTHUCTHANHTOAN");

            migrationBuilder.DropTable(
                name: "VOUCHER");

            migrationBuilder.DropTable(
                name: "NHANVIEN");

            migrationBuilder.DropTable(
                name: "SANPHAM");

            migrationBuilder.DropTable(
                name: "SIZE");

            migrationBuilder.DropTable(
                name: "TINH");

            migrationBuilder.DropTable(
                name: "TAIKHOAN");

            migrationBuilder.DropTable(
                name: "DONGSANPHAM");

            migrationBuilder.DropTable(
                name: "MAU");

            migrationBuilder.DropTable(
                name: "LOAI");
        }
    }
}
