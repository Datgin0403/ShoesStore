using ShoesStore.InterfaceRepositories;
using ShoesStore.Models;

namespace ShoesStore.Repositories
{
    public class PhuongthucthanhtoanRepo : IPhuongthucthanhtoan
    {
        ShoesDbContext context;
        public PhuongthucthanhtoanRepo(ShoesDbContext data)
        {
            this.context = data;
        }
        public List<Phuongthucthanhtoan> GetAllPttt()
        {
            List<Phuongthucthanhtoan> ptttList =context.Phuongthucthanhtoans.ToList();
            return ptttList;
        }
    }
}
