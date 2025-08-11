using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoesStore.Areas.Admin.InterfaceRepositories;
using ShoesStore.Models;
using ShoesStore.Models.Authentication;
using System.Diagnostics;
using System.Linq;

namespace ShoesStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthenticationM_S]
    public class DongsanphamController : Controller
    {
        private readonly IDongsanphamAdmin _dongsanphamRepo;

        private readonly ILoaiAdmin _loairepo;



        public DongsanphamController(IDongsanphamAdmin dongsanphamRepo, ILoaiAdmin loairepo)
        {
            _dongsanphamRepo = dongsanphamRepo;
            _loairepo = loairepo;

        }


        public IActionResult Index()
        {
            return View(_dongsanphamRepo.GetAllDongsanpham().ToList());
        }

        private SelectList GetSelectListItems()
        {
            var loaiList = _loairepo.GetAllLoai().ToList();
            return new SelectList(loaiList, "Maloai", "Tenloai");
        }

        public IActionResult Create()
        {

            ViewBag.Selectloai = GetSelectListItems();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Dongsanpham dongsanpham)
        {
            dongsanpham.MaloaiNavigation = null;
            var loaiList = _loairepo.GetAllLoai().ToList();
            ViewBag.Selectloai = new SelectList(loaiList, "Maloai", "Tenloai", dongsanpham.Maloai);
            if (ModelState.IsValid)
            {
                _dongsanphamRepo.AddDongsanpham(dongsanpham);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Selectloai = GetSelectListItems();
            return View(dongsanpham);
        }

        public IActionResult Edit(int id)
        {
            var dongsanpham = _dongsanphamRepo.GetDongsanphamById(id);
            var loaiList = _loairepo.GetAllLoai().ToList();
            ViewBag.Selectloai = new SelectList(loaiList, "Maloai", "Tenloai", dongsanpham.Maloai);

            return View(dongsanpham);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Dongsanpham dongsanpham, int id)
        {
            if (ModelState.IsValid)
            {
                _dongsanphamRepo.UpdateDongsanpham(dongsanpham, id);
                return RedirectToAction(nameof(Index));
            }
            return View(dongsanpham);
        }



        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _dongsanphamRepo.DeleteDongsanpham(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
