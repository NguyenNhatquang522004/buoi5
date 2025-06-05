using System.ComponentModel.DataAnnotations;

namespace buoi5.Repository
{
    public class StudentViewModel
    {
        [Required(ErrorMessage = "Mã sinh viên là bắt buộc")]
        [StringLength(10, ErrorMessage = "Mã sinh viên không được quá 10 ký tự")]
        [Display(Name = "Mã sinh viên")]
        public string MaSV { get; set; } = string.Empty;

        [Required(ErrorMessage = "Họ tên là bắt buộc")]
        [StringLength(50, ErrorMessage = "Họ tên không được quá 50 ký tự")]
        [Display(Name = "Họ và tên")]
        public string HoTen { get; set; } = string.Empty;

        [Display(Name = "Giới tính")]
        public string? GioiTinh { get; set; }

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime? NgaySinh { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public string? Hinh { get; set; }

        [Display(Name = "Ngành học")]
        public string? MaNganh { get; set; }

        [Display(Name = "Tên ngành")]
        public string? TenNganh { get; set; }

        [Display(Name = "File ảnh")]
        public IFormFile? ImageFile { get; set; }
    }
}
