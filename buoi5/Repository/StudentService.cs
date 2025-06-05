using buoi5.Models;
using Microsoft.EntityFrameworkCore;  // THIS WAS MISSING - Important for Include(), AnyAsync()

namespace buoi5.Repository
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public StudentService(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IEnumerable<SinhVien>> GetAllStudentsAsync()
        {
            return await _context.SinhViens
                .Include(s => s.NganhHoc)  // Now this will work
                .OrderBy(s => s.HoTen)
                .ToListAsync();
        }

        public async Task<SinhVien?> GetStudentByIdAsync(string id)
        {
            return await _context.SinhViens
                .Include(s => s.NganhHoc)  // Now this will work
                .Include(s => s.DangKys)
                    .ThenInclude(d => d.ChiTietDangKys)
                        .ThenInclude(c => c.HocPhan)
                .FirstOrDefaultAsync(s => s.MaSV == id);
        }

        public async Task<bool> CreateStudentAsync(SinhVien student, IFormFile? imageFile)
        {
            try
            {
                // Check if student already exists
                if (await StudentExistsAsync(student.MaSV))
                {
                    return false;
                }

                // Handle image upload
                if (imageFile != null && imageFile.Length > 0)
                {
                    student.Hinh = await SaveImageAsync(imageFile);
                }

                _context.SinhViens.Add(student);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateStudentAsync(SinhVien student, IFormFile? imageFile)
        {
            try
            {
                var existingStudent = await _context.SinhViens.FindAsync(student.MaSV);
                if (existingStudent == null) return false;

                // Handle image upload
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Delete old image
                    if (!string.IsNullOrEmpty(existingStudent.Hinh))
                    {
                        DeleteImage(existingStudent.Hinh);
                    }
                    student.Hinh = await SaveImageAsync(imageFile);
                }
                else
                {
                    student.Hinh = existingStudent.Hinh; // Keep existing image
                }

                _context.Entry(existingStudent).CurrentValues.SetValues(student);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteStudentAsync(string id)
        {
            try
            {
                var student = await _context.SinhViens.FindAsync(id);
                if (student == null) return false;

                // Delete image file
                if (!string.IsNullOrEmpty(student.Hinh))
                {
                    DeleteImage(student.Hinh);
                }

                _context.SinhViens.Remove(student);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> StudentExistsAsync(string id)
        {
            return await _context.SinhViens.AnyAsync(e => e.MaSV == id);  // Now this will work
        }

        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "students");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return "/images/students/" + uniqueFileName;
        }

        private void DeleteImage(string imagePath)
        {
            try
            {
                var fullPath = Path.Combine(_environment.WebRootPath, imagePath.TrimStart('/'));
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
            catch
            {
                // Log error if needed, but don't throw
            }
        }
    }
}