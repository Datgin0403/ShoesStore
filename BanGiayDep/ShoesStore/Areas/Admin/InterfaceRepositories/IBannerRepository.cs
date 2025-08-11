using System.Collections.Generic;
using System.Threading.Tasks;
using ShoesStore.Models;

namespace ShoesStore.Areas.Admin.InterfaceRepositories
{
	public interface IBannerRepository
	{
        List<Banner> GetBanners();
        void AddBanner(Banner banner);
        void RemoveBanner(int id);
        void UpdateBanner(Banner banner);
        Banner GetBannerById(int id);

    }
}
