namespace ECommerceMVC.ViewModels
{
    public class HangHoaVM
    {
        public int maHH { get; set; }
        public string TenHH { get; set; }
        public string Hinh { get; set; }
        public double DonGia { get; set; }
        public string Mota { get; set; }
        public string TenLoai { get; set; }
    }
    public class ChiTietHangHoaVM
    {
        public int maHH { get; set; }
        public string TenHH { get; set; }
        public string Hinh { get; set; }
        public double DonGia { get; set; }
        public string Mota { get; set; }
        public string TenLoai { get; set; }
        public string ChiTiet { get; set; }
        public int DiemDanhGia { get; set; }
        public int SoLuongTon { get; set; }

    }
}
