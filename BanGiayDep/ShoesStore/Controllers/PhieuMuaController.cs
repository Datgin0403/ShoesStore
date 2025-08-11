using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoesStore.InterfaceRepositories;
using ShoesStore.Models;
using ShoesStore.Models.Authentication;
using ShoesStore.Models.Session;
using ShoesStore.Repositories;
using ShoesStore.ViewModels;
using System.Diagnostics;
using System.Security.Principal;

namespace ShoesStore.Controllers
{
    public class PhieuMuaController : Controller
    {
        IKhachhang khRepo; IPhuongthucthanhtoan pttt; ISanphamSize spsize; IPhieuMua pm; IEmailSender emailSender; IAddressNoteBook addressRepo;
        IVoucher voucherRepo; ISanpham sanphamrepo; IDongSanpham _product; IMau _mau; IKhuyenMai kmRepo; ISanphamSize _tkho;
        public PhieuMuaController(IKhachhang kh, IPhuongthucthanhtoan pt, ISanphamSize sizesp, IPhieuMua pm2, IEmailSender email
            , IAddressNoteBook addressRepo, IVoucher voucherRepo, ISanpham sanphamrepo, IDongSanpham product, IMau mau, IKhuyenMai kmRepo
            , ISanphamSize tkho)
        {
            this.khRepo = kh;
            this.pttt = pt;
            spsize = sizesp;
            pm = pm2;
            emailSender = email;
            this.addressRepo = addressRepo;
            this.voucherRepo = voucherRepo;
            this.sanphamrepo = sanphamrepo;
            _product = product;
            _mau = mau;
            this.kmRepo = kmRepo;
            _tkho = tkho;
        }

        public IActionResult ThanhToan()
        {
            if (HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") == null)
            {
                return RedirectToAction("SanPhamTheoLoai", "SanPham", new { maloai = -1 });
            }
            //if (HttpContext.Session.GetString("Email") == null || HttpContext.Session.GetString("Loaitk") != "0")
            //{
            //    TempData["ThanhToan"] = "Please login before checkout";
            //    return RedirectToAction("Login", "Account");
            //}
            string emailkh = HttpContext.Session.GetString("Email") ?? "";
            Khachhang kh = khRepo.GetCurrentKh(emailkh);
            List<ShoppingCartItem> cartItem = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();
           
            decimal total = 0;
            foreach (ShoppingCartItem shopcartItem in cartItem)
            {
                decimal tiensp;
                if (shopcartItem.PhanTramGiam != 1)
                {
                    tiensp = shopcartItem.Quantity * (shopcartItem.GiaGoc - (shopcartItem.GiaGoc * shopcartItem.PhanTramGiam / 100));
                }
                else
                {
                    tiensp = shopcartItem.Quantity * shopcartItem.GiaGoc;
                }
                total += tiensp;
            }
            decimal coin = Math.Ceiling(total / 100000);
            coin = coin * 1000;
            PhieuMuaViewModel phieuMua = new PhieuMuaViewModel()
            {
                listcartItem = cartItem,
                khInfo = kh,
                HoTen = kh?.Tenkh ?? "",
                Sdt = kh?.Sdt ?? "",
                Email = kh?.Email ?? "",
                Mapttt = 0,
                voucherList = voucherRepo.getAllVoucherToday(),
              //  sodiachis = addressRepo.GetAllAddressNoteByMaKH(kh.Makh),
                sodiachis = kh != null ? addressRepo.GetAllAddressNoteByMaKH(kh.Makh) : addressRepo.GetAllAddressNote(),
                totalCost = total,
                tempCost = total,
                Choosenvoucher = null,
                coinGet = coin,
                discountMoney = 0,
                coinChoosen = 0,
                coinApply = 0
            };
            
            ViewBag.MethodPurchase = pttt.GetAllPttt();
            CreateSelectAddress();
            return View(phieuMua);
        }

        public IActionResult ChooseVoucherAndApplyXu(string? mavoucher, bool? Boolcheck, decimal? AmountXu, int? changeAmount, bool? Apply,decimal? AmountApply)
        {
            string emailkh = HttpContext.Session.GetString("Email") ;
            decimal voucherApplyCode = 0, maxDiscount = 0;
            List<Voucher> voucherList = voucherRepo.getAllVoucherToday();
            Khachhang kh = khRepo.GetCurrentKh(emailkh);
            List<ShoppingCartItem> cartItem = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();
            decimal total = 0;

            foreach (ShoppingCartItem shopcartItem in cartItem)
            {
                decimal tiensp;
                if (shopcartItem.PhanTramGiam != 1)
                {
                    tiensp = shopcartItem.Quantity * (shopcartItem.GiaGoc - (shopcartItem.GiaGoc * shopcartItem.PhanTramGiam / 100));
                }
                else
                {
                    tiensp = shopcartItem.Quantity * shopcartItem.GiaGoc;
                    voucherApplyCode += tiensp;
                }
                total += tiensp;
            }
            decimal tempCost = total;

            Voucher voucher = new Voucher();
            if (!string.IsNullOrEmpty(mavoucher))
            {
                voucher = voucherRepo.GetVoucherByCode(mavoucher);
                if (voucherApplyCode >= voucher.Giatoithieu)
                {
                    voucherList.FirstOrDefault(x => x.Mavoucher == mavoucher).Soluong -= 1;
                    total -= voucher.Giamtoida;
                    ViewBag.voucherCode = mavoucher;
                    maxDiscount += voucher.Giamtoida;
                }
                else
                {
                    string messageError = "Mã giảm  " + mavoucher + " chỉ áp dụng cho đơn hàng " + voucher.Giatoithieu.ToString("#,##0") + " VND";
                    ViewBag.voucherError = messageError;
                }
            }
            if (changeAmount == 1)
            {
                if(AmountXu <= kh.Tongxu - 10000m)
                {
                    AmountXu += 10000m;
                }
            }
            else if(changeAmount==2)
            {
                if(AmountXu >= 10000m)
                {
                    AmountXu -= 10000m;
                }
            }
            ViewBag.xuTemp = AmountXu;
            ViewBag.BoolCheck = Boolcheck;
            if(Apply == true)
            {
                AmountApply = AmountXu;
            }

            ViewBag.AmountApply = AmountApply;
            total -= AmountApply ?? 0;
            maxDiscount += AmountApply ?? 0;
            ViewBag.voucherDiscount = maxDiscount;
            decimal coin = Math.Ceiling(total / 100000);
            coin = coin * 1000;
            PhieuMuaViewModel phieuMua = new PhieuMuaViewModel()
            {
                listcartItem = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>(),
                khInfo = kh,
                Mapttt = 0,
                voucherList = voucherList,
                Choosenvoucher = string.IsNullOrEmpty(mavoucher) ? null : voucher,
                sodiachis = addressRepo.GetAllAddressNote(),
                totalCost = total,
                tempCost = tempCost,
                coinGet = coin,
                discountMoney = maxDiscount,
                coinChoosen = AmountXu ?? 0,
                coinApply = AmountApply ?? 0,
            };



            return PartialView("_PartialViewPriceDetail", phieuMua);
        }

        /*
        [HttpPost]
        public IActionResult ThanhToan(PhieuMuaViewModel phieuMua)
        {

            phieuMua.listcartItem = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();
            ViewBag.MethodPurchase = pttt.GetAllPttt();
            string emailkh = HttpContext.Session.GetString("Email");
            Khachhang kh = khRepo.GetCurrentKh(emailkh);

            CreateSelectAddress();
            
            phieuMua.khInfo = kh;

            if (Check(phieuMua) == true)
            {
                phieuMua.sodiachis = addressRepo.GetAllAddressNote();
                phieuMua.voucherList = voucherRepo.getAllVoucherToday();
                ViewBag.xuTemp = phieuMua.coinChoosen;
                ViewBag.AmountApply = phieuMua.coinApply;
                if (phieuMua.maTinh != 0)
                {
                    
                    List<Quan> quanList = addressRepo.GetQuanList(phieuMua.maTinh);
                    List<SelectListItem> selectQuan = quanList.Select(x => new SelectListItem
                    {
                        Value = x.Maquan.ToString(),
                        Text = x.Tenquan
                    }).ToList();
                    ViewBag.QuanSelect = selectQuan;
                }

                if (phieuMua.maQuan != 0)
                {
                    List<Phuong> phuongList = addressRepo.GetPhuongList(phieuMua.maQuan);
                    List<SelectListItem> selectPhuong = phuongList.Select(x => new SelectListItem
                    {
                        Value = x.Maphuong.ToString(),
                        Text = x.Tenphuong
                    }).ToList();
                    ViewBag.PhuongSelect = selectPhuong;
                }

                if (phieuMua.coinChoosen != 0)
                {
                    ViewBag.BoolCheck = true;
                }

                return View(phieuMua);
            }

            spsize.MinusSanPhamSize(phieuMua);
            int mapm = pm.AddPhieuMua(phieuMua);
            //khRepo.UpdateKh(phieuMua.khInfo);

            HttpContext.Session.Remove("Cart");

            string receiver = "lephat011104@gmail.com";
            string subject = "New Order Success";
            string message = "ORDER ID: ";
            emailSender.SendEmail(receiver, subject, message);

            return RedirectToAction("Confirmation", "PhieuMua", new {mapm = mapm});
        }
        */
        [HttpPost]
        public IActionResult ThanhToan(PhieuMuaViewModel phieuMua)
        {
            // Lấy giỏ hàng từ Session
            phieuMua.listcartItem = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            // Kiểm tra giỏ hàng rỗng
            if (!phieuMua.listcartItem.Any())
            {
                ModelState.AddModelError("", "Giỏ hàng của bạn đang trống. Vui lòng thêm sản phẩm trước khi thanh toán.");
            }

            // Lấy thông tin khách hàng
            string emailkh = HttpContext.Session.GetString("Email");
            phieuMua.khInfo = khRepo.GetCurrentKh(emailkh);

            // Tái tạo danh sách dropdown
            CreateSelectAddress();
            ViewBag.MethodPurchase = pttt.GetAllPttt();

            // Nếu maTinh có giá trị, tạo danh sách Quận
            if (phieuMua.maTinh.HasValue && phieuMua.maTinh.Value != 0)
            {
                ViewBag.QuanSelect = addressRepo.GetQuanList(phieuMua.maTinh.Value)
                    .Select(x => new SelectListItem { Value = x.Maquan.ToString(), Text = x.Tenquan }).ToList();
            }
            else
            {
                ViewBag.QuanSelect = new List<SelectListItem> { new SelectListItem { Value = "", Text = "Chọn quận/huyện" } };
            }

            // Nếu maQuan có giá trị, tạo danh sách Phường
            if (phieuMua.maQuan.HasValue && phieuMua.maQuan.Value != 0)
            {
                ViewBag.PhuongSelect = addressRepo.GetPhuongList(phieuMua.maQuan.Value)
                    .Select(x => new SelectListItem { Value = x.Maphuong.ToString(), Text = x.Tenphuong }).ToList();
            }
            else
            {
                ViewBag.PhuongSelect = new List<SelectListItem> { new SelectListItem { Value = "", Text = "Chọn phường/xã" } };
            }

            // Gán các thông tin khác
            phieuMua.sodiachis = addressRepo.GetAllAddressNote();
            phieuMua.voucherList = voucherRepo.getAllVoucherToday();
            ViewBag.xuTemp = phieuMua.coinChoosen;
            ViewBag.AmountApply = phieuMua.coinApply;
            ViewBag.BoolCheck = phieuMua.coinChoosen != 0;

            // Kiểm tra validation
            if (!ModelState.IsValid || !Check(phieuMua))
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Debug.WriteLine($"Validation Error: {error.ErrorMessage}");
                }
                return View(phieuMua);
            }

            try
            {
                // Lưu thông tin đơn hàng
                spsize.MinusSanPhamSize(phieuMua);
                int mapm = pm.AddPhieuMua(phieuMua);

                // Xóa giỏ hàng khỏi session
                HttpContext.Session.Remove("Cart");

                // Gửi email cho quản trị viên
                string receiver = "lephat011104@gmail.com";
                string subject = "New Order Success";
                string message = $"ORDER ID: {mapm}";
                emailSender.SendEmail(receiver, subject, message);

                // Gửi email xác nhận cho khách hàng
                if (!string.IsNullOrEmpty(emailkh))
                {
                    string customerMessage = $"Xin chào {phieuMua.khInfo?.Tenkh},\n\n"
                                           + $"Cảm ơn bạn đã đặt hàng tại ShoesStore!\n"
                                           + $"Mã đơn hàng của bạn là: {mapm}\n"
                                           + $"Tổng số tiền thanh toán: {phieuMua.totalCost.ToString("#,##0")}₫.\n"
                                           + $"Chúng tôi sẽ liên hệ để xác nhận đơn hàng trong thời gian sớm nhất.\n\n"
                                           + $"Trân trọng,\nĐội ngũ ShoesStore";

                    emailSender.SendEmail(emailkh, "Xác nhận đơn hàng", customerMessage);
                }

                return RedirectToAction("Confirmation", "PhieuMua", new { mapm = mapm });
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(phieuMua);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi xử lý đơn hàng. Vui lòng thử lại.");
                return View(phieuMua);
            }
        }
        public IActionResult AddressNoteBook()
        {

            PhieuMuaViewModel pm = new PhieuMuaViewModel();
            List<Tinh> tinhList = addressRepo.GetTinhList();
            List<SelectListItem> selectTinh = tinhList.Select(x => new SelectListItem
            {
                Value = x.Matinh.ToString(),
                Text = x.Tentinh
            }).ToList();

            ViewBag.SelectTinh = selectTinh;

            Khachhang kh = khRepo.GetCurrentKh("lephat@gmail.com");
            pm.khInfo = kh;

            pm.sodiachis = addressRepo.GetAllAddressNote();

            return View(pm);
        }

        public IActionResult AddAddress(int province, int district, int ward, string address, int makh, string tennguoinhan, string sdtnguoinhan)
        {
            addressRepo.AddAddressNote(province, district, ward, address, makh, tennguoinhan, sdtnguoinhan);
            return RedirectToAction("AddressNoteBook");
        }

        public IActionResult GetDistinctsByProvince(int provinceId)
        {
            List<Quan> quanList = addressRepo.GetQuanList(provinceId);
            List<SelectListItem> selectQuan = quanList.Select(x => new SelectListItem
            {
                Value = x.Maquan.ToString(),
                Text = x.Tenquan
            }).ToList();
            return Json(selectQuan);
        }

        public IActionResult GetWardByDistrict(int districtId)
        {
            List<Phuong> phuongList = addressRepo.GetPhuongList(districtId);
            List<SelectListItem> selectPhuong = phuongList.Select(x => new SelectListItem
            {
                Value = x.Maphuong.ToString(),
                Text = x.Tenphuong
            }).ToList();
            return Json(selectPhuong);
        }

        public IActionResult Confirmation(int mapm)
        {
            return View(mapm);
        }

        public void CreateSelectAddress()
        {
            List<Tinh> tinhs = addressRepo.GetTinhList();
            List<SelectListItem> selectTinh = tinhs.Select(x => new SelectListItem
            {
                Value = x.Matinh.ToString(),
                Text = x.Tentinh
            }).ToList();
            ViewBag.TinhSelect = selectTinh;
            ViewBag.QuanSelect = null;
            ViewBag.PhuongSelect = null;
        }

        public IActionResult UpdateAddress(int masdc)
        {

            Sodiachi sdc = addressRepo.GetSodiachi(masdc);
            int maTinh = addressRepo.GetMaTinh(sdc.Diachi.Split(',')[1].TrimStart());
            int maQuan = addressRepo.GetMaQuan(sdc.Diachi.Split(',')[2].TrimStart());
            int maPhuong = addressRepo.GetMaPhuong(sdc.Diachi.Split(',')[3].TrimStart());

            List<Tinh> tinhs = addressRepo.GetTinhList();
            List<SelectListItem> selectTinh = tinhs.Select(x => new SelectListItem
            {
                Value = x.Matinh.ToString(),
                Text = x.Tentinh
            }).ToList();
          
            List<Quan> quanList = addressRepo.GetQuanList(maTinh);
            List<SelectListItem> selectQuan = quanList.Select(x => new SelectListItem
            {
                Value = x.Maquan.ToString(),
                Text = x.Tenquan
            }).ToList();
           

            List<Phuong> phuongList = addressRepo.GetPhuongList(maQuan);
            List<SelectListItem> selectPhuong = phuongList.Select(x => new SelectListItem
            {
                Value = x.Maphuong.ToString(),
                Text = x.Tenphuong
            }).ToList();
          

            UpdateAddressViewModel addView = new UpdateAddressViewModel()
            {
                Masdc = masdc,
                Tennguoinhan = sdc.Tennguoinhan,
                Sdtnguoinhan = sdc.Sdtnguoinhan,
                Diachi = sdc.Diachi.Split(',')[0],
                MaTinh = maTinh,
                MaQuan = maQuan,
                MaPhuong = maPhuong,
                tinhSelect = selectTinh,
                quanSelect = selectQuan,
                phuongSelect = selectPhuong
            };
            return Json(addView);
        }

        public IActionResult UpdateAddressFinal(int masdc,string hoten,string sdt,string diachi,int matinh,int maquan,int maphuong)
        {
            addressRepo.UpdateSDC(masdc, hoten, sdt, diachi, matinh, maquan, maphuong);
            List<Sodiachi> sdcList = addressRepo.GetAllAddressNote();

            return PartialView("AddressBookPartialView",sdcList);

        }
        
        public IActionResult DeleteAddress(int masdc)
        {
            addressRepo.DeleteSDC(masdc);
            string emailkh = HttpContext.Session.GetString("Email") ?? "lephat@gmail.com";
            Khachhang kh = khRepo.GetCurrentKh(emailkh);
            List<Sodiachi> sdcList = addressRepo.GetAllAddressNoteByMaKH(kh.Makh);

            return PartialView("AddressBookPartialView", sdcList);
        }

        public bool Check(PhieuMuaViewModel phieuMua)
        {
            bool check = false;
            //if (string.IsNullOrEmpty(phieuMua.khInfo.Diachi))
            //{
            //    Debug.WriteLine("Get in thanh toan post in check null");
            //    ModelState.AddModelError("khInfo.Diachi", "Vui lòng nhập địa chỉ");
            //    check = true;
            //}
            if (phieuMua.maTinh == 0 )
            {
                ModelState.AddModelError("maTinh", "Please choose province");
                check = true;
            }
            if (phieuMua.maQuan == 0)
            {
                ModelState.AddModelError("maQuan", "Please choose district");
                check = true;
            }
            if (phieuMua.maPhuong == 0)
            {
                ModelState.AddModelError("maPhuong", "Please choose ward");
                check = true;
            }
            if (phieuMua.Mapttt == null || phieuMua.Mapttt == 0)
            {
                ModelState.AddModelError("Mapttt", "Please choose method purchase");
                check = true;
            }
            return check;
        }

        /*
        public IActionResult ChangeAddressPhieuMua(int masodiachi)
        {
            Sodiachi sdc = addressRepo.GetSodiachi(masodiachi);
            string emailkh = HttpContext.Session.GetString("Email") ?? "lephat@gmail.com";
            Khachhang kh = khRepo.GetCurrentKh(emailkh);

            PhieuMuaViewModel pmview = new PhieuMuaViewModel()
            {
                HoTen = sdc.Tennguoinhan,
                Sdt = sdc.Sdtnguoinhan,
                Diachi = sdc.Diachi.Split(',')[0],
                maTinh = addressRepo.GetMaTinh(sdc.Diachi.Split(',')[1].TrimStart()),
                maQuan = addressRepo.GetMaQuan(sdc.Diachi.Split(',')[2].TrimStart()),
                maPhuong = addressRepo.GetMaPhuong(sdc.Diachi.Split(',')[3].TrimStart())

            };

            List<Quan> quanList = addressRepo.GetQuanList(pmview.maTinh);
            List<SelectListItem> selectQuan = quanList.Select(x => new SelectListItem
            {
                Value = x.Maquan.ToString(),
                Text = x.Tenquan
            }).ToList();
            pmview.selectQuan = selectQuan;

            List<Phuong> phuongList = addressRepo.GetPhuongList(pmview.maQuan);
            List<SelectListItem> selectPhuong = phuongList.Select(x => new SelectListItem
            {
                Value = x.Maphuong.ToString(),
                Text = x.Tenphuong
            }).ToList();
            pmview.selectPhuong = selectPhuong;
            return Json(pmview);
        }
        */


        public IActionResult ChangeAddressPhieuMua(int masodiachi)
        {
            Sodiachi sdc = addressRepo.GetSodiachi(masodiachi);
            string emailkh = HttpContext.Session.GetString("Email") ?? "lephat@gmail.com";
            Khachhang kh = khRepo.GetCurrentKh(emailkh);

            var diachiParts = sdc.Diachi.Split(',');
            if (diachiParts.Length < 4)
            {
                // Trường hợp địa chỉ không hợp lệ
                return Json(new { success = false, message = "Địa chỉ không hợp lệ" });
            }

            PhieuMuaViewModel pmview = new PhieuMuaViewModel()
            {
                HoTen = sdc.Tennguoinhan,
                Sdt = sdc.Sdtnguoinhan,
                Diachi = diachiParts[0],
                maTinh = addressRepo.GetMaTinh(diachiParts[1].TrimStart()),
                maQuan = addressRepo.GetMaQuan(diachiParts[2].TrimStart()),
                maPhuong = addressRepo.GetMaPhuong(diachiParts[3].TrimStart())
            };

            List<Quan> quanList = addressRepo.GetQuanList(pmview.maTinh.Value);
            List<SelectListItem> selectQuan = quanList.Select(x => new SelectListItem
            {
                Value = x.Maquan.ToString(),
                Text = x.Tenquan
            }).ToList();
            pmview.selectQuan = selectQuan;

            List<Phuong> phuongList = addressRepo.GetPhuongList(pmview.maQuan.Value);
            List<SelectListItem> selectPhuong = phuongList.Select(x => new SelectListItem
            {
                Value = x.Maphuong.ToString(),
                Text = x.Tenphuong
            }).ToList();
            pmview.selectPhuong = selectPhuong;

            return Json(pmview);
        }

        public IActionResult OrderDetail(int id)
        {
            var order = pm.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }




    }
}
