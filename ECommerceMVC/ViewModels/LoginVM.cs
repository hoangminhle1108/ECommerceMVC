using System.ComponentModel.DataAnnotations;

namespace ECommerceMVC.ViewModels
{
    public class LoginVM
    {
        [Display(Name ="Username")]
        [Required(ErrorMessage ="YÊU CẦU TÊN ĐĂNG NHẬP")]
        [MaxLength(20,ErrorMessage ="Tối đa 20 ký tự ")]
        public string UserName { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "YÊU CẦU MẬT KHẨU")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }  
}
