using ShoesStore.InterfaceRepositories;
using ShoesStore.Models;
using ShoesStore.ViewModels;
using System.Drawing;

namespace ShoesStore.Repositories
{
	public class SanphamSizeRepo : ISanphamSize
	{
		ShoesDbContext context;
		public SanphamSizeRepo(ShoesDbContext context)
		{
			this.context = context;
		}

		public Sanphamsize GetSLTon(int masp, int masize)
		{
			Sanphamsize spsize = context.Sanphamsizes.FirstOrDefault(x => x.Masp == masp && x.Masize == masize);
			return spsize;
		}

        public int GetMaspsize(int masp, string tensize)
		{
			int masize = context.Sizes.FirstOrDefault(x=>x.Tensize == tensize).Masize;
			int maspsize = context.Sanphamsizes.FirstOrDefault(x=>x.Masp ==masp && x.Masize == masize).Maspsize;
			return maspsize;
		}

        public void MinusSanPhamSize(PhieuMuaViewModel pm)
		{
			List<Sanphamsize> spsize = new List<Sanphamsize>();

            foreach (ShoppingCartItem cartItem in pm.listcartItem)
			{
				spsize.Add(context.Sanphamsizes
			   .FirstOrDefault(x => x.Maspsize == cartItem.Maspsize));
				spsize.Last().Slton -= cartItem.Quantity;
            }


			context.Sanphamsizes.UpdateRange(spsize);
			
        }

    }
}
