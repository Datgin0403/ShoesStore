using Microsoft.AspNetCore.Mvc;
using ShoesStore.Models;
using ShoesStore.Models.Authentication;
using System.Diagnostics;

namespace ShoesStore.Controllers
{
    [Area("Admin")]
    public class AccountAdminController : Controller
    {
        ShoesDbContext db = new ShoesDbContext();

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Email") != null || (HttpContext.Session.GetString("Loaitk") != "2" && HttpContext.Session.GetString("Loaitk") != "1"))
            {   //Check co tk hoac co phai la admin ko
                return View();
            }
            else
            {
                Debug.WriteLine("In login GET Redirect");
                return RedirectToAction("Index", "HomeAdmin");
                //Có rồi thì đi đến home admin 
            }
        }
        [HttpPost]
        public IActionResult Login(Taikhoan taikhoan)
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                var u = db.Taikhoans.Where(x => x.Email.Equals(taikhoan.Email) && x.Matkhau.Equals(taikhoan.Matkhau)
                            && (x.Loaitk==2 || x.Loaitk==1)).FirstOrDefault();
                //Check có tài khoản hay không đồng thời check luôn loại tài khoản đó là 1 hay 2 rồi mới cho đăng nhập
                if (u != null)
                {
                    HttpContext.Session.SetString("Email", u.Email);
                    HttpContext.Session.SetString("Loaitk",u.Loaitk.ToString());
                    //Lưu lại cái session LoaiTK để lát mình dùng trong authentication

                    
                    return RedirectToAction("Index", "HomeAdmin");
                    //Vì cái này là trong admin controller rồi chỉ có thể staff hoặc nhân viên đăng nhập  dùng chung HomeAdmin rồi
                }
            }
            return View(taikhoan);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("Email");
            return RedirectToAction("Login", "AccountAdmin");
        }	


	}
}
