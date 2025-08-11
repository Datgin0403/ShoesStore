using ShoesStore.Models;

namespace ShoesStore.ViewModels
{
    public class AddressNoteBookViewModel
    {
        public string HoTen {  get; set; }
        public string Sdt {  get; set; }
        public List<Tinh> tinhList {  get; set; }
        public List<Quan> quanList {  get; set; }
        public List<Phuong> phuongList {  get; set; }
    }
}
