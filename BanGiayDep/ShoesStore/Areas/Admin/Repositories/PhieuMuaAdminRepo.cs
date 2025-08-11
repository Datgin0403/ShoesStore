using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesStore.Areas.Admin.InterfaceRepositories;
using ShoesStore.InterfaceRepositories;
using ShoesStore.Models;

namespace ShoesStore.Areas.Admin.Repositories
{
    public class PhieuMuaAdminRepo : IPhieuMuaAdmin
    {
        private readonly ShoesDbContext _context;
        public PhieuMuaAdminRepo(ShoesDbContext context)
        {
            _context = context;
        }
        public Phieumua GetPhieuMuaById(int id)
        {
            Phieumua phieuMua = _context.Phieumuas.FirstOrDefault(x => x.Mapm == id);
            /*phieuMua.MakhNavigation = _context.Khachhangs.FirstOrDefault(x => x.Makh == phieuMua.Makh);
            phieuMua.ManvNavigation = _context.Nhanviens.FirstOrDefault(x => x.Manv == phieuMua.Manv);
            phieuMua.MaptttNavigation = _context.Phuongthucthanhtoans.FirstOrDefault(x => x.Mapttt == phieuMua.Mapttt);*/

            return phieuMua;
        }
        public IQueryable<Phieumua> GetAllPhieuMua()
        {
            var phieuMuas = _context.Phieumuas
            .Select(pm => new Phieumua
            {
                Mapm = pm.Mapm,
                Ngaydat = pm.Ngaydat,
                Makh = pm.Makh,
                Manv = pm.Manv,
                Tinhtrang = pm.Tinhtrang,
                Mapttt = pm.Mapttt,
                Ghichu = pm.Ghichu,
                Lydohuydon = pm.Lydohuydon,
                Tongtien = pm.Tongtien,
                /*MakhNavigation = _context.Khachhangs.FirstOrDefault(x => x.Makh == pm.Makh),
                ManvNavigation = _context.Nhanviens.FirstOrDefault(x => x.Manv == pm.Manv),
                MaptttNavigation = _context.Phuongthucthanhtoans.FirstOrDefault(x => x.Mapttt == pm.Mapttt)*/
            }).OrderByDescending(x=>x.Mapm);

            return phieuMuas;
        }
        /*public void UpdatePhieuMua(Phieumua pm, int id)
        {
            var PhieuMua = _context.Phieumuas.Find(id);
            if (PhieuMua != null)
            {
                PhieuMua.Ngaydat = pm.Ngaydat;
                PhieuMua.Makh = pm.Makh;
                PhieuMua.Manv = pm.Manv;
                PhieuMua.Tinhtrang = pm.Tinhtrang;
                PhieuMua.Mapttt = pm.Mapttt;
                PhieuMua.Ghichu = pm.Ghichu;
                PhieuMua.Lydohuydon= pm.Lydohuydon;
                PhieuMua.Tongtien = pm.Tongtien;

                _context.SaveChanges();
            }
        }*/
        public void UpdatePhieuMua(Phieumua pm, int id,string oldState)
        {
            var PhieuMua = _context.Phieumuas.Find(id);
            if (PhieuMua != null)
            {
                PhieuMua.Manv = pm.Manv;
                PhieuMua.Lydohuydon = pm.Lydohuydon;
                PhieuMua.Tinhtrang = pm.Tinhtrang;
                PhieuMua.Tongtien = _context.Chitietphieumuas
                            .Where(c => c.Mapm == id)
                            .Sum(c => c.Dongia * c.Soluong);
                _context.SaveChanges();
            }
            if(PhieuMua.Makh != null)
            {
                if(oldState != PhieuMua.Tinhtrang)
                {
                    Khachhang kh = _context.Khachhangs.Find(PhieuMua.Makh);

                    decimal coinGet = Math.Ceiling((PhieuMua.Tongtien ?? 0) / 100000);
                    coinGet *= 1000;
                    if (PhieuMua.Tinhtrang == "Confirm")
                    {
                        kh.Tongxu += coinGet;
                    }
                    else if(PhieuMua.Tinhtrang == "Cancel")
                    {
                        kh.Tongxu -= coinGet;
                    }
                    _context.Khachhangs.Update(kh);
                    _context.SaveChanges();
                }
            }
        }
        /*public void DeletePhieuMua(int Id)
        {
            Phieumua phieuMua = _context.Phieumuas.FirstOrDefault(x => x.Mapm == Id);

            if (phieuMua != null)
            {
                _context.Phieumuas.Remove(phieuMua);
                _context.SaveChanges();
            }
        }   */
    }
}
