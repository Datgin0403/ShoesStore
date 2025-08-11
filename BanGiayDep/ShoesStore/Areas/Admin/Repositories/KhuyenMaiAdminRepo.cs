using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesStore.Areas.Admin.InterfaceRepositories;
using ShoesStore.Models;
using System.Linq;

namespace ShoesStore.Areas.Admin.Repositories
{
    public class KhuyenMaiAdminRepo : IKhuyenMaiAdmin
    {
        ShoesDbContext _context;
        public KhuyenMaiAdminRepo(ShoesDbContext context)
        {
            this._context = context;
        }
        public Khuyenmai GetKhuyenmaiById(int id)
        {
            Khuyenmai km = _context.Khuyenmais.FirstOrDefault(x => x.Makm == id);
            return km;
        }
        public IQueryable<Khuyenmai> GetAllKhuyenmai()
        {
            var khuyenmai = _context.Khuyenmais.Select(km => new Khuyenmai
            {
                Makm = km.Makm,
                Ngaybd = km.Ngaybd,
                Ngaykt = km.Ngaykt,
                Phantramgiam = km.Phantramgiam
            }).OrderByDescending(x=>x.Ngaykt);
            return khuyenmai;
        }
        public void AddKhuyenmai(Khuyenmai km)
        {
            _context.Khuyenmais.Add(km);
            _context.SaveChanges();

            //Dongsanpham dsp1 = _context.Dongsanphams.Find(1);
            //km.Madongsanphams.Add(dsp1);
            //_context.Update(km);
            //_context.SaveChanges();
        }
        public void DeleteKhuyenmai(int Id)
        {
            Khuyenmai km = _context.Khuyenmais
                        .FirstOrDefault(k => k.Makm == Id);

            if (km != null)
            {
                _context.Khuyenmais.Remove(km);
                _context.SaveChanges();
            }
        }
        /*public List<Dongsanpham> GetDSPList(int makm)
        {
            List<Dongsanpham> dsp = _context.Khuyenmais.Where(x => x.Makm == makm).Select(x => new Dongsanpham
            {
                Madongsanpham = x.Madongsanpham,
                Mamau = x.Mamau,
                Masp = x.Masp,
                Anhdaidien = x.Anhdaidien,
                MamauNavigation = _context.Maus.FirstOrDefault(m => m.Mamau == x.Mamau),
                MadongsanphamNavigation = _context.Dongsanphams.FirstOrDefault(sp => sp.Madongsanpham == x.Madongsanpham),
                Sanphamsizes = c_context.Sanphamsizes.Where(tkho => tkho.Masp == x.Masp).ToList()
            }).ToList();
            return dsp;
        }*/

    }
}
