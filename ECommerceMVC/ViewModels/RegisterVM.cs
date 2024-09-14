using System.ComponentModel.DataAnnotations;

namespace ECommerceMVC.ViewModels
{
    public class RegisterVM
    {
        [Display(Name ="Tên Đăng Nhập")]
        [Required(ErrorMessage = "*")]
        [MaxLength(20,ErrorMessage ="Tối đa 20 kí tự thôi má!!")]          
        public string MaKh { get; set; }

        [Display(Name = "Mật Khẩu")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        [Display(Name = "Họ và Tên")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự thôi má!!")]
        public string HoTen { get; set; }

        public bool GioiTinh { get; set; } = true;
        [DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; }
        [Display(Name = "Địa Chỉ")]
        [MaxLength(60, ErrorMessage = "Tối đa 60 kí tự thôi má!!")]
        public string DiaChi { get; set; }
        [Display(Name = "Điện Thoại")]
        [MaxLength(24, ErrorMessage = "Tối đa 24 kí tự thôi má!!")]
        [RegularExpression(@"0[9875]\d{8}",ErrorMessage ="Chưa đúng định dạng di động Việt Nam")]
        public string DienThoai { get; set; }

        [EmailAddress (ErrorMessage = "Chưa đúng định dạng email")]
        public string Email { get; set; }

        public string Hinh { get; set; }
    }
}
