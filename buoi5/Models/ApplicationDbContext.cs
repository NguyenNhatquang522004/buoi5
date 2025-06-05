using Microsoft.EntityFrameworkCore;

namespace buoi5.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<NganhHoc> NganhHocs { get; set; }
        public DbSet<SinhVien> SinhViens { get; set; }
        public DbSet<HocPhan> HocPhans { get; set; }
        public DbSet<DangKy> DangKys { get; set; }
        public DbSet<ChiTietDangKy> ChiTietDangKys { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<ChiTietDangKy>()
               .HasKey(c => new { c.MaDK, c.MaHP });

            // Configure relationships
            modelBuilder.Entity<SinhVien>()
                .HasOne(s => s.NganhHoc)
                .WithMany(n => n.SinhViens)
                .HasForeignKey(s => s.MaNganh)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<DangKy>()
                .HasOne(d => d.SinhVien)
                .WithMany(s => s.DangKys)
                .HasForeignKey(d => d.MaSV)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ChiTietDangKy>()
                .HasOne(c => c.DangKy)
                .WithMany(d => d.ChiTietDangKys)
                .HasForeignKey(c => c.MaDK)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ChiTietDangKy>()
                .HasOne(c => c.HocPhan)
                .WithMany(h => h.ChiTietDangKys)
                .HasForeignKey(c => c.MaHP)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<NganhHoc>().HasData(
              new NganhHoc { MaNganh = "CNTT", TenNganh = "Công nghệ thông tin" },
              new NganhHoc { MaNganh = "QTKD", TenNganh = "Quản trị kinh doanh" }
          );            modelBuilder.Entity<HocPhan>().HasData(
                new HocPhan { MaHP = "CNTT01", TenHP = "Lập trình C", SoTinChi = 3, SoLuongToiDa = 40, SoLuongDaDangKy = 0 },
                new HocPhan { MaHP = "CNTT02", TenHP = "Cơ sở dữ liệu", SoTinChi = 2, SoLuongToiDa = 35, SoLuongDaDangKy = 0 },
                new HocPhan { MaHP = "QTKD01", TenHP = "Kinh tế vi mô", SoTinChi = 2, SoLuongToiDa = 30, SoLuongDaDangKy = 0 },
                new HocPhan { MaHP = "QTKD02", TenHP = "Xác suất thống kê 1", SoTinChi = 3, SoLuongToiDa = 25, SoLuongDaDangKy = 0 }
            );            modelBuilder.Entity<SinhVien>().HasData(
                new SinhVien
                {
                    MaSV = "SV001",
                    HoTen = "Nguyễn Văn A",
                    GioiTinh = "Nam",
                    NgaySinh = new DateTime(2000, 12, 2),
                    Hinh = "/Content/images/sv1.svg",
                    MaNganh = "CNTT"
                },
                new SinhVien
                {
                    MaSV = "SV002",
                    HoTen = "Trần Thị B",
                    GioiTinh = "Nữ",
                    NgaySinh = new DateTime(2000, 3, 7),
                    Hinh = "/Content/images/sv2.svg",
                    MaNganh = "QTKD"
                },
                new SinhVien
                {
                    MaSV = "SV003",
                    HoTen = "Lê Văn C",
                    GioiTinh = "Nam",
                    NgaySinh = new DateTime(1999, 8, 15),
                    Hinh = "/Content/images/sv1.svg",
                    MaNganh = "CNTT"
                },
                // Giữ lại dữ liệu cũ để tương thích
                new SinhVien
                {
                    MaSV = "0123456789",
                    HoTen = "Nguyễn Văn A (Cũ)",
                    GioiTinh = "Nam",
                    NgaySinh = new DateTime(2000, 12, 2),
                    Hinh = "/Content/images/sv1.svg",
                    MaNganh = "CNTT"
                },
                new SinhVien
                {
                    MaSV = "9876543210",
                    HoTen = "Nguyễn Thị B (Cũ)",
                    GioiTinh = "Nữ",
                    NgaySinh = new DateTime(2000, 3, 7),
                    Hinh = "/Content/images/sv2.svg",
                    MaNganh = "QTKD"
                }
            );
        }
    }
}

