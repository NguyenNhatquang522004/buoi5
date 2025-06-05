using System.ComponentModel.DataAnnotations;

namespace buoi5.Models
{
    public class NganhHoc
    {
        [Key]  // Primary key
        [StringLength(4)]
        public string MaNganh { get; set; } = string.Empty;

        [StringLength(30)]
        public string TenNganh { get; set; } = string.Empty;

        // Navigation property
        public virtual ICollection<SinhVien> SinhViens { get; set; } = new List<SinhVien>();
    }
}
