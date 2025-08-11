using ShoesStore.InterfaceRepositories;
using ShoesStore.Models;
using ShoesStore.ViewModels;
using X.PagedList;

namespace ShoesStore.Repositories
{
    public class BinhLuanRepository : IBinhLuan
    {
        private readonly ShoesDbContext _context;

        public BinhLuanRepository(ShoesDbContext context)
        {
            _context = context;
        }

        public IQueryable<Binhluan> LayTatCa()
        {
            return _context.Binhluans.OrderBy(x => x.Ngaybl);
        }

        public void AddBinhLuan(Binhluan bl)
        {
            _context.Binhluans.Add(bl);
            _context.SaveChanges();
        }

        public CommentViewModel GetBlList(int masp,int page=1)
        {
			Sanpham sp = _context.Sanphams.Find(masp);
			int Madongsanpham = _context.Dongsanphams.FirstOrDefault(x => x.Madongsanpham == sp.Madongsanpham).Madongsanpham;
			List<Binhluan> listofComment = _context.Binhluans
				.Where(objComment => objComment.Madongsanpham == Madongsanpham)
				.Select(x => new Binhluan
				{
					Mabl = x.Mabl,
					Makh = x.Makh,
					MakhNavigation = x.MakhNavigation,
					Ngaybl = x.Ngaybl,
					Rating = x.Rating,
					Noidungbl = x.Noidungbl
				})
				.OrderByDescending(x => x.Mabl).ToList();

			PagedList<Binhluan> pageBl = new PagedList<Binhluan>(listofComment, page, 3);
			CommentViewModel cmtView = new CommentViewModel
			{
				blPage = pageBl,
				overallStar = listofComment.Count()>0 ? listofComment.Average(x=>x.Rating) : 0,
				totalReview = listofComment.Count(),
				fiveStar = listofComment.Count(x => x.Rating==5),
				fourStar = listofComment.Count(x => x.Rating == 4),
				threeStar = listofComment.Count(x => x.Rating == 3),
				twoStar = listofComment.Count(x => x.Rating == 2),
				oneStar = listofComment.Count(x => x.Rating == 1),
			};

			return cmtView;
		}

	}
}
