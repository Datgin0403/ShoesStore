using ShoesStore.Models;
using ShoesStore.Areas.Admin.InterfaceRepositories;

namespace ShoesStore.Areas.Admin.Repositories
{
    public class NhanvienAdminRepo :INhanvien
    {
        ShoesDbContext context;
        public NhanvienAdminRepo(ShoesDbContext context)
        {
            this.context = context;
        }

        public int getMaNVCurrent(string email)
        {
            int manv = context.Nhanviens.FirstOrDefault(x => x.Email == email).Manv;
            return manv;
        }

    }
}
