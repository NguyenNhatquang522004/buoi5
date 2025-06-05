using buoi5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace buoi5.Controllers
{
    public class HocPhanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HocPhanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Kiểm tra đăng nhập
        private bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(HttpContext.Session.GetString("MaSV"));
        }

        public async Task<IActionResult> Index()
        {
            if (!IsLoggedIn())
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập để xem học phần!";
                return RedirectToAction("Login", "Account");
            }

            var hocPhans = await _context.HocPhans.ToListAsync();
            return View(hocPhans);
        }        [HttpPost]
        public async Task<IActionResult> AddToCart(string maHP)
        {
            if (!IsLoggedIn())
            {
                return Json(new { success = false, message = "Vui lòng đăng nhập!" });
            }

            // Kiểm tra học phần có tồn tại và còn chỗ không
            var hocPhan = await _context.HocPhans.FindAsync(maHP);
            if (hocPhan == null)
            {
                return Json(new { success = false, message = "Học phần không tồn tại!" });
            }

            if (hocPhan.SoLuongConLai <= 0)
            {
                return Json(new { success = false, message = "Học phần đã hết chỗ!" });
            }

            var cart = GetCartFromSession();
            if (!cart.Contains(maHP))
            {
                cart.Add(maHP);
                SetCartToSession(cart);
                return Json(new { success = true, message = "Đã thêm vào giỏ đăng ký!" });
            }
            else
            {
                return Json(new { success = false, message = "Học phần đã có trong giỏ!" });
            }
        }

        private List<string> GetCartFromSession()
        {
            var cart = HttpContext.Session.GetString("Cart");
            if (string.IsNullOrEmpty(cart))
                return new List<string>();
            
            return System.Text.Json.JsonSerializer.Deserialize<List<string>>(cart) ?? new List<string>();
        }

        private void SetCartToSession(List<string> cart)
        {
            var cartJson = System.Text.Json.JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString("Cart", cartJson);
        }
    }
}
