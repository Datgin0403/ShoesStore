using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ShoesStore.Models;
using ShoesStore.Repositories;
using ShoesStore.InterfaceRepositories;

public class OrderHistoryViewComponent : ViewComponent
{
    private readonly IPhieuMua _phieuMuaRepository;

    public OrderHistoryViewComponent(IPhieuMua phieuMuaRepository)
    {
        _phieuMuaRepository = phieuMuaRepository;
    }

    public IViewComponentResult Invoke()
    {
        string emailkh = HttpContext.Session.GetString("Email");
        // ?? "lephat@gmail.com";
        /*		if (string.IsNullOrEmpty(emailkh))
                {
                    TempData["OrderHistory"] = "Vui lòng đăng nhập để xem lịch sử đơn hàng";
                }*/

        var orders = _phieuMuaRepository.GetOrderHistoryByEmail(emailkh);

        return View(orders);
    }
}

