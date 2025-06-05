using System.ComponentModel.DataAnnotations;

namespace buoi5.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập mã sinh viên")]
        [Display(Name = "Mã sinh viên")]
        public string MaSV { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Ghi nhớ đăng nhập")]
        public bool RememberMe { get; set; }
    }
}
