using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoesStore.Migrations
{
    /// <inheritdoc />
    public partial class deleteXephang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NGAYXEPHANG",
                table: "KHACHHANG");

            migrationBuilder.DropColumn(
                name: "XEPHANG",
                table: "KHACHHANG");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NGAYXEPHANG",
                table: "KHACHHANG",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "XEPHANG",
                table: "KHACHHANG",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
