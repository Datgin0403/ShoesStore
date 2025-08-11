using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesStore.Models;

namespace ShoesStore.ViewComponents
{
    public class UserProfileViewComponent :ViewComponent
    {
        private readonly ShoesDbContext _db;
        public UserProfileViewComponent(ShoesDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            string userEmail = HttpContext.Session.GetString("Email");
                // ?? "lephat@gmail.com";
            var user = _db.Taikhoans
                            .Include(t => t.Khachhang)
                            .FirstOrDefault(x => x.Email == userEmail);


            return View(user.Khachhang);


        }
    }
}
