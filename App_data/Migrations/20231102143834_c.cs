using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_data.Migrations
{
    public partial class c : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatLieu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatLieu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KhuyenMai",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    GiaTri = table.Column<int>(type: "int", nullable: false),
                    NgayApDung = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhuyenMai", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NhaCungCap",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaCungCap", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhuongThucThanhToan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhuongThucThanhToan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrangThaiHoaDon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrangThaiHoaDon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VaiTro",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaiTro", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoaiSP",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    IdLoaiSPCha = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdChatLieu = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiSP", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoaiSP_ChatLieu_IdChatLieu",
                        column: x => x.IdChatLieu,
                        principalTable: "ChatLieu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoaiSP_LoaiSP_IdLoaiSPCha",
                        column: x => x.IdLoaiSPCha,
                        principalTable: "LoaiSP",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoan",
                columns: table => new
                {
                    IdNguoiDung = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TenDem = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Ho = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    GioiTinh = table.Column<int>(type: "int", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SDT = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    IdVaiTro = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoan", x => x.IdNguoiDung);
                    table.ForeignKey(
                        name: "FK_TaiKhoan_VaiTro_IdVaiTro",
                        column: x => x.IdVaiTro,
                        principalTable: "VaiTro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GioHang",
                columns: table => new
                {
                    IdNguoiDung = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHang", x => x.IdNguoiDung);
                    table.ForeignKey(
                        name: "FK_GioHang_TaiKhoan_IdNguoiDung",
                        column: x => x.IdNguoiDung,
                        principalTable: "TaiKhoan",
                        principalColumn: "IdNguoiDung",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime", nullable: true),
                    TenNguoiNhan = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    SDT = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TienKhachDua = table.Column<int>(type: "int", nullable: false),
                    TongTien = table.Column<long>(type: "bigint", nullable: false),
                    TienShip = table.Column<int>(type: "int", nullable: false),
                    TienThua = table.Column<long>(type: "bigint", nullable: false),
                    PhuongThucThanhToan = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    IdTrangThaiGiaoHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdNguoiDung = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoaDon_TaiKhoan_IdNguoiDung",
                        column: x => x.IdNguoiDung,
                        principalTable: "TaiKhoan",
                        principalColumn: "IdNguoiDung");
                    table.ForeignKey(
                        name: "FK_HoaDon_TrangThaiHoaDon_IdTrangThaiGiaoHang",
                        column: x => x.IdTrangThaiGiaoHang,
                        principalTable: "TrangThaiHoaDon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietPTTT",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdPTTT = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdHoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoTien = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietPTTT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietPTTT_HoaDon_IdHoaDon",
                        column: x => x.IdHoaDon,
                        principalTable: "HoaDon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietPTTT_PhuongThucThanhToan_IdPTTT",
                        column: x => x.IdPTTT,
                        principalTable: "PhuongThucThanhToan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Anh",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    ChiTietSanPhamId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietGioHang",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<int>(type: "int", nullable: false),
                    IdChiTietSP = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdNguoiDung = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietGioHang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietGioHang_GioHang_IdNguoiDung",
                        column: x => x.IdNguoiDung,
                        principalTable: "GioHang",
                        principalColumn: "IdNguoiDung",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietHoaDon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DonGia = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    IdChiTiepSP = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdHoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietHoaDon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDon_HoaDon_IdHoaDon",
                        column: x => x.IdHoaDon,
                        principalTable: "HoaDon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietSanPham",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    IdAnh = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdMauSac = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSanPham = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdLoaiSP = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdKhuyenMai = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdNhaCungCap = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSize = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DonGia = table.Column<int>(type: "int", nullable: false),
                    soluong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietSanPham", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietSanPham_Anh_IdAnh",
                        column: x => x.IdAnh,
                        principalTable: "Anh",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietSanPham_KhuyenMai_IdKhuyenMai",
                        column: x => x.IdKhuyenMai,
                        principalTable: "KhuyenMai",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChiTietSanPham_LoaiSP_IdLoaiSP",
                        column: x => x.IdLoaiSP,
                        principalTable: "LoaiSP",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietSanPham_NhaCungCap_IdNhaCungCap",
                        column: x => x.IdNhaCungCap,
                        principalTable: "NhaCungCap",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietSanPham_SanPham_IdSanPham",
                        column: x => x.IdSanPham,
                        principalTable: "SanPham",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DanhGia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BinhLuan = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IdNguoiDung = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdChiTietSP = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sao = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhGia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DanhGia_ChiTietSanPham_IdChiTietSP",
                        column: x => x.IdChiTietSP,
                        principalTable: "ChiTietSanPham",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DanhGia_TaiKhoan_IdNguoiDung",
                        column: x => x.IdNguoiDung,
                        principalTable: "TaiKhoan",
                        principalColumn: "IdNguoiDung",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MauSac",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnhMauSac = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    ChiTietSanPhamId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MauSac", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MauSac_ChiTietSanPham_ChiTietSanPhamId",
                        column: x => x.ChiTietSanPhamId,
                        principalTable: "ChiTietSanPham",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    ChiTietSanPhamId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Size", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Size_ChiTietSanPham_ChiTietSanPhamId",
                        column: x => x.ChiTietSanPhamId,
                        principalTable: "ChiTietSanPham",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Anh_ChiTietSanPhamId",
                table: "Anh",
                column: "ChiTietSanPhamId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGioHang_IdChiTietSP",
                table: "ChiTietGioHang",
                column: "IdChiTietSP");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGioHang_IdNguoiDung",
                table: "ChiTietGioHang",
                column: "IdNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDon_IdChiTiepSP",
                table: "ChiTietHoaDon",
                column: "IdChiTiepSP");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDon_IdHoaDon",
                table: "ChiTietHoaDon",
                column: "IdHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPTTT_IdHoaDon",
                table: "ChiTietPTTT",
                column: "IdHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPTTT_IdPTTT",
                table: "ChiTietPTTT",
                column: "IdPTTT");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPham_IdAnh",
                table: "ChiTietSanPham",
                column: "IdAnh");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPham_IdKhuyenMai",
                table: "ChiTietSanPham",
                column: "IdKhuyenMai");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPham_IdLoaiSP",
                table: "ChiTietSanPham",
                column: "IdLoaiSP");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPham_IdMauSac",
                table: "ChiTietSanPham",
                column: "IdMauSac");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPham_IdNhaCungCap",
                table: "ChiTietSanPham",
                column: "IdNhaCungCap");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPham_IdSanPham",
                table: "ChiTietSanPham",
                column: "IdSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSanPham_IdSize",
                table: "ChiTietSanPham",
                column: "IdSize");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGia_IdChiTietSP",
                table: "DanhGia",
                column: "IdChiTietSP");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGia_IdNguoiDung",
                table: "DanhGia",
                column: "IdNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_IdNguoiDung",
                table: "HoaDon",
                column: "IdNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_IdTrangThaiGiaoHang",
                table: "HoaDon",
                column: "IdTrangThaiGiaoHang");

            migrationBuilder.CreateIndex(
                name: "IX_LoaiSP_IdChatLieu",
                table: "LoaiSP",
                column: "IdChatLieu");

            migrationBuilder.CreateIndex(
                name: "IX_LoaiSP_IdLoaiSPCha",
                table: "LoaiSP",
                column: "IdLoaiSPCha");

            migrationBuilder.CreateIndex(
                name: "IX_MauSac_ChiTietSanPhamId",
                table: "MauSac",
                column: "ChiTietSanPhamId");

            migrationBuilder.CreateIndex(
                name: "IX_Size_ChiTietSanPhamId",
                table: "Size",
                column: "ChiTietSanPhamId");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoan_IdVaiTro",
                table: "TaiKhoan",
                column: "IdVaiTro");

            migrationBuilder.AddForeignKey(
                name: "FK_Anh_ChiTietSanPham_ChiTietSanPhamId",
                table: "Anh",
                column: "ChiTietSanPhamId",
                principalTable: "ChiTietSanPham",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietGioHang_ChiTietSanPham_IdChiTietSP",
                table: "ChiTietGioHang",
                column: "IdChiTietSP",
                principalTable: "ChiTietSanPham",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoaDon_ChiTietSanPham_IdChiTiepSP",
                table: "ChiTietHoaDon",
                column: "IdChiTiepSP",
                principalTable: "ChiTietSanPham",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietSanPham_MauSac_IdMauSac",
                table: "ChiTietSanPham",
                column: "IdMauSac",
                principalTable: "MauSac",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietSanPham_Size_IdSize",
                table: "ChiTietSanPham",
                column: "IdSize",
                principalTable: "Size",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anh_ChiTietSanPham_ChiTietSanPhamId",
                table: "Anh");

            migrationBuilder.DropForeignKey(
                name: "FK_MauSac_ChiTietSanPham_ChiTietSanPhamId",
                table: "MauSac");

            migrationBuilder.DropForeignKey(
                name: "FK_Size_ChiTietSanPham_ChiTietSanPhamId",
                table: "Size");

            migrationBuilder.DropTable(
                name: "ChiTietGioHang");

            migrationBuilder.DropTable(
                name: "ChiTietHoaDon");

            migrationBuilder.DropTable(
                name: "ChiTietPTTT");

            migrationBuilder.DropTable(
                name: "DanhGia");

            migrationBuilder.DropTable(
                name: "GioHang");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "PhuongThucThanhToan");

            migrationBuilder.DropTable(
                name: "TaiKhoan");

            migrationBuilder.DropTable(
                name: "TrangThaiHoaDon");

            migrationBuilder.DropTable(
                name: "VaiTro");

            migrationBuilder.DropTable(
                name: "ChiTietSanPham");

            migrationBuilder.DropTable(
                name: "Anh");

            migrationBuilder.DropTable(
                name: "KhuyenMai");

            migrationBuilder.DropTable(
                name: "LoaiSP");

            migrationBuilder.DropTable(
                name: "MauSac");

            migrationBuilder.DropTable(
                name: "NhaCungCap");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "Size");

            migrationBuilder.DropTable(
                name: "ChatLieu");
        }
    }
}
