using Microsoft.EntityFrameworkCore;
using ShoesStore.Areas.Admin.InterfaceRepositories;
using ShoesStore.Areas.Admin.Repositories;
//using ShoesStore.Areas.Admin.Repositories;
using ShoesStore.InterfaceRepositories;
using ShoesStore.Models;
using ShoesStore.Repositories;
using ShoesStore.Service.Vnpay;
//using ShoesStore.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<IEmailSender, EmailSender>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor(); // Add this line
builder.Services.AddMemoryCache();
builder.Services.AddSession();
// 🟢 THÊM CHỖ IN CHUỖI KẾT NỐI Ở ĐÂY


Console.WriteLine("connection string: " + builder.Configuration.GetConnectionString("Shoes") );

builder.Services.AddDbContext<ShoesDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Shoes"));
});
builder.Services.AddScoped<IPhieuMuaAdmin, PhieuMuaAdminRepo>();
builder.Services.AddScoped<IDongsanphamAdmin, DongsanphamAdminRepo>();
builder.Services.AddScoped<IKhuyenMaiAdmin, KhuyenMaiAdminRepo>();
builder.Services.AddScoped<INhanvien, NhanvienAdminRepo>();
builder.Services.AddScoped<IBlogAdmin, BlogAdminRepo>();
builder.Services.AddScoped<ILoaiAdmin, LoaiAdminRepo>();
builder.Services.AddScoped<IMauAdmin, MauAdminRepo>();
builder.Services.AddScoped<ISizeAdmin, SizeAdminRepo>();
builder.Services.AddScoped<ISpSizeAdmin, SpSizeAdminRepo>();
builder.Services.AddScoped<ISanPhamAdmin, SanPhamAdminRepo>();
builder.Services.AddScoped<IDongSanpham, DongSanphamRepo>();
builder.Services.AddScoped<IPhuongthucthanhtoan, PhuongthucthanhtoanRepo>();
builder.Services.AddScoped<ISanpham, SanphamRepo>();
builder.Services.AddScoped<ISize, SizeRepo>();
builder.Services.AddScoped<ISanphamSize, SanphamSizeRepo>();
builder.Services.AddScoped<IMau, MauRepo>();
builder.Services.AddScoped<IKhachhang, KhachhangRepo>();
builder.Services.AddScoped<IPhieuMua, PhieuMuaRepo>();
builder.Services.AddScoped<IKhuyenMai, KhuyenMaiRepo>();
builder.Services.AddScoped<IBannerRepository, BannerRepository>();
builder.Services.AddScoped<IBinhLuan, BinhLuanRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IAddressNoteBook, AddressNoteBookRepo>();
builder.Services.AddScoped<IVoucher, VoucherRepo>();
builder.Services.AddScoped<IVoucherAdmin, VoucherAdminRepo>();
builder.Services.AddScoped<IVnPayService, VnPayService>();
var app = builder.Build();
app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "Admin/{controller=HomeAdmin}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
