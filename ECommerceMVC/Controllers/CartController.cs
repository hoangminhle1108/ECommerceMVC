using ECommerceMVC.Data;
using Microsoft.AspNetCore.Mvc;
using ECommerceMVC.Helper;
using ECommerceMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
namespace ECommerceMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly Hshop2023Context db;
        public CartController(Hshop2023Context context)
        {
            db = context;
        }
        public List<GioHang> Cart => HttpContext.Session.Get<List<GioHang>> (MySetting.CART_KEY) ?? new List<GioHang> ();
        public IActionResult Index()
        {
            return View(Cart);
        }

        public IActionResult AddToCart (int id, int quantity =1)
        {
            var cart = Cart;
            var item = cart.SingleOrDefault(p => p.MaHH == id);
            if (item == null)
            {
                var hanghoa = db.HangHoas.SingleOrDefault(p => p.MaHh== id);
                if (hanghoa == null)
                {
                    TempData["Message"] = $"Không có sản phẩm yêu cầu";
                    return Redirect("/404");
                }
                item = new GioHang
                {
                    MaHH = hanghoa.MaHh,
                    TenHH = hanghoa.TenHh,
                    DonGia = hanghoa.DonGia ?? 0,
                    Hinh = hanghoa.Hinh ?? string.Empty,
                    Soluong= quantity,
                };
                cart.Add(item);
            }
            else
            {
                item.Soluong += quantity;
            }
            HttpContext.Session.Set(MySetting.CART_KEY, cart);
            return RedirectToAction ("Index");
        }
        public IActionResult RemoveCart(int id)
        {
            var cart = Cart;
            var item = cart.SingleOrDefault(p => p.MaHH == id);
            if ( item != null)
            {
                cart.Remove(item);
                HttpContext.Session.Set(MySetting.CART_KEY, cart);
            }
              return RedirectToAction ("Index");
        }
        [Authorize]
        public IActionResult CheckOut(int id)
        {
            if (Cart.Count == 0)
            {
                return Redirect("/");
            }
            return View(Cart);
        }
    }
}
