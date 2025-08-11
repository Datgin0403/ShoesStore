using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ShoesStore.Models.Authentication
{
    public class AuthenticationM_S : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var loaiTK = context.HttpContext.Session.GetString("Loaitk");
            if (context.HttpContext.Session.GetString("Email") == null || (loaiTK != "1" && loaiTK != "2"))
            {   // Kiểm tra có tài khoản đăng nhập không và đây có phải loại tài khoản Staff hoặc Manager không
                // Lấy cái session LoaiTK đã lưu trong account controller ra dùng để so sánh
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"Controller", "AccountAdmin" },
                    {"Action", "Login" },
                    {"Area","Admin" }

                });
            }
        }
    }
}
