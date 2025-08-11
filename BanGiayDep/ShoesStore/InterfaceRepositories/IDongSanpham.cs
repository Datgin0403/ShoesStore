using Microsoft.AspNetCore.Mvc;
using ShoesStore.Models;
using ShoesStore.ViewModels;

namespace ShoesStore.InterfaceRepositories
{
    public interface IDongSanpham 
    {
        Dongsanpham GetDongSanpham(int madong);
		List<DongsanphamViewModel> GetDongspView(string? maMau,int? sortGia, string? searchString,decimal? minPrice,decimal? maxPrice,int maLoai);
	}
}
