using Microsoft.AspNetCore.Mvc;
using ShoesStore.Models;

namespace ShoesStore.ViewComponents
{
    public class LoaiSPViewComponent : ViewComponent
    {
        ShoesDbContext _context;
        public LoaiSPViewComponent(ShoesDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            IEnumerable<Loai> l1 = _context.Loais.ToList();
            return View(l1);
        }

    }
}
