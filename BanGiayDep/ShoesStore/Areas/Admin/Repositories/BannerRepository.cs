using Microsoft.EntityFrameworkCore;
using ShoesStore.Areas.Admin.InterfaceRepositories;
using ShoesStore.Models;

namespace ShoesStore.Areas.Admin.Repositories
{
	public class BannerRepository : IBannerRepository
	{
        private readonly ShoesDbContext _context;

        public BannerRepository(ShoesDbContext context)
        {
            _context = context;
        }

        public List<Banner> GetBanners()
        {
            return _context.Banners.OrderByDescending(x=>x.Mabanner).ToList();
        }

        public void AddBanner(Banner banner)
        {
            _context.Banners.Add(banner);
            _context.SaveChanges();
        }

        public void RemoveBanner(int id)
        {
            var banner = _context.Banners.Find(id);
            if (banner != null)
            {
                _context.Banners.Remove(banner);
                _context.SaveChanges();
            }
        }

        public void UpdateBanner(Banner banner)
        {
            _context.Entry(banner).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Banner GetBannerById(int id)
        {
            return _context.Banners.Find(id);
        }
    }
}
