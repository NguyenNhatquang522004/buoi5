using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace buoi5.Migrations
{
    /// <inheritdoc />
    public partial class AddNewStudentData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HocPhans",
                keyColumn: "MaHP",
                keyValue: "QTDK02");

            migrationBuilder.InsertData(
                table: "HocPhans",
                columns: new[] { "MaHP", "SoLuongDaDangKy", "SoLuongToiDa", "SoTinChi", "TenHP" },
                values: new object[] { "QTKD02", 0, 25, 3, "Xác suất thống kê 1" });

            migrationBuilder.UpdateData(
                table: "SinhViens",
                keyColumn: "MaSV",
                keyValue: "0123456789",
                columns: new[] { "Hinh", "HoTen" },
                values: new object[] { "/Content/images/sv1.svg", "Nguyễn Văn A (Cũ)" });

            migrationBuilder.UpdateData(
                table: "SinhViens",
                keyColumn: "MaSV",
                keyValue: "9876543210",
                columns: new[] { "Hinh", "HoTen" },
                values: new object[] { "/Content/images/sv2.svg", "Nguyễn Thị B (Cũ)" });

            migrationBuilder.InsertData(
                table: "SinhViens",
                columns: new[] { "MaSV", "GioiTinh", "Hinh", "HoTen", "MaNganh", "NgaySinh" },
                values: new object[,]
                {
                    { "SV001", "Nam", "/Content/images/sv1.svg", "Nguyễn Văn A", "CNTT", new DateTime(2000, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "SV002", "Nữ", "/Content/images/sv2.svg", "Trần Thị B", "QTKD", new DateTime(2000, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "SV003", "Nam", "/Content/images/sv1.svg", "Lê Văn C", "CNTT", new DateTime(1999, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HocPhans",
                keyColumn: "MaHP",
                keyValue: "QTKD02");

            migrationBuilder.DeleteData(
                table: "SinhViens",
                keyColumn: "MaSV",
                keyValue: "SV001");

            migrationBuilder.DeleteData(
                table: "SinhViens",
                keyColumn: "MaSV",
                keyValue: "SV002");

            migrationBuilder.DeleteData(
                table: "SinhViens",
                keyColumn: "MaSV",
                keyValue: "SV003");

            migrationBuilder.InsertData(
                table: "HocPhans",
                columns: new[] { "MaHP", "SoLuongDaDangKy", "SoLuongToiDa", "SoTinChi", "TenHP" },
                values: new object[] { "QTDK02", 0, 25, 3, "Xác suất thống kê 1" });

            migrationBuilder.UpdateData(
                table: "SinhViens",
                keyColumn: "MaSV",
                keyValue: "0123456789",
                columns: new[] { "Hinh", "HoTen" },
                values: new object[] { "/Content/images/sv1.jpg", "Nguyễn Văn A" });

            migrationBuilder.UpdateData(
                table: "SinhViens",
                keyColumn: "MaSV",
                keyValue: "9876543210",
                columns: new[] { "Hinh", "HoTen" },
                values: new object[] { "/Content/images/sv2.jpg", "Nguyễn Thị B" });
        }
    }
}
