using API.Models;
using Microsoft.Data.Sql;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace API.Data
{
    public class DBAppContext : DbContext
    {
        public DBAppContext(DbContextOptions options) : base(options)
        {
        }

        public DBAppContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-DOANBAO\SQLEXPRESS;Initial Catalog=Malis_app;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True");

            //string dbFilePath = "dbconfig.txt";
            //string Dbcheck = "check.txt";
            //string Db = "";
            //string check = ""; 


            //if (File.Exists(Dbcheck))
            //{
            //    check = File.ReadAllText(dbFilePath).Trim();
            //}


            //if (string.IsNullOrWhiteSpace(check))
            //{
            //    DataTable instances = SqlDataSourceEnumerator.Instance.GetDataSources();
            //    if (instances.Rows.Count > 0)
            //    {
            //        DataRow firstRow = instances.Rows[0];
            //        Db = firstRow["InstanceName"].ToString();
            //        File.WriteAllText(Dbcheck, Db);
            //        File.WriteAllText(Dbcheck, "True");
            //    }
            //}


            //string connectionString = $"Data Source=.\\{Db};Initial Catalog=Jolly;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True";

            //optionsBuilder.UseSqlServer($"Data Source=.\\{Db};Initial Catalog=Jolly;Integrated Security=True;TrustServerCertificate=True");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<NhanVien>()
                .HasOne(nv => nv.NguoiDung)
                .WithOne(nd => nd.NhanVien)
                .HasForeignKey<NhanVien>(nv => nv.NguoiDungId);
            modelBuilder.Entity<NguoiDung>()
                .HasOne(nd => nd.NhanVien)
                .WithOne(nv => nv.NguoiDung)
                .HasForeignKey<NhanVien>(nv => nv.NguoiDungId)
                .IsRequired();

            // Cấu hình mối quan hệ TaiKhoan - NguoiDung (1-1)
            modelBuilder.Entity<TaiKhoan>()
                .HasOne(tk => tk.NguoiDung)
                .WithOne()
                .HasForeignKey<TaiKhoan>(tk => tk.NguoiDungId)
                .IsRequired();

            // Cấu hình mối quan hệ NhanVien - ChucVu (nhiều-một)
            modelBuilder.Entity<NhanVien>()
                .HasOne(nv => nv.ChucVu)
                .WithMany(cv => cv.NhanVien)
                .HasForeignKey(nv => nv.ChucVuId);

            modelBuilder.Entity<Anh>()
                .HasOne(a => a.ChiTietMonAn)
                .WithMany(c => c.Anhs)
                .HasForeignKey(a => a.ChiTietMonAnId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ChiTietMonAn>()
                 .Property(c => c.Gia)
                 .HasPrecision(20, 2); 

            modelBuilder.Entity<Combo>()
                .Property(c => c.Gia)
                .HasPrecision(20, 2);

            modelBuilder.Entity<GiamGia>()
                .Property(g => g.GiaTriToiDa)
                .HasPrecision(20, 2);

            modelBuilder.Entity<GiamGia>()
                .Property(g => g.SoTienKhuyenMai)
                .HasPrecision(20, 2);

            modelBuilder.Entity<GiamGia>()
                .Property(g => g.SoTienToiThieu)
                .HasPrecision(20, 2);

            modelBuilder.Entity<GioHang>()
                .Property(g => g.TongGia)
                .HasPrecision(20, 2);

            modelBuilder.Entity<HoaDon>()
                .Property(h => h.TongTien)
                .HasPrecision(20, 2);

            modelBuilder.Entity<HoaDon>()
                .Property(h => h.TongTienSauKhiGiam)
                .HasPrecision(20, 2);

            modelBuilder.Entity<HoaDonChiTiet>()
                .Property(h => h.ThanhTien)
                .HasPrecision(20, 2);
        }
        public DbSet<Anh> anhs { get; set; }
        public DbSet<Combo> combos { get; set; }
        public DbSet<ChiTietCombo> chiTietCombos { get; set; }
        public DbSet<ChiTietGiamGia> chiTietGiamGias { get; set; }
        public DbSet<ChiTietMonAn> chiTietMonAns { get; set; }
        public DbSet<ChucVu> chucVus { get; set; }
        public DbSet<DiaChi> diaChis { get; set; }
        public DbSet<NguyenLieu> dongGois { get; set; }
        public DbSet<GiamGia> giamGias { get; set; }
        public DbSet<GioHang> gioHangs { get; set; }
        public DbSet<HinhThucThanhToan> hinhThucThanhToans { get; set; }
        public DbSet<HoaDon> hoaDons { get; set; }
        public DbSet<HoaDonChiTiet> hoaDonChiTiets { get; set; }
        public DbSet<KhachHang> khachHangs { get; set; }
        public DbSet<Loai> loais { get; set; }
        public DbSet<MonAn> monAns { get; set; }
        public DbSet<NguoiDung> nguoiDungs { get; set; }
        public DbSet<NhaCungCap> nhaCungCaps { get; set; }
        public DbSet<NhanVien> nhanViens { get; set; }
        public DbSet<TaiKhoan>taiKhoans { get; set; }
        public DbSet<TheLoai> theLoais { get; set; }
        public DbSet<ThuongHieu> thuongHieus { get; set; }
        public DbSet<LichSuTrangThai> trangThais { get; set; }
        public DbSet<LoaiVi> LoaiVis { get; set; }
        public DbSet<KichCo> KichCos { get; set; }
    }
}
