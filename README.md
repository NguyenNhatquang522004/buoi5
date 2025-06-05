# Website Đăng Ký Học Phần - Buoi 5

## Mô tả dự án
Đây là website đăng ký học phần được xây dựng bằng ASP.NET Core MVC, hoàn thành tất cả các yêu cầu trong đề bài Test 01.

## Các chức năng đã hoàn thành

### ✅ Câu 1 (6.0 điểm): Quản lý SINH VIÊN
- **a.** Trang danh sách sinh viên: `Index.cshtml` (1đ)
- **b.** Thêm sinh viên: `Create.cshtml` (1đ)  
- **c.** Sửa thông tin sinh viên: `Edit.cshtml` (1đ)
- **d.** Xóa sinh viên: `Delete.cshtml` (1đ)
- **e.** Thông tin chi tiết: `Detail.cshtml` (1đ)
- **f.** Giao diện đẹp, responsive (1 điểm)

### ✅ Câu 2 (0.5 điểm): Trang đăng nhập
- Đăng nhập bằng mã sinh viên
- Session management
- Redirect sau khi đăng nhập thành công

### ✅ Câu 3 (0.5 điểm): Trang HỌC PHẦN để sinh viên đăng ký
- Chỉ cho phép đăng ký khi đã đăng nhập
- Hiển thị danh sách học phần với đầy đủ thông tin
- Thêm vào giỏ đăng ký

### ✅ Câu 4 (1.0 điểm): Trang hiển thị thông tin học phần đã đăng ký
- Trang giỏ hàng sau khi chọn đăng ký học phần
- Xóa từng học phần đăng ký
- Xóa đăng ký (xóa hết các học phần)

### ✅ Câu 5 (1.0 điểm): Xử lý chức năng Lưu thông tin trên trang ĐĂNG KÝ HỌC PHẦN
- Chức năng Lưu đăng ký
- Thông báo đăng ký thành công
- Kết quả sau khi đăng ký học phần

### ✅ Câu 6 (1.0 điểm): Quản lý số lượng sinh viên cho mỗi học phần
- Thêm trường số lượng tối đa và số lượng đã đăng ký
- Mỗi lần sinh viên đăng ký, số lượng giảm theo đăng ký
- Số lượng đăng ký được cập nhật trong cơ sở dữ liệu
- Kiểm tra hết chỗ trước khi cho phép đăng ký

## Cách chạy dự án

### Yêu cầu hệ thống
- .NET 8.0 SDK
- MySQL Server (XAMPP hoặc standalone)
- Visual Studio 2022 hoặc VS Code

### Bước 1: Chuẩn bị database
1. Mở XAMPP và khởi động MySQL
2. Tạo database `testdb` trong phpMyAdmin

### Bước 2: Cập nhật connection string
Kiểm tra file `appsettings.json` và đảm bảo connection string đúng:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=testdb;User=root;Password=;"
  }
}
```

### Bước 3: Chạy migration
```bash
cd buoi5
dotnet ef database update
```

### Bước 4: Chạy ứng dụng
```bash
dotnet run --launch-profile https
```

Truy cập: `https://localhost:7055`

## Hướng dẫn sử dụng

### 1. Đăng nhập
- Sử dụng một trong các mã sinh viên mẫu:
  - `0123456789` (Nguyễn Văn A - CNTT)
  - `9876543210` (Nguyễn Thị B - QTKD)
- Mật khẩu: nhập bất kỳ (hệ thống chỉ kiểm tra mã sinh viên)

### 2. Quản lý sinh viên
- Truy cập menu "Sinh viên" để xem danh sách
- Có thể thêm, sửa, xóa, xem chi tiết sinh viên

### 3. Đăng ký học phần
- Sau khi đăng nhập, click "Học phần" để xem danh sách
- Click "Thêm vào giỏ đăng ký" cho các học phần muốn đăng ký
- Vào "Giỏ đăng ký" để kiểm tra và lưu đăng ký

### 4. Xem học phần đã đăng ký
- Click "Đã đăng ký" để xem các học phần đã đăng ký
- Có thể xóa từng đăng ký nếu cần

## Cấu trúc dự án

```
buoi5/
├── Controllers/           # Các controller xử lý logic
│   ├── AccountController.cs    # Đăng nhập/đăng xuất
│   ├── DangKyController.cs     # Đăng ký học phần
│   ├── HocPhanController.cs    # Quản lý học phần
│   └── SinhVienController.cs   # Quản lý sinh viên
├── Models/               # Các model và DbContext
│   ├── ApplicationDbContext.cs
│   ├── SinhVien.cs
│   ├── HocPhan.cs
│   ├── DangKy.cs
│   ├── ChiTietDangKy.cs
│   └── NganhHoc.cs
├── Views/                # Các view Razor
│   ├── Account/         # Views đăng nhập
│   ├── DangKy/          # Views đăng ký học phần
│   ├── HocPhan/         # Views học phần
│   └── SinhVien/        # Views sinh viên
└── wwwroot/             # Static files
    └── Content/images/  # Hình ảnh sinh viên
```

## Tính năng nổi bật

### 🎨 Giao diện hiện đại
- Sử dụng Bootstrap 5
- Responsive design
- Icons FontAwesome
- SweetAlert2 cho thông báo đẹp

### 🔐 Bảo mật
- Session-based authentication
- Kiểm tra quyền truy cập mọi action
- Validate dữ liệu đầu vào

### 📊 Quản lý số lượng thông minh
- Theo dõi số lượng sinh viên đăng ký
- Tự động cập nhật khi có đăng ký/hủy
- Hiển thị trạng thái "hết chỗ"

### 🛒 Giỏ đăng ký linh hoạt
- Thêm/xóa học phần trong session
- Kiểm tra trùng lặp
- Tính toán tổng tín chỉ

## Liên hệ
Nếu có vấn đề gì trong quá trình chạy dự án, vui lòng liên hệ để được hỗ trợ.

---
**Developed with ❤️ using ASP.NET Core MVC**
