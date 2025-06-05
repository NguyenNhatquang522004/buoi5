using buoi5.Models;

namespace buoi5.Repository
{
    public interface IStudentService
    {
        Task<IEnumerable<SinhVien>> GetAllStudentsAsync();
        Task<SinhVien?> GetStudentByIdAsync(string id);
        Task<bool> CreateStudentAsync(SinhVien student, IFormFile? imageFile);
        Task<bool> UpdateStudentAsync(SinhVien student, IFormFile? imageFile);
        Task<bool> DeleteStudentAsync(string id);
        Task<bool> StudentExistsAsync(string id);
    }
}
