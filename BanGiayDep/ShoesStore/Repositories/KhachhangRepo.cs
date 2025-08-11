using ShoesStore.InterfaceRepositories;
using ShoesStore.Models;

namespace ShoesStore.Repositories
{
    public class KhachhangRepo :IKhachhang
    {
        ShoesDbContext context;
        public KhachhangRepo(ShoesDbContext context)
        {
            this.context = context;
        }

        public Khachhang GetCurrentKh(string emailkh)
        {

            Khachhang kh = context.Khachhangs.FirstOrDefault(x=>x.Email == emailkh);
            return kh;
        }

        public void UpdateKh(Khachhang khachhang)
        {
            context.Khachhangs.Update(khachhang);
            context.SaveChanges();
        }
	}
}
