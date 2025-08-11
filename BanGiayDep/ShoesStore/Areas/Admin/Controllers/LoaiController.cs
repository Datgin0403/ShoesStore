using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesStore.Areas.Admin.InterfaceRepositories;
using ShoesStore.Models;
using ShoesStore.Models.Authentication;
using ShoesStore.ViewModels;

namespace ShoesStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthenticationM_S]
    public class LoaiController : Controller
    {

        private readonly ILoaiAdmin _lrepo;
        public LoaiController(ILoaiAdmin lrepo)
        {
            _lrepo = lrepo;
        }
        public IActionResult Index()
        {
            return View(_lrepo.GetAllLoai().ToList());
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]

        public IActionResult Create([Bind("Idloai,Tenloai,Slogan")] Loai loai)
        {
            if (!ModelState.IsValid)
            {
                return View(loai);
            }

            _lrepo.AddLoai(loai);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int Id)
        {
            Loai loai = _lrepo.GetLoaiById(Id);
            return View(loai);
        }


        [HttpPost]
        public IActionResult Edit(Loai loai, int Id)
        {
            if (!ModelState.IsValid)
            {
                return View(loai);
            }

            _lrepo.UpdateLoai(loai, Id);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _lrepo.DeleteLoai(id);
            return RedirectToAction("Index");
        }

    }
}
