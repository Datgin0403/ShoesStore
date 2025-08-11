using ShoesStore.Areas.Admin.InterfaceRepositories;
using ShoesStore.Models;

namespace ShoesStore.Areas.Admin.Repositories
{
    public class DongsanphamAdminRepo : IDongsanphamAdmin
    {
        ShoesDbContext _context;
        public DongsanphamAdminRepo(ShoesDbContext context)
        {
            this._context = context;
        }

        public Dongsanpham GetDongsanphamById(int id)
        {
            Dongsanpham dsp = _context.Dongsanphams.FirstOrDefault(x => x.Madongsanpham == id);
            return dsp;
        }

        public IQueryable<Dongsanpham> GetAllDongsanpham()
        {
            var dsp = _context.Dongsanphams
            .Select(dongsanpham => new Dongsanpham
            {
                Madongsanpham = dongsanpham.Madongsanpham,
                Maloai = dongsanpham.Maloai,
                Tendongsp = dongsanpham.Tendongsp,
                Giagoc = dongsanpham.Giagoc,
                Mota = dongsanpham.Mota,
                Sanphams = _context.Sanphams.Where(x=>x.Madongsanpham == dongsanpham.Madongsanpham).ToList(),
                MaloaiNavigation = _context.Loais.FirstOrDefault(x=>x.Maloai==dongsanpham.Maloai)
            }).OrderByDescending(x=>x.Madongsanpham);

            return dsp;
        }

        public void AddDongsanpham(Dongsanpham Dongsanpham)
        {
            _context.Dongsanphams.Add(Dongsanpham);
            _context.SaveChanges();
        }

        public void UpdateDongsanpham(Dongsanpham Dongsanpham, int id)
        {
            var existingDongsanpham = _context.Dongsanphams.Find(id);
            if (existingDongsanpham != null)
            {
                existingDongsanpham.Tendongsp = Dongsanpham.Tendongsp;
                existingDongsanpham.Maloai = Dongsanpham.Maloai;
                existingDongsanpham.Tendongsp = Dongsanpham.Tendongsp;
                existingDongsanpham.Giagoc = Dongsanpham.Giagoc;
                existingDongsanpham.Mota = Dongsanpham.Mota;

                _context.SaveChanges();
            }
        }

        public void DeleteDongsanpham(int Id)
        {
            Dongsanpham Dongsanpham = _context.Dongsanphams
                        .FirstOrDefault(l => l.Madongsanpham == Id);

            if (Dongsanpham != null)
            {
                _context.Dongsanphams.Remove(Dongsanpham);
                _context.SaveChanges();
            }
        }
        public List<string> GetDistinctDongsanpham()
        {
            return _context.Dongsanphams.Select(l => l.Tendongsp).Distinct().ToList();
        }
    }
}

