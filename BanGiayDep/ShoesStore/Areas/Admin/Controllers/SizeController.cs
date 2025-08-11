using Microsoft.AspNetCore.Mvc;
using ShoesStore.Areas.Admin.InterfaceRepositories;
using ShoesStore.Models;
using ShoesStore.Models.Authentication;

namespace ShoesStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthenticationM_S]
    public class SizeController : Controller
    {
        private readonly ISizeAdmin _repo;
        public SizeController(ISizeAdmin repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View(_repo.GetAllSizes().ToList());
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Size size)
        {
            if (!ModelState.IsValid)
            {
                return View(size);
            }

            _repo.AddSizes(size);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int Id)
        {
            Size size = _repo.GetSizesById(Id);
            return View(size);
        }


        [HttpPost]
        public IActionResult Edit(Size size, int Id)
        {
            if (!ModelState.IsValid)
            {
                return View(size);
            }

            _repo.UpdateSizes(size, Id);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _repo.DeleteSizes(id);
            return RedirectToAction("Index");
        }
    }
}
