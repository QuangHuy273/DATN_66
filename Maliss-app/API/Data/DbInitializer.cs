using API.Models;

namespace API.Data
{
    public static class DbInitializer
    {
        public static void SeedData(DBAppContext context)
        {
            Console.WriteLine("Stating seeding data");
            // Kiểm tra nếu đã có dữ liệu thì không seed
            if (!context.chucVus.Any())
            {
                context.chucVus.AddRange(
                    new ChucVu
                    {
                        Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                        Ten = "Admin",
                        Mota = "Quản trị hệ thống",
                        TrangThai = true
                    },
                    new ChucVu
                    {
                        Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                        Ten = "NhanVien",
                        Mota = "Nhân viên bán hàng",
                        TrangThai = true
                    });
            }

            if (!context.taiKhoans.Any())
            {
                var adminTaiKhoanId = Guid.Parse("99999999-9999-9999-9999-999999999999");

                context.nguoiDungs.Add(new NguoiDung
                {
                    Id = adminTaiKhoanId,
                    Ho = "Nguyễn Văn",
                    Ten = "Quản Trị",
                    Sdt = "0987654321",
                    Gmail = "admin@shop.com",
                    GioiTinh = "Nam",
                    NgaySinh = new DateTime(1995, 1, 1),
                });

                context.taiKhoans.Add(new TaiKhoan
                {
                    Id = Guid.NewGuid(),
                    UserName = "admin",
                    Password = "admin123",
                    NguoiDungId = adminTaiKhoanId,
                    NgayTaoTk = DateTime.Now
                });
                context.SaveChanges();

                context.nhanViens.Add(new NhanVien
                {
                    Id = "NV001",
                    NgayVaoLam = DateTime.Now,
                    NguoiDungId = adminTaiKhoanId,
                    TrangThai = true,
                    ChucVuId = Guid.Parse("11111111-1111-1111-1111-111111111111")
                });
            }

            if (!context.hinhThucThanhToans.Any())
            {
                context.hinhThucThanhToans.AddRange(
                    new HinhThucThanhToan
                    {
                        Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                        Ten = "Tiền mặt",
                        Mota = "Thanh toán khi nhận hàng"
                    },
                    new HinhThucThanhToan
                    {
                        Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                        Ten = "Chuyển khoản",
                        Mota = "Thanh toán qua ngân hàng"
                    },
                    new HinhThucThanhToan
                    {
                        Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                        Ten = "VNPay",
                        Mota = "Thanh toán qua VNPay"
                    },
                    new HinhThucThanhToan
                    {
                        Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                        Ten = "Momo",
                        Mota = "Thanh toán qua ví Momo"
                    }
                );
            }
            context.SaveChanges();
        }
    }

}
