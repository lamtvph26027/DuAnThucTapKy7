using App_banAo.Services;
using App_data.Models;
using Microsoft.AspNetCore.Mvc;

namespace App_banAo.Components
{
    public class CartWidget:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cartproducts = GioHangServices.GetObjFormSession(HttpContext.Session, "Cart");
           
            return View (cartproducts);
        }

    }
}
