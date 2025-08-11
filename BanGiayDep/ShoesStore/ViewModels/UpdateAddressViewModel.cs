using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShoesStore.ViewModels
{
    public class UpdateAddressViewModel
    {
        public int Masdc {  get; set; }
        public string Tennguoinhan {  get; set; }
        public string Sdtnguoinhan { get; set; }
        public string Diachi {  get; set; }
        public int MaTinh {  get; set; }
        public int MaQuan {  get; set; }
        public int MaPhuong {  get; set; }
        public List<SelectListItem> tinhSelect {  get; set; }
        public List<SelectListItem> quanSelect {  get; set; }
        public List<SelectListItem> phuongSelect {  get; set; }
    }
}
