using buoi5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace buoi5.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra sinh viên có tồn tại không
                var sinhVien = await _context.SinhViens
                    .Include(s => s.NganhHoc)
                    .FirstOrDefaultAsync(s => s.MaSV == model.MaSV);
                
                if (sinhVien != null)
                {
                    // Lưu thông tin đăng nhập vào session
                    HttpContext.Session.SetString("MaSV", model.MaSV);
                    HttpContext.Session.SetString("HoTen", sinhVien.HoTen);
                    
                    TempData["SuccessMessage"] = "Đăng nhập thành công!";
                    return RedirectToAction("Index", "HocPhan");
                }
                else
                {
                    ModelState.AddModelError("", "Mã sinh viên không tồn tại!");
                }
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["SuccessMessage"] = "Đăng xuất thành công!";
            return RedirectToAction("Login");
        }
    }
}
