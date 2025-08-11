using ShoesStore.Areas.Admin.InterfaceRepositories;
using ShoesStore.Models;

namespace ShoesStore.Areas.Admin.Repositories
{
    public class MauAdminRepo : IMauAdmin
    {
        private readonly ShoesDbContext _context;

        public MauAdminRepo(ShoesDbContext context)
        {
            _context = context;
        }

        public Mau GetColorsById(string id)
        {

            var mau = _context.Maus.Find(id);
            return mau;
        }

        public IQueryable<Mau> GetAllColors()
        {
            var maus = _context.Maus
            .Select(maus => new Mau
            {
                Mamau = maus.Mamau,
                Tenmau = maus.Tenmau,
            });

            return maus;
        }

        public void AddColors(Mau color)
        {
            _context.Maus.Add(color);
            _context.SaveChanges();
        }

        public void UpdateColors(Mau color, string id)
        {
            var existingMau = _context.Maus.Find(id);
            if (existingMau != null)
            {
                existingMau.Tenmau = color.Tenmau;

                _context.SaveChanges();
            }
        }

        public void DeleteColors(string Id)
        {
            Mau mau = _context.Maus
                        .FirstOrDefault(m => m.Mamau == Id);

            if (mau != null)
            {
                _context.Maus.Remove(mau);
                _context.SaveChanges();
            }
        }
        public List<string> GetDistinctColors()
        {
            return _context.Maus.Select(l => l.Tenmau).Distinct().ToList();
        }

        public List<string> GetAllIdMau()
        {
            return _context.Maus.Select(x => x.Mamau).ToList();
        }

        public List<Mau> GetAllMauNotUsedForSp(int madongsp)
        {

            List<Sanpham> spList = _context.Sanphams.Where(x => x.Madongsanpham == madongsp).ToList();
            List<Mau> m = _context.Maus
                 .AsEnumerable()
                .Where(x => !x.Sanphams.Any(sp => spList.Any(z => z.Masp == sp.Masp))).ToList();


            return m;

        }
    }
}
