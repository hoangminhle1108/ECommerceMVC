using ECommerceMVC.Data;
using ECommerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly Hshop2023Context db;
        public HangHoaController(Hshop2023Context context) => db = context;
        public IActionResult Index(int? loai)
        {
            var hangHoas = db.HangHoas.AsQueryable();
            if (loai.HasValue)
            {
                hangHoas = hangHoas.Where(p => p.MaLoai == loai.Value);
            }
            var result = hangHoas.Select(p => new HangHoaVM
            {
                maHH = p.MaHh,
                TenHH = p.TenHh,
                DonGia = p.DonGia ?? 0,
                Hinh = p.Hinh ?? "",
                Mota = p.MoTaDonVi ?? "",
                TenLoai = p.MaLoaiNavigation.TenLoai
            });
            return View(result);
        }
        public IActionResult Search(string? query)
        {
            var hangHoas = db.HangHoas.AsQueryable();
            if (query != null)
            {
                hangHoas = hangHoas.Where(p => p.TenHh.Contains(query));
            }
            var result = hangHoas.Select(p => new HangHoaVM
            {
                maHH = p.MaHh,
                TenHH = p.TenHh,
                DonGia = p.DonGia ?? 0,
                Hinh = p.Hinh ?? "",
                Mota = p.MoTaDonVi ?? "",
                TenLoai = p.MaLoaiNavigation.TenLoai
            });
            return View(result);
        }
        public IActionResult Detail (int id)
        {
            var data = db.HangHoas.Include(p=>p.MaLoaiNavigation).SingleOrDefault(p => p.MaHh == id);
            if (data == null)
            {
                TempData["Message"]= $"Không thấy sản phẩm yêu cầu";
                return Redirect("/404");
            }
            var result = new ChiTietHangHoaVM
            {
                maHH = data.MaHh,
                TenHH = data.TenHh,
                DonGia= data.DonGia ?? 0,
                ChiTiet = data.MoTa ?? String.Empty,
                Hinh= data.Hinh ?? String.Empty,
                Mota = data.MoTaDonVi,
                TenLoai = data.MaLoaiNavigation.TenLoai,
                SoLuongTon= 10,
                DiemDanhGia = 5

            };
            return View(result);
        }
    }
}
