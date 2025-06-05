using System.ComponentModel.DataAnnotations;

namespace buoi5.Models
{    public class HocPhan
    {
        [Key]
        [StringLength(6)]
        public string MaHP { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string TenHP { get; set; } = string.Empty;

        public int? SoTinChi { get; set; }

        [Display(Name = "Số lượng tối đa")]
        public int SoLuongToiDa { get; set; } = 50;

        [Display(Name = "Số lượng đã đăng ký")]
        public int SoLuongDaDangKy { get; set; } = 0;

        [Display(Name = "Số lượng còn lại")]
        public int SoLuongConLai => SoLuongToiDa - SoLuongDaDangKy;

        // Navigation property - THIS WAS MISSING
        public virtual ICollection<ChiTietDangKy> ChiTietDangKys { get; set; } = new List<ChiTietDangKy>();
    }

}

