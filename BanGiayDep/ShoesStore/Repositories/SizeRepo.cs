using ShoesStore.InterfaceRepositories;
using ShoesStore.Models;

namespace ShoesStore.Repositories
{
    public class SizeRepo : ISize
    {
        ShoesDbContext _context;
        public SizeRepo(ShoesDbContext context)
        {
            _context = context;
        }


        public Size GetSize(int masize)
        {
            Size sz = _context.Sizes.FirstOrDefault(x => x.Masize == masize);
            return sz;
        }

        public Size GetSizeByName(string name)
        {
            Size sz = _context.Sizes.FirstOrDefault(x => x.Tensize == name);
            return sz;
        }
    }
}
