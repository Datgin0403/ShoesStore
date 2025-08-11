using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ShoesStore.Models.Authentication
{
	public class AuthenticationCustomer : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("Email") == null || context.HttpContext.Session.GetString("Loaitk") != "0")
            {   //Check có tài khoản đăng nhập không và đây có phải loại tài khoản Customer không
                //Lấy cái session LoaiTK đã lưu trong account controller ra dùng để so sánh
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"Controller", "Account" },
                    {"Action", "Login" },
                });
            }
        }
       

    }
}
