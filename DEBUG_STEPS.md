# 🔍 Hướng dẫn Debug chức năng "Thêm vào giỏ đăng ký"

## ✅ **Các sửa đổi đã thực hiện:**

1. **Sửa JavaScript URL**: Đã thêm controller name vào `@Url.Action("AddToCart", "HocPhan")`
2. **Thêm jQuery**: Đã thêm jQuery CDN vào layout
3. **Thêm error handling**: JavaScript có xử lý lỗi AJAX chi tiết
4. **Thêm console logging**: Để debug dễ dàng hơn

## 🧪 **Các bước test chi tiết:**

### **Bước 1: Mở Developer Tools**
1. Mở trình duyệt tại: `http://localhost:5107`
2. Nhấn **F12** để mở Developer Tools
3. Chọn tab **Console**
4. Đảm bảo không có lỗi JavaScript nào

### **Bước 2: Đăng nhập**
1. Click "Đăng nhập" 
2. Nhập: `SV001` (hoặc `SV002`, `SV003`)
3. Click "Đăng nhập"
4. Kiểm tra redirect về trang chủ thành công

### **Bước 3: Truy cập trang học phần**
1. Click menu "Học phần" hoặc truy cập: `http://localhost:5107/HocPhan`
2. Kiểm tra danh sách học phần hiển thị
3. Tìm học phần có "Còn lại: > 0"

### **Bước 4: Test thêm vào giỏ**
1. Click nút **"Thêm vào giỏ đăng ký"** ở một học phần
2. **Quan sát Console tab** phải thấy:
   ```
   Đang thêm học phần: [MaHP]
   Kết quả: {success: true, message: "Đã thêm vào giỏ đăng ký!"}
   ```
3. **Phải thấy popup SweetAlert** với thông báo "Thành công!"

## 🚨 **Nếu vẫn không hoạt động:**

### **Kiểm tra Console Errors:**
1. **"$ is not defined"** → jQuery chưa load (đã sửa)
2. **"404 Not Found"** → URL sai (đã sửa)
3. **"500 Internal Server Error"** → Lỗi server
4. **"Uncaught TypeError"** → Lỗi JavaScript

### **Kiểm tra Network Tab:**
1. Chọn tab **Network** trong Developer Tools
2. Click nút "Thêm vào giỏ"
3. Xem có request POST đến `/HocPhan/AddToCart` không
4. Kiểm tra response status và content

### **Kiểm tra Application Tab:**
1. Chọn tab **Application** > **Session Storage**
2. Kiểm tra có key "Cart" không sau khi thêm thành công

## 🔧 **Troubleshooting thêm:**

### **Nếu jQuery không load:**
```html
<!-- Kiểm tra trong _Layout.cshtml có dòng này không: -->
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
```

### **Nếu action không tìm thấy:**
```csharp
// Kiểm tra trong HocPhanController.cs có method này không:
[HttpPost]
public async Task<IActionResult> AddToCart(string maHP)
```

### **Nếu session lỗi:**
1. Restart ứng dụng: `Ctrl+C` rồi `dotnet run`
2. Clear browser cache và cookies
3. Đăng nhập lại

## 📋 **Checklist cuối:**

- [ ] Ứng dụng đang chạy trên port 5107
- [ ] Đã đăng nhập thành công
- [ ] Console không có lỗi JavaScript
- [ ] jQuery đã được load
- [ ] SweetAlert đã được load
- [ ] Database có dữ liệu học phần

## 🆘 **Nếu tất cả đều đúng mà vẫn không hoạt động:**

1. **Restart toàn bộ:**
   ```
   Ctrl+C (dừng app)
   dotnet clean
   dotnet build
   dotnet run
   ```

2. **Kiểm tra session middleware** trong Program.cs

3. **Kiểm tra database connection** trong appsettings.json

4. **Test trực tiếp URL:**
   ```
   POST: http://localhost:5107/HocPhan/AddToCart
   Body: maHP=HP001
   ```

Hãy thực hiện các bước trên và báo lại kết quả!
