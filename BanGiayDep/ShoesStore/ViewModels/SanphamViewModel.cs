using ShoesStore.Models;

namespace ShoesStore.ViewModels
{
	public class SanphamViewModel
	{
		public Dongsanpham dongsanpham {  get; set; }
		public List<Sanpham> sanphams { get; set; }
		public List<string> tenSize {  get; set; }
		public List<int> slton {  get; set; }
		
	}
}
