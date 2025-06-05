# 🎯 Hướng dẫn kiểm tra chức năng Đăng ký học phần

## ✅ Quy trình kiểm tra từng bước

### **Bước 1: Đăng nhập vào hệ thống**

1. **Truy cập trang đăng nhập:**
   ```
   URL: http://localhost:5107/Account/Login
   ```

2. **Sử dụng một trong các tài khoản test:**
   - **SV001** - Nguyễn Văn A (CNTT)
   - **SV002** - Trần Thị B (QTKD) 
   - **SV003** - Lê Văn C (CNTT)

3. **Thực hiện đăng nhập:**
   - Nhập mã sinh viên (VD: `SV001`)
   - Không cần nhập mật khẩu (bỏ trống)
   - Click "Đăng nhập"
   - Hệ thống sẽ redirect về trang chủ

### **Bước 2: Xem danh sách học phần**

1. **Truy cập trang học phần:**
   ```
   URL: http://localhost:5107/HocPhan
   hoặc click menu "Học phần"
   ```

2. **Kiểm tra hiển thị:**
   - Danh sách học phần được hiển thị dạng cards
   - Mỗi card hiển thị: Tên HP, Mã HP, Số tín chỉ, Số lượng (tối đa/đã đăng ký/còn lại)
   - Nút "Thêm vào giỏ đăng ký" cho các HP còn chỗ
   - Nút "Đã hết chỗ" (disabled) cho các HP hết chỗ

### **Bước 3: Thêm học phần vào giỏ**

1. **Chọn học phần muốn đăng ký:**
   - Click nút "Thêm vào giỏ đăng ký" ở học phần bất kỳ
   - Hệ thống hiển thị thông báo SweetAlert "Thành công!"
   - Nếu học phần đã có trong giỏ, hiển thị "Học phần đã có trong giỏ!"

2. **Thêm nhiều học phần:**
   - Thực hiện tương tự cho 2-3 học phần khác
   - Mỗi lần thêm thành công sẽ có thông báo xác nhận

### **Bước 4: Xem và quản lý giỏ đăng ký**

1. **Truy cập giỏ đăng ký:**
   ```
   URL: http://localhost:5107/DangKy/ViewCart
   hoặc click "Xem giỏ đăng ký"
   ```

2. **Kiểm tra chức năng:**
   - Xem danh sách học phần đã thêm
   - Tổng số tín chỉ được tính tự động
   - **Xóa từng học phần:** Click nút "Xóa" ở mỗi học phần
   - **Xóa tất cả:** Click "Xóa tất cả học phần"
   - Mỗi thao tác có thông báo xác nhận

### **Bước 5: Lưu đăng ký học phần**

1. **Thực hiện lưu đăng ký:**
   - Trong trang giỏ đăng ký, click "Lưu đăng ký"
   - Hệ thống thực hiện transaction:
     * Kiểm tra số lượng còn lại
     * Tạo bản ghi đăng ký mới
     * Cập nhật số lượng đã đăng ký của học phần
     * Xóa giỏ hàng sau khi lưu thành công

2. **Xử lý lỗi:**
   - Nếu học phần hết chỗ: "Học phần X đã hết chỗ!"
   - Nếu giỏ trống: "Giỏ đăng ký trống!"
   - Nếu có lỗi khác: Hiển thị thông báo lỗi cụ thể

### **Bước 6: Xem học phần đã đăng ký**

1. **Truy cập danh sách đăng ký:**
   ```
   URL: http://localhost:5107/DangKy/MyRegistrations
   hoặc click "Đã đăng ký"
   ```

2. **Kiểm tra hiển thị:**
   - Danh sách các lần đăng ký (theo ngày đăng ký)
   - Chi tiết học phần trong mỗi lần đăng ký
   - Tổng tín chỉ của mỗi lần đăng ký
   - Nút "Hủy đăng ký" cho mỗi lần đăng ký

3. **Thử hủy đăng ký:**
   - Click "Hủy đăng ký"
   - Xác nhận hủy
   - Số lượng học phần được cập nhật tự động

## 🧪 Các scenario test nâng cao

### **Test 1: Đăng ký học phần gần hết chỗ**
1. Đăng nhập với nhiều tài khoản khác nhau
2. Đăng ký cùng một học phần cho đến khi hết chỗ
3. Kiểm tra thông báo "hết chỗ" hiển thị đúng

### **Test 2: Đăng ký trùng lặp**
1. Thêm học phần A vào giỏ
2. Thử thêm học phần A lần nữa
3. Kiểm tra thông báo "đã có trong giỏ"

### **Test 3: Session management**
1. Thêm học phần vào giỏ
2. Đăng xuất và đăng nhập lại
3. Kiểm tra giỏ đã bị xóa (session reset)

### **Test 4: Đồng thời đăng ký**
1. Mở 2 tab browser
2. Đăng nhập 2 tài khoản khác nhau
3. Cùng đăng ký học phần có ít chỗ trống
4. Kiểm tra xử lý đồng thời

## ⚠️ Các lỗi thường gặp và cách khắc phục

### **Lỗi 1: Không thể thêm vào giỏ**
- **Nguyên nhân:** Chưa đăng nhập hoặc session hết hạn
- **Khắc phục:** Đăng nhập lại

### **Lỗi 2: Không hiển thị học phần**
- **Nguyên nhân:** Database chưa có dữ liệu
- **Khắc phục:** Chạy lại migration `dotnet ef database update`

### **Lỗi 3: Lỗi JavaScript**
- **Nguyên nhân:** Thiếu thư viện jQuery/Bootstrap
- **Khắc phục:** Kiểm tra Network tab trong F12

### **Lỗi 4: Thông báo không hiển thị**
- **Nguyên nhân:** SweetAlert2 không load
- **Khắc phục:** Kiểm tra CDN link trong _Layout.cshtml

## 📊 Kết quả mong đợi

Sau khi hoàn thành tất cả bước kiểm tra:

✅ **Đăng nhập thành công** với SV001/SV002/SV003  
✅ **Hiển thị danh sách học phần** với đầy đủ thông tin  
✅ **Thêm học phần vào giỏ** với thông báo xác nhận  
✅ **Quản lý giỏ hàng** (xem/xóa từng item/xóa tất cả)  
✅ **Lưu đăng ký thành công** với transaction hoàn chỉnh  
✅ **Xem danh sách đã đăng ký** với khả năng hủy  
✅ **Cập nhật số lượng tự động** khi đăng ký/hủy  
✅ **Xử lý lỗi đúng** cho các trường hợp edge case  

## 🎉 Tất cả chức năng đăng ký học phần đã hoạt động hoàn hảo!
