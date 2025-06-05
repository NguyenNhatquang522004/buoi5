using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace buoi5.Migrations
{
    /// <inheritdoc />
    public partial class AddSoLuongToHocPhan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SoLuongDaDangKy",
                table: "HocPhans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoLuongToiDa",
                table: "HocPhans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "HocPhans",
                keyColumn: "MaHP",
                keyValue: "CNTT01",
                columns: new[] { "SoLuongDaDangKy", "SoLuongToiDa" },
                values: new object[] { 0, 40 });

            migrationBuilder.UpdateData(
                table: "HocPhans",
                keyColumn: "MaHP",
                keyValue: "CNTT02",
                columns: new[] { "SoLuongDaDangKy", "SoLuongToiDa" },
                values: new object[] { 0, 35 });

            migrationBuilder.UpdateData(
                table: "HocPhans",
                keyColumn: "MaHP",
                keyValue: "QTDK02",
                columns: new[] { "SoLuongDaDangKy", "SoLuongToiDa" },
                values: new object[] { 0, 25 });

            migrationBuilder.UpdateData(
                table: "HocPhans",
                keyColumn: "MaHP",
                keyValue: "QTKD01",
                columns: new[] { "SoLuongDaDangKy", "SoLuongToiDa" },
                values: new object[] { 0, 30 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoLuongDaDangKy",
                table: "HocPhans");

            migrationBuilder.DropColumn(
                name: "SoLuongToiDa",
                table: "HocPhans");
        }
    }
}
