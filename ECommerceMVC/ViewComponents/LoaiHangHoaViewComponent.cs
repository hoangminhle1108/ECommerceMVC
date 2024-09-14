using ECommerceMVC.Data;
using ECommerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMVC.ViewComponents
{
    public class LoaiHangHoaViewComponent: ViewComponent
    {
        private readonly Hshop2023Context db;
        public LoaiHangHoaViewComponent(Hshop2023Context _context) => db = _context;

        public IViewComponentResult Invoke()
        {
            var data = db.Loais.Select(lo => new LoaiHangHoaVM
            {
               Maloai = lo.MaLoai,
               TenLoai=  lo.TenLoai,
               Soluong = lo.HangHoas.Count,
            }).OrderBy(p => p.TenLoai);
            return View(data);
        }
    }
}
