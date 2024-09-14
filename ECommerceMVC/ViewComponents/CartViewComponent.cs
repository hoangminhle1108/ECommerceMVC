using ECommerceMVC.Helper;
using ECommerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMVC.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<GioHang>>(MySetting.CART_KEY) ??
                new List<GioHang>();
            return View("CartPanel", new GioHangModel
            {
                    quantity = cart.Sum(p=>p.Soluong),
                    total = cart.Sum(p=>p.ThanhTien),
            });
        } 
        
    }
}
