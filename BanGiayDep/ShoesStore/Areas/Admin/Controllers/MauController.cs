using Microsoft.AspNetCore.Mvc;
using ShoesStore.Areas.Admin.InterfaceRepositories;
using ShoesStore.Models;
using ShoesStore.Models.Authentication;

namespace ShoesStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthenticationM_S]
    public class MauController : Controller
    {
        private readonly IMauAdmin _repo;
        public MauController(IMauAdmin repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View(_repo.GetAllColors().ToList());
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]

        public IActionResult Create(Mau mau)
        {
            if (!ModelState.IsValid)
            {
                return View(mau);
            }

            _repo.AddColors(mau);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(string Id)
        {
            Mau mau = _repo.GetColorsById(Id);
            return View(mau);
        }


        [HttpPost]
        public IActionResult Edit(Mau mau, string Id)
        {
            if (!ModelState.IsValid)
            {
                return View(mau);
            }

            _repo.UpdateColors(mau, Id);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string id)
        {
            _repo.DeleteColors(id);
            return RedirectToAction("Index");
        }
    }
}
