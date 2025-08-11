using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoesStore.InterfaceRepositories;
using ShoesStore.Models;
using ShoesStore.Models.Session;
using ShoesStore.Models.Vnpay;
using ShoesStore.Service.Vnpay;
using ShoesStore.ViewModels;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShoesStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        ISanpham sanphamrepo;
        IDongSanpham _product;
        ISize _size;
        ISanphamSize _tkho;
        IMau _mau; IKhuyenMai kmRepo;

        private readonly IVnPayService _vnPayService;

        public ShoppingCartController(ISanpham productDetail, IDongSanpham product, ISize sz, ISanphamSize tkho, IMau mau, IKhuyenMai kmRepo, IVnPayService vnPayService)
        {
            sanphamrepo = productDetail;
            _product = product;
            _size = sz;
            _tkho = tkho;
            _mau = mau;
            this.kmRepo = kmRepo;
            _vnPayService = vnPayService;
        }

       
        [HttpPost]
        public IActionResult CreatePaymentUrlVnpay(PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Json(url);
        }

        [HttpGet]
        public IActionResult VnPaySuccess([FromQuery] PaymentResponseModel response)
        {
            

            return View("VnPaySuccess", response);
        }

        [HttpGet]
        public IActionResult PaymentCallbackVnpay()  
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            if (response == null || response.VnPayResponseCode != "00")
            {
                // Thanh toán thất bại, chuyển hướng về giỏ hàng với thông báo lỗi
                TempData["Message"] = $"Lỗi thanh toán VNPAY: {response?.VnPayResponseCode}";
                return RedirectToAction("ViewCart");
            }

            // Thanh toán thành công
            // Xóa giỏ hàng khỏi session tại đây
            HttpContext.Session.Remove("Cart");

            // Chuyển hướng đến trang thành công với các tham số trên query string
            return RedirectToAction("VnPaySuccess", response);
            //return Json(response);
        }



        public IActionResult ViewCart()
        {
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            return View(cartItems);
        }

        [Route("ShoppingCart/AddToCart/{id}/{tenSize}/{slton}")]
        public IActionResult AddToCart(int id, string tenSize, int slton)
        {
            Sanpham sp = sanphamrepo.Getsanpham(id);

            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();
            var existingCartItem = cartItems.FirstOrDefault(item => item.sanpham.Masp == id && item.Size == tenSize);

            if (existingCartItem != null)
            {
                if (existingCartItem.Quantity <= slton - 1)
                {
                    existingCartItem.Quantity += 1;
                }
                else
                {
                    return RedirectToAction("HienThiSanPham", "SanPham", new { madongsanpham = sp.Madongsanpham, masp = sp.Masp });
                }
            }
            else
            {
                Dongsanpham dongSanPham = _product.GetDongSanpham(sp.Madongsanpham);
                Mau mau = _mau.GetMau(sp.Mamau);

                cartItems.Add(new ShoppingCartItem()
                {
                    sanpham = sp,
                    Name = dongSanPham.Tendongsp,
                    TenMau = mau.Tenmau,
                    Quantity = 1,
                    tonkho = slton,
                    GiaGoc = dongSanPham.Giagoc,
                    PhanTramGiam = kmRepo.GetKmProductToday(dongSanPham),
                    Size = tenSize,
                    Maspsize = _tkho.GetMaspsize(sp.Masp, tenSize)
                });
            }
            HttpContext.Session.Set("Cart", cartItems);

            return RedirectToAction("ViewCart");
        }

        public IActionResult IncreaseOne(int Masp, string size)
        {
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart");
            ShoppingCartItem shopCarteIncrease = cartItems
                .FirstOrDefault(x => x.sanpham.Masp == Masp && x.Size == size);

            if (shopCarteIncrease.tonkho >= shopCarteIncrease.Quantity + 1)
            {
                shopCarteIncrease.Quantity += 1;
            }

            HttpContext.Session.Set("Cart", cartItems);
            return PartialView("PartialCartList",cartItems);
        }

  
        public IActionResult DecreaseOne(int Masp, string size)
        {
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart");
            ShoppingCartItem shopCarteDecrease = cartItems
                .FirstOrDefault(x => x.sanpham.Masp == Masp && x.Size == size);

            shopCarteDecrease.Quantity -= 1;
            if (shopCarteDecrease.Quantity == 0)
            {
                cartItems.Remove(shopCarteDecrease);
            }
            HttpContext.Session.Set("Cart", cartItems);

            return PartialView("PartialCartList", cartItems);
        }

        public IActionResult Delete(int Masp, string tenSize)
        {

            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart");
            ShoppingCartItem shopCartNeedDelete = cartItems
                .FirstOrDefault(x => x.sanpham.Masp == Masp && x.Size == tenSize);

            if (shopCartNeedDelete != null)
            {
                cartItems.Remove(shopCartNeedDelete);
            }

            HttpContext.Session.Set("Cart", cartItems);

            return PartialView("PartialCartList", cartItems);

        }



    }
}
