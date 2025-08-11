using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ShoesStore.Models.Authentication
{
    public class AuthenticationAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("Email") == null || context.HttpContext.Session.GetString("Loaitk") != "2")
            {   //Check có tài khoản đăng nhập không và đây có phải loại tài khoản admin không
                //Lấy cái session LoaiTK đã lưu trong account controller ra dùng để so sánh
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
