using ShoesStore.Models;
using System.ComponentModel.DataAnnotations;


namespace ShoesStore.Areas.Admin.ViewModels
{
    public class PhieuMuaDetailViewModel
    {
        public int Mapm { get; set; }
        public string Tendongsp { get; set; } = null!;
        public string Tensize { get; set; } = null!;
        public string Tennv { get; set; } = null!;
        public string Tenkh { get; set; } = null!;


        public Phieumua Phieumua { get; set; }
        public List<Chitietphieumua> Chitietphieumuas { get; set; }
    }
}
