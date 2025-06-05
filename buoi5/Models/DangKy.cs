using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace buoi5.Models
{
    public class DangKy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaDK { get; set; }

        public DateTime? NgayDK { get; set; }

        [StringLength(10)]
        [ForeignKey("SinhVien")]
        public string? MaSV { get; set; }

        // Navigation properties
        public virtual SinhVien? SinhVien { get; set; }
        public virtual ICollection<ChiTietDangKy> ChiTietDangKys { get; set; } = new List<ChiTietDangKy>();
    }
}
