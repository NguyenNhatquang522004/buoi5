// Controllers/SinhVienController.cs - Using Service Layer
using buoi5.Models;
using buoi5.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace buoi5.Controllers
{
    public class SinhVienController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStudentService _studentService;

        public SinhVienController(ApplicationDbContext context, IStudentService studentService)
        {
            _context = context;
            _studentService = studentService;
        }

        // GET: SinhVien
        public async Task<IActionResult> Index(string searchString, string sortOrder, string genderFilter, string nganhFilter)
        {
            // Sorting parameters
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["MaSVSortParm"] = sortOrder == "MaSV" ? "masv_desc" : "MaSV";

            // Filter parameters
            ViewData["CurrentFilter"] = searchString;
            ViewData["GenderFilter"] = genderFilter;
            ViewData["NganhFilter"] = nganhFilter;

            // Get data with includes - using direct context for search/filter functionality
            var sinhViens = from s in _context.SinhViens.Include(s => s.NganhHoc)
                            select s;

            // Search functionality
            if (!String.IsNullOrEmpty(searchString))
            {
                sinhViens = sinhViens.Where(s => s.HoTen.Contains(searchString)
                                               || s.MaSV.Contains(searchString));
            }

            // Gender filter
            if (!String.IsNullOrEmpty(genderFilter) && genderFilter != "Tất cả")
            {
                sinhViens = sinhViens.Where(s => s.GioiTinh == genderFilter);
            }

            // Nganh filter
            if (!String.IsNullOrEmpty(nganhFilter) && nganhFilter != "Tất cả")
            {
                sinhViens = sinhViens.Where(s => s.MaNganh == nganhFilter);
            }

            // Sorting
            switch (sortOrder)
            {
                case "name_desc":
                    sinhViens = sinhViens.OrderByDescending(s => s.HoTen);
                    break;
                case "Date":
                    sinhViens = sinhViens.OrderBy(s => s.NgaySinh);
                    break;
                case "date_desc":
                    sinhViens = sinhViens.OrderByDescending(s => s.NgaySinh);
                    break;
                case "MaSV":
                    sinhViens = sinhViens.OrderBy(s => s.MaSV);
                    break;
                case "masv_desc":
                    sinhViens = sinhViens.OrderByDescending(s => s.MaSV);
                    break;
                default:
                    sinhViens = sinhViens.OrderBy(s => s.HoTen);
                    break;
            }

            // Populate filter dropdowns
            ViewBag.GenderList = new SelectList(new[]
            {
                new { Value = "", Text = "Tất cả" },
                new { Value = "Nam", Text = "Nam" },
                new { Value = "Nữ", Text = "Nữ" }
            }, "Value", "Text", genderFilter);

            var nganhList = await _context.NganhHocs.ToListAsync();
            var nganhSelectList = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Tất cả" }
            };
            nganhSelectList.AddRange(nganhList.Select(n => new SelectListItem
            {
                Value = n.MaNganh,
                Text = n.TenNganh
            }));
            ViewBag.NganhList = new SelectList(nganhSelectList, "Value", "Text", nganhFilter);

            return View(await sinhViens.ToListAsync());
        }

        // GET: SinhVien/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhVien = await _studentService.GetStudentByIdAsync(id);
            if (sinhVien == null)
            {
                return NotFound();
            }

            return View(sinhVien);
        }

        // GET: SinhVien/Create
        public async Task<IActionResult> Create()
        {
            ViewData["MaNganh"] = new SelectList(await _context.NganhHocs.ToListAsync(), "MaNganh", "TenNganh");
            ViewData["GioiTinh"] = new SelectList(new[]
            {
                new { Value = "Nam", Text = "Nam" },
                new { Value = "Nữ", Text = "Nữ" }
            }, "Value", "Text");

            return View();
        }

        // POST: SinhVien/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSV,HoTen,GioiTinh,NgaySinh,Hinh,MaNganh")] SinhVien sinhVien, IFormFile? imageFile)
        {
            // Check if MaSV already exists
            if (await _studentService.StudentExistsAsync(sinhVien.MaSV))
            {
                ModelState.AddModelError("MaSV", "Mã sinh viên đã tồn tại!");
            }

            if (ModelState.IsValid)
            {
                var result = await _studentService.CreateStudentAsync(sinhVien, imageFile);
                if (result)
                {
                    TempData["SuccessMessage"] = "Thêm sinh viên thành công!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi thêm sinh viên!");
                }
            }

            ViewData["MaNganh"] = new SelectList(await _context.NganhHocs.ToListAsync(), "MaNganh", "TenNganh", sinhVien.MaNganh);
            ViewData["GioiTinh"] = new SelectList(new[]
            {
                new { Value = "Nam", Text = "Nam" },
                new { Value = "Nữ", Text = "Nữ" }
            }, "Value", "Text", sinhVien.GioiTinh);

            return View(sinhVien);
        }

        // GET: SinhVien/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhVien = await _context.SinhViens.FindAsync(id);
            if (sinhVien == null)
            {
                return NotFound();
            }

            ViewData["MaNganh"] = new SelectList(await _context.NganhHocs.ToListAsync(), "MaNganh", "TenNganh", sinhVien.MaNganh);
            ViewData["GioiTinh"] = new SelectList(new[]
            {
                new { Value = "Nam", Text = "Nam" },
                new { Value = "Nữ", Text = "Nữ" }
            }, "Value", "Text", sinhVien.GioiTinh);

            return View(sinhVien);
        }

        // POST: SinhVien/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaSV,HoTen,GioiTinh,NgaySinh,Hinh,MaNganh")] SinhVien sinhVien, IFormFile? imageFile)
        {
            if (id != sinhVien.MaSV)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _studentService.UpdateStudentAsync(sinhVien, imageFile);
                if (result)
                {
                    TempData["SuccessMessage"] = "Cập nhật sinh viên thành công!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật sinh viên!");
                }
            }

            ViewData["MaNganh"] = new SelectList(await _context.NganhHocs.ToListAsync(), "MaNganh", "TenNganh", sinhVien.MaNganh);
            ViewData["GioiTinh"] = new SelectList(new[]
            {
                new { Value = "Nam", Text = "Nam" },
                new { Value = "Nữ", Text = "Nữ" }
            }, "Value", "Text", sinhVien.GioiTinh);

            return View(sinhVien);
        }

        // GET: SinhVien/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhVien = await _studentService.GetStudentByIdAsync(id);
            if (sinhVien == null)
            {
                return NotFound();
            }

            return View(sinhVien);
        }

        // POST: SinhVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var result = await _studentService.DeleteStudentAsync(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Xóa sinh viên thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa sinh viên!";
            }

            return RedirectToAction(nameof(Index));
        }

        // API endpoint for getting student data (for AJAX calls)
        [HttpGet]
        public async Task<IActionResult> GetSinhVienJson(string id)
        {
            var sinhVien = await _studentService.GetStudentByIdAsync(id);

            if (sinhVien == null)
            {
                return NotFound();
            }

            return Json(new
            {
                maSV = sinhVien.MaSV,
                hoTen = sinhVien.HoTen,
                gioiTinh = sinhVien.GioiTinh,
                ngaySinh = sinhVien.NgaySinh?.ToString("yyyy-MM-dd"),
                hinh = sinhVien.Hinh,
                maNganh = sinhVien.MaNganh,
                tenNganh = sinhVien.NganhHoc?.TenNganh
            });
        }

        // Debug action to check image data
        [HttpGet]
        public async Task<IActionResult> DebugImages()
        {
            var students = await _context.SinhViens.ToListAsync();
            var debugInfo = students.Select(s => new {
                MaSV = s.MaSV,
                HoTen = s.HoTen,
                Hinh = s.Hinh,
                HasImage = !string.IsNullOrEmpty(s.Hinh),
                ImagePath = s.Hinh
            }).ToList();

            return Json(debugInfo);
        }

        // Test action to create sample student with image
        [HttpGet]
        public async Task<IActionResult> CreateTestStudent()
        {
            try
            {
                var testStudent = new SinhVien
                {
                    MaSV = "TEST001",
                    HoTen = "Nguyễn Văn Test",
                    GioiTinh = "Nam",
                    NgaySinh = DateTime.Now.AddYears(-20),
                    Hinh = "/images/students/44758217-84b9-4548-ade4-a5214c3694bb_Figure_1.png",
                    MaNganh = "CNTT"
                };

                // Check if student already exists
                var existingStudent = await _context.SinhViens.FindAsync("TEST001");
                if (existingStudent != null)
                {
                    _context.SinhViens.Remove(existingStudent);
                }

                _context.SinhViens.Add(testStudent);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Test student created successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}