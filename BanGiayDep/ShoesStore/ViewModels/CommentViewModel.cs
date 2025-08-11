using ShoesStore.Models;
using X.PagedList;

namespace ShoesStore.ViewModels
{
	public class CommentViewModel
	{
		public PagedList<Binhluan> blPage {  get; set; }
		public double overallStar {  get; set; }
		public int totalReview {  get; set; }
		public int fiveStar {  get; set; }
		public int fourStar { get; set; }
		public int threeStar { get; set; }

		public int twoStar { get; set; }
		public int oneStar { get; set; }

	}
}
