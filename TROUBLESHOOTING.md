# 🔧 Khắc phục vấn đề đăng ký học phần

## 🚨 Các vấn đề có thể gặp phải và cách khắc phục

### 1. **Vấn đề không thể thêm học phần vào giỏ**

#### Nguyên nhân có thể:
- Chưa đăng nhập
- Lỗi AJAX/JavaScript 
- Vấn đề session
- Dữ liệu học phần không có sẵn

#### Cách kiểm tra:
1. **Đăng nhập trước tiên:**
   - Truy cập: http://localhost:5107/Account/Login
   - Sử dụng mã sinh viên: `SV001`, `SV002`, hoặc `SV003`
   - Không cần mật khẩu (hệ thống chỉ kiểm tra mã SV)

2. **Kiểm tra dữ liệu học phần:**
   - Truy cập: http://localhost:5107/HocPhan
   - Xem có học phần nào hiển thị không
   - Kiểm tra số lượng còn lại > 0

### 2. **Vấn đề hiển thị học phần**

#### Nếu không thấy học phần nào:
1. Kiểm tra database có dữ liệu mẫu
2. Kiểm tra connection string trong `appsettings.json`
3. Chạy lại migration nếu cần

### 3. **Vấn đề session/đăng nhập**

#### Nếu bị redirect về trang login liên tục:
1. Kiểm tra session middleware trong Program.cs
2. Đảm bảo đã thêm `app.UseSession()`
3. Kiểm tra cookies trong browser

### 4. **Lỗi JavaScript/AJAX**

#### Nếu nút "Thêm vào giỏ" không hoạt động:
1. **Mở Developer Tools (F12)**
2. **Kiểm tra Console tab có lỗi JavaScript:**
   - Tìm lỗi "$ is not defined" → Thiếu jQuery
   - Tìm lỗi "404 Not Found" → Sai đường dẫn action
   - Tìm lỗi "500 Internal Server Error" → Lỗi server
3. **Kiểm tra Network tab để xem AJAX request:**
   - Click nút "Thêm vào giỏ"
   - Xem có request POST không
   - Kiểm tra response status và nội dung

#### Các vấn đề thường gặp:
- **jQuery chưa được load**: Đã sửa bằng cách thêm jQuery vào layout
- **Controller/Action sai**: Đã sửa thành `@Url.Action("AddToCart", "HocPhan")`
- **Session hết hạn**: Cần đăng nhập lại

## ✅ Hướng dẫn kiểm tra từng bước

### Bước 1: Kiểm tra trang chủ
1. Mở: http://localhost:5107
2. Kiểm tra navigation menu hiển thị đúng
3. Click "Đăng nhập" nếu chưa đăng nhập

### Bước 2: Đăng nhập
1. Truy cập: http://localhost:5107/Account/Login
2. Nhập mã sinh viên: `SV001` (hoặc `SV002`, `SV003`)
3. Click "Đăng nhập"
4. Xem có redirect về trang chủ không

### Bước 3: Kiểm tra học phần
1. Click menu "Học phần" hoặc truy cập: http://localhost:5107/HocPhan
2. Kiểm tra danh sách học phần hiển thị
3. Xem số lượng còn lại của từng học phần

### Bước 4: Thử thêm vào giỏ
1. Click nút "Thêm vào giỏ đăng ký" ở một học phần
2. Chờ thông báo success/error
3. Nếu thành công, thử click "Xem giỏ đăng ký"

### Bước 5: Kiểm tra giỏ đăng ký
1. Truy cập: http://localhost:5107/DangKy/ViewCart
2. Xem học phần đã thêm có hiển thị không
3. Thử chức năng "Lưu đăng ký"

## 🛠️ Debug chi tiết

### Kiểm tra logs trong terminal:
```
Mở terminal đang chạy ứng dụng và xem có lỗi nào không
```

### Kiểm tra database:
```sql
-- Truy cập phpMyAdmin và chạy:
SELECT * FROM HocPhans;
SELECT * FROM SinhViens;
SELECT * FROM DangKys;
SELECT * FROM ChiTietDangKys;
```

### Kiểm tra Developer Tools:
1. Mở F12 trong browser
2. Tab Console: Xem lỗi JavaScript
3. Tab Network: Xem AJAX requests
4. Tab Application: Xem session data

## 🔍 Các URL để test:

1. **Trang chủ**: http://localhost:5107
2. **Đăng nhập**: http://localhost:5107/Account/Login  
3. **Học phần**: http://localhost:5107/HocPhan
4. **Giỏ đăng ký**: http://localhost:5107/DangKy/ViewCart
5. **Đăng ký của tôi**: http://localhost:5107/DangKy/MyRegistrations
6. **Quản lý sinh viên**: http://localhost:5107/SinhVien

## ⚠️ Lưu ý quan trọng:

1. **Phải đăng nhập trước** khi thực hiện bất kỳ thao tác nào
2. **Sử dụng mã sinh viên đúng**: SV001, SV002, hoặc SV003
3. **Kiểm tra số lượng còn lại** của học phần trước khi đăng ký
4. **Session sẽ mất** khi restart ứng dụng

## 🆘 Nếu vẫn gặp vấn đề:

1. **Restart ứng dụng**: Ctrl+C và chạy lại `dotnet run`
2. **Clear browser cache**: Xóa cookies và cache
3. **Kiểm tra firewall**: Đảm bảo port 5107 không bị block
4. **Kiểm tra MySQL**: Đảm bảo XAMPP MySQL đang chạy

## 📞 Liên hệ để được hỗ trợ chi tiết hơn nếu cần thiết!
