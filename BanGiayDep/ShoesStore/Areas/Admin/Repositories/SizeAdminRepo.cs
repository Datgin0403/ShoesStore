using Microsoft.EntityFrameworkCore;
using ShoesStore.Areas.Admin.InterfaceRepositories;
using ShoesStore.Models;

namespace ShoesStore.Areas.Admin.Repositories
{
    public class SizeAdminRepo : ISizeAdmin
    {
        ShoesDbContext _context;

        public SizeAdminRepo(ShoesDbContext context)
        {
            this._context = context;
        }

        public List<string> GetSizeNameList()
        {
            List<string> szList = _context.Sizes.Select(x => x.Tensize).ToList();
            return szList;
        }

        public List<int> GetMasizeList()
        {
            List<int> MasizeList = _context.Sizes.Select(x => x.Masize).ToList();

            return MasizeList;
        }

        public Size GetSizesById(int id)
        {

            var size = _context.Sizes.Find(id);
            return size;
        }

        public IQueryable<Size> GetAllSizes()
        {
            var sizes = _context.Sizes
            .Select(sizes => new Size
            {
                Masize = sizes.Masize,
                Tensize = sizes.Tensize,
            });

            return sizes;
        }

        public void AddSizes(Size size)
        {

            _context.Sizes.Add(size);
            _context.SaveChanges();

            List<Sanphamsize> tkho = _context.Sanphams.Select(x => new Sanphamsize
            {   //x here is each object in chitietsanpham collection
                //create a new tonkho for each collected chitietsanpham
                Masp = x.Masp,
                Masize = size.Masize,
                Slton = 0
            }).ToList();

            _context.Sanphamsizes.AddRange(tkho);

            _context.SaveChanges();
        }

        public void UpdateSizes(Size size, int id)
        {
            var existingSize = _context.Sizes.Find(id);
            if (existingSize != null)
            {
                existingSize.Tensize = size.Tensize;

                _context.SaveChanges();
            }
        }

        public void DeleteSizes(int Id)
        {
            Size size = _context.Sizes
                        .FirstOrDefault(l => l.Masize == Id);

            if (size != null)
            {
                List<Sanphamsize> tkhoDelete = _context.Sanphamsizes.Where(x => x.Masize == Id).ToList();

                _context.Sanphamsizes.RemoveRange(tkhoDelete);
                _context.Sizes.Remove(size);
                _context.SaveChanges();
            }
        }
        public List<string> GetDistinctSizes()
        {
            return _context.Sizes.Select(l => l.Tensize).Distinct().ToList();
        }
    }
}
