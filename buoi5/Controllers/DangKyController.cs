using buoi5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace buoi5.Controllers
{
    public class DangKyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DangKyController(ApplicationDbContext context)
        {
            _context = context;
        }

        private bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(HttpContext.Session.GetString("MaSV"));
        }

        public async Task<IActionResult> ViewCart()
        {
            if (!IsLoggedIn())
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập!";
                return RedirectToAction("Login", "Account");
            }

            var cart = GetCartFromSession();
            var hocPhans = await _context.HocPhans
                .Where(h => cart.Contains(h.MaHP))
                .ToListAsync();

            return View(hocPhans);
        }

        [HttpPost]
        public IActionResult RemoveFromCart(string maHP)
        {
            var cart = GetCartFromSession();
            cart.Remove(maHP);
            SetCartToSession(cart);
            
            return Json(new { success = true, message = "Đã xóa khỏi giỏ đăng ký!" });
        }

        [HttpPost]
        public IActionResult ClearCart()
        {
            HttpContext.Session.Remove("Cart");
            return Json(new { success = true, message = "Đã xóa tất cả học phần!" });
        }        [HttpPost]
        public async Task<IActionResult> SaveRegistration()
        {
            if (!IsLoggedIn())
            {
                return Json(new { success = false, message = "Vui lòng đăng nhập!" });
            }

            var maSV = HttpContext.Session.GetString("MaSV");
            var cart = GetCartFromSession();

            if (!cart.Any())
            {
                return Json(new { success = false, message = "Giỏ đăng ký trống!" });
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Kiểm tra số lượng còn lại của các học phần
                var hocPhans = await _context.HocPhans
                    .Where(h => cart.Contains(h.MaHP))
                    .ToListAsync();

                var errors = new List<string>();
                foreach (var hp in hocPhans)
                {
                    if (hp.SoLuongConLai <= 0)
                    {
                        errors.Add($"Học phần {hp.TenHP} đã hết chỗ!");
                    }
                }

                if (errors.Any())
                {
                    return Json(new { success = false, message = string.Join("<br>", errors) });
                }

                // Tạo đăng ký mới
                var dangKy = new DangKy
                {
                    NgayDK = DateTime.Now,
                    MaSV = maSV
                };

                _context.DangKys.Add(dangKy);
                await _context.SaveChangesAsync();

                // Thêm chi tiết đăng ký và cập nhật số lượng
                foreach (var maHP in cart)
                {
                    var chiTiet = new ChiTietDangKy
                    {
                        MaDK = dangKy.MaDK,
                        MaHP = maHP
                    };
                    _context.ChiTietDangKys.Add(chiTiet);

                    // Cập nhật số lượng đã đăng ký
                    var hocPhan = hocPhans.First(h => h.MaHP == maHP);
                    hocPhan.SoLuongDaDangKy += 1;
                    _context.HocPhans.Update(hocPhan);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // Xóa giỏ hàng
                HttpContext.Session.Remove("Cart");

                return Json(new { success = true, message = "Đăng ký thành công!" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        public async Task<IActionResult> MyRegistrations()
        {
            if (!IsLoggedIn())
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập!";
                return RedirectToAction("Login", "Account");
            }

            var maSV = HttpContext.Session.GetString("MaSV");
            var dangKys = await _context.DangKys
                .Include(d => d.ChiTietDangKys)
                    .ThenInclude(c => c.HocPhan)
                .Where(d => d.MaSV == maSV)
                .OrderByDescending(d => d.NgayDK)
                .ToListAsync();

            return View(dangKys);
        }        [HttpPost]
        public async Task<IActionResult> DeleteRegistration(int maDK)
        {
            if (!IsLoggedIn())
            {
                return Json(new { success = false, message = "Vui lòng đăng nhập!" });
            }

            var maSV = HttpContext.Session.GetString("MaSV");
            var dangKy = await _context.DangKys
                .Include(d => d.ChiTietDangKys)
                    .ThenInclude(c => c.HocPhan)
                .FirstOrDefaultAsync(d => d.MaDK == maDK && d.MaSV == maSV);

            if (dangKy == null)
            {
                return Json(new { success = false, message = "Không tìm thấy đăng ký!" });
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Giảm số lượng đã đăng ký của các học phần
                foreach (var chiTiet in dangKy.ChiTietDangKys)
                {
                    if (chiTiet.HocPhan != null)
                    {
                        chiTiet.HocPhan.SoLuongDaDangKy = Math.Max(0, chiTiet.HocPhan.SoLuongDaDangKy - 1);
                        _context.HocPhans.Update(chiTiet.HocPhan);
                    }
                }

                _context.ChiTietDangKys.RemoveRange(dangKy.ChiTietDangKys);
                _context.DangKys.Remove(dangKy);
                
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Json(new { success = true, message = "Đã xóa đăng ký thành công!" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        private List<string> GetCartFromSession()
        {
            var cart = HttpContext.Session.GetString("Cart");
            if (string.IsNullOrEmpty(cart))
                return new List<string>();
            
            return System.Text.Json.JsonSerializer.Deserialize<List<string>>(cart) ?? new List<string>();
        }        private void SetCartToSession(List<string> cart)
        {
            var cartJson = System.Text.Json.JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString("Cart", cartJson);
        }
    }
}
