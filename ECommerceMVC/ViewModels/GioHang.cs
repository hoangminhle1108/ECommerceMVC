namespace ECommerceMVC.ViewModels
{
    public class GioHang
    {
        public int MaHH { get; set; }
        public string Hinh { get; set; }
        public string TenHH { get; set; }
        public double DonGia { get; set; }
        public int Soluong { get; set; }
        public double ThanhTien => Soluong*DonGia;
    }
}
