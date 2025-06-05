using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace buoi5.Models
{
    public class ChiTietDangKy
    {
        [ForeignKey("DangKy")]
        public int MaDK { get; set; }

        [StringLength(6)]
        [ForeignKey("HocPhan")]
        public string MaHP { get; set; } = string.Empty;

        // Navigation properties
        public virtual DangKy? DangKy { get; set; }
        public virtual HocPhan? HocPhan { get; set; }
    }
}
