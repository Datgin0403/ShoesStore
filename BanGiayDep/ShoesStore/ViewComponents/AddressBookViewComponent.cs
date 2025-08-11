using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoesStore.InterfaceRepositories;
using ShoesStore.Models;
using ShoesStore.ViewModels;

namespace ShoesStore.ViewComponents
{
    public class AddressBookViewComponent : ViewComponent
    {

        IAddressNoteBook addressRepo; IKhachhang khRepo;
        public AddressBookViewComponent(IAddressNoteBook addressRepo,IKhachhang khRepo)
        {
            this.addressRepo = addressRepo;
            this.khRepo = khRepo;
        }
        public IViewComponentResult Invoke()
        {
            List<Tinh> tinhs = addressRepo.GetTinhList();
            List<SelectListItem> selectTinh = tinhs.Select(x => new SelectListItem
            {
                Value = x.Matinh.ToString(),
                Text = x.Tentinh
            }).ToList();
            ViewBag.TinhSelect = selectTinh;

            string Email = HttpContext.Session.GetString("Email")  ?? "lephat@gmail.com";
            Khachhang kh = khRepo.GetCurrentKh(Email);

            SodiachiProfileViewModel sdcprofile = new SodiachiProfileViewModel()
            {
                sdc = addressRepo.GetAllAddressNoteByMaKH(kh.Makh),
                currentKhName = kh.Tenkh,
                currentPhone = kh.Sdt
            };
            return View(sdcprofile);
        }
    }
}
