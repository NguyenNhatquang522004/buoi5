
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace buoi5.Models
{
    public class SinhVien
    {
        [Key]
        [StringLength(10)]
        public string MaSV { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string HoTen { get; set; } = string.Empty;

        [StringLength(5)]
        public string? GioiTinh { get; set; }

        public DateTime? NgaySinh { get; set; }

        [StringLength(50)]
        public string? Hinh { get; set; }

        [StringLength(4)]
        [ForeignKey("NganhHoc")]
        public string? MaNganh { get; set; }

        // Navigation properties - THESE WERE MISSING
        public virtual NganhHoc? NganhHoc { get; set; }
        public virtual ICollection<DangKy> DangKys { get; set; } = new List<DangKy>();
    }
}
