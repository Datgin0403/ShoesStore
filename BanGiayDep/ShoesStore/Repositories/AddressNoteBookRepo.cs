using Microsoft.EntityFrameworkCore;
using ShoesStore.InterfaceRepositories;
using ShoesStore.Models;

namespace ShoesStore.Repositories
{
    public class AddressNoteBookRepo : IAddressNoteBook
    {
        ShoesDbContext context;
        public AddressNoteBookRepo(ShoesDbContext context)
        {
            this.context = context;
        }
        public List<Tinh> GetTinhList()
        {
            return context.Tinhs.ToList();
        }
        public List<Quan> GetQuanList(int provinces)
        {
            return context.Quans.Where(x=>x.Matinh == provinces).ToList();
        }
        public List<Phuong> GetPhuongList(int districts)
        {
            return context.Phuongs.Where(x => x.Maquan == districts).ToList();
        }

        public List<Sodiachi> GetAllAddressNote()
        {
            return context.Sodiachis.ToList();
        }

        public List<Sodiachi> GetAllAddressNoteByMaKH(int maKH)
        {
            return context.Sodiachis.Where(x => x.Makh == maKH).ToList();
        }
        public List<Sodiachi> GetAllAddressNoteById(int masdc)
        {
            return context.Sodiachis.Where(x => x.Masodiachi == masdc).ToList();
        } 

        public int GetMaTinh(string TenTinh)
        {
             return context.Tinhs.FirstOrDefault(x=>x.Tentinh == TenTinh).Matinh;
        }
        public int GetMaQuan(string TenQuan)
        {
            return context.Quans.FirstOrDefault(x => x.Tenquan == TenQuan).Maquan;
        }
        public int GetMaPhuong(string TenPhuong)
        {
            return context.Phuongs.FirstOrDefault(x => x.Tenphuong == TenPhuong).Maphuong;
        }
        public void AddAddressNote(int proviceId, int districtId, int wardId,string address,int makh,string tennguoinhan,string sdt)
        {
            Tinh province = context.Tinhs.Find(proviceId);
            Quan district = context.Quans.Find(districtId);
            Phuong ward = context.Phuongs.Find(wardId);

            string finalAddress = address + ", "+province.Tentinh + ", "+district.Tenquan + ", "+ward.Tenphuong;

            Sodiachi sdc = new Sodiachi
            {
                Tennguoinhan = tennguoinhan,
                Sdtnguoinhan = sdt,
                Makh = makh,
                Diachi = finalAddress
            };

            context.Sodiachis.Add(sdc);
            context.SaveChanges();
        }
        

        public Sodiachi GetSodiachi(int masodiachi)
        {
            return context.Sodiachis.FirstOrDefault(x => x.Masodiachi == masodiachi);
        }

        public void UpdateSDC(int masdc, string hoten, string sdt, string diachi, int matinh, int maquan, int maphuong)
        {
            Sodiachi sdc = context.Sodiachis.FirstOrDefault(x => x.Masodiachi == masdc);
            string tentinh = context.Tinhs.FirstOrDefault(x => x.Matinh == matinh).Tentinh;
            string tenquan = context.Quans.FirstOrDefault(x => x.Maquan == maquan).Tenquan;
            string tenphuong = context.Phuongs.FirstOrDefault(x => x.Maphuong == maphuong).Tenphuong;
            string diachiFinal = diachi +", "+tentinh+", "+tenquan+", "+tenphuong;

            sdc.Tennguoinhan = hoten;
            sdc.Sdtnguoinhan = sdt;
            sdc.Diachi = diachiFinal;
            context.Sodiachis.Update(sdc);
            context.SaveChanges();
        }

        public void DeleteSDC(int masdc)
        {
            Sodiachi sdc = context.Sodiachis.FirstOrDefault(x => x.Masodiachi == masdc);
            context.Sodiachis.Remove(sdc);
            context.SaveChanges();
        }
    }
}
