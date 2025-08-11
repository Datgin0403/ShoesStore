using ShoesStore.Areas.Admin.InterfaceRepositories;
using ShoesStore.Models;

namespace ShoesStore.Areas.Admin.Repositories
{
    public class SpSizeAdminRepo : ISpSizeAdmin
    {
        ShoesDbContext context;
        public SpSizeAdminRepo(ShoesDbContext _context)
        {
            this.context = _context;
        }

        public void AddListTonKho(List<Sanphamsize> tkhoList)
        {
            context.Sanphamsizes.AddRange(tkhoList);
            context.SaveChanges();
        }
        public void DeleteTonKhoListByCtSp(int idctsp)
        {
            List<Sanphamsize> tkhoList = context.Sanphamsizes.Where(x => x.Masp == idctsp).ToList();
            context.Sanphamsizes.RemoveRange(tkhoList);
            context.SaveChanges();
        }

        public void UpdateListSpSize(List<Sanphamsize> spSize)
        {
            List<Sanphamsize> spsizeList = new List<Sanphamsize>();

            for (int i=0;i<spSize.Count;++i)
            {
                spsizeList.Add(context.Sanphamsizes.FirstOrDefault(x => x.Masize == spSize[i].Masize && x.Masp == spSize[i].Masp));
                spsizeList[i].Slton = spSize[i].Slton;
               
            }
           
            context.Sanphamsizes.UpdateRange(spsizeList);
            context.SaveChanges();
        }
    }
}
