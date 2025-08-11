using Microsoft.AspNetCore.Mvc;
using ShoesStore.InterfaceRepositories;
using ShoesStore.Models;

namespace ShoesStore.Controllers
{
    public class TrackingOrderController : Controller
    {
        private readonly IPhieuMua _phieuMuaRepo;

        public TrackingOrderController(IPhieuMua phieuMuaRepo)
        {
            _phieuMuaRepo = phieuMuaRepo;
        }


        public IActionResult Tracking()
        {
            return View();
        }


        public IActionResult TrackingDetails(int orderId)
        {
            var order = _phieuMuaRepo.GetOrderById(orderId);
            if (order == null)
            {
                return PartialView("NotFound");  // Ensure you have a view to handle the not found case.
            }

            return PartialView("TrackingDetails", order);
        }
    }
}