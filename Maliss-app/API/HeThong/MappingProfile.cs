using AutoMapper;
using API.Models;
using API.Models.DTO;

namespace API.HeThong
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Anh, AnhDTO>().ReverseMap();
            CreateMap<AnhDTO, Anh>().ReverseMap();
            CreateMap<Combo, ComboDTO>().ReverseMap();
            CreateMap<ChiTietCombo, ChiTietComboDTO>().ReverseMap();
            CreateMap<ChiTietGiamGia, ChiTietGiamGiaDTO>().ReverseMap();
            CreateMap<ChiTietMonAn, ChiTietMonAnDTO>()
                .ForMember(t => t.Ten, opt => opt.MapFrom(src =>
                    src.MonAn != null && !string.IsNullOrWhiteSpace(src.MonAn.Ten)
                    ? src.MonAn.Ten : "")) 
                .ForMember(dg => dg.NguyenLieu, opt => opt.MapFrom(src =>
                    src.NguyenLieu != null && !string.IsNullOrWhiteSpace(src.NguyenLieu.Ten)
                    ? src.NguyenLieu.Ten : ""))
                .ForMember(ncc => ncc.NhaCungCap, opt => opt.MapFrom(src =>
                    src.NhaCungCap != null && !string.IsNullOrWhiteSpace(src.NhaCungCap.Ten)
                    ? src.NhaCungCap.Ten : ""))
                .ForMember(anh => anh.DanhSachAnh,  opt =>  opt.MapFrom(src => 
                    src.Anhs !=  null  ? src.Anhs : new List<Anh>()))
                .ForMember(tl => tl.LoaiVi, opt => opt.MapFrom(src =>
                    src.LoaiVi != null && !string.IsNullOrWhiteSpace(src.LoaiVi.Ten)
                    ? src.LoaiVi.Ten : ""))
                .ForMember(tl => tl.KichCo, opt => opt.MapFrom(src =>
                    src.KichCo != null && !string.IsNullOrWhiteSpace(src.KichCo.Ten)
                    ? src.KichCo.Ten : ""))
                .ForMember(dg => dg.Gia, opt => opt.MapFrom(src => src.Gia))
                .ReverseMap();

            CreateMap<ChiTietMonAnDTO, ChiTietMonAn>()
                .ForMember(dest => dest.MonAn, opt => opt.Ignore())
                .ForMember(dest => dest.NhaCungCap, opt => opt.Ignore())
                .ForMember(dest => dest.NguyenLieu, opt => opt.Ignore())
                .ForMember(dest => dest.LoaiVi, opt => opt.Ignore())
                .ForMember(dest => dest.KichCo, opt => opt.Ignore());

            CreateMap<ChucVu, ChucVuDTO>().ReverseMap();
            CreateMap<DiaChi, DiaChiDTO>().ReverseMap();
            CreateMap<NguyenLieu, DongGoiDTO>().ReverseMap();
            CreateMap<GiamGia, Voucher>().ReverseMap();
            CreateMap<GioHang, GioHangDTO>().ReverseMap();
            CreateMap<HinhThucThanhToan, HinhThucThanhToanDTO>().ReverseMap();
            CreateMap<HoaDonChiTiet, HoaDonChiTietDTO>().ReverseMap();
            CreateMap<HoaDon, HoaDonDTO>().ReverseMap();
            CreateMap<KhachHang, KhachHangDTO>()
            .ForMember(dest => dest.DiaChis, opt => opt.MapFrom(src =>
            src.NguoiDung != null ? src.NguoiDung.DiaChis : new List<DiaChi>()))
            .ForMember(dest => dest.Ho, opt => opt.MapFrom(src =>
                src.NguoiDung != null && !string.IsNullOrWhiteSpace(src.NguoiDung.Ho)
                ? src.NguoiDung.Ho : ""))
            .ForMember(dest => dest.Ten, opt => opt.MapFrom(src =>
                src.NguoiDung != null && !string.IsNullOrWhiteSpace(src.NguoiDung.Ten)
                ? src.NguoiDung.Ten : ""))
            .ForMember(dest => dest.Sdt, opt => opt.MapFrom(src =>
                src.NguoiDung != null && !string.IsNullOrWhiteSpace(src.NguoiDung.Sdt)
                ? src.NguoiDung.Sdt : ""))
            .ForMember(dest => dest.Gmail, opt => opt.MapFrom(src =>
                src.NguoiDung != null && !string.IsNullOrWhiteSpace(src.NguoiDung.Gmail)
                ? src.NguoiDung.Gmail : ""))
            .ForMember(dest => dest.NgaySinh, opt => opt.MapFrom(src =>
                src.NguoiDung != null ? src.NguoiDung.NgaySinh : DateTime.Now))
            .ReverseMap();

            CreateMap<KhachHangDTO, KhachHang>()
                .ForMember(dest => dest.NguoiDung, opt => opt.Ignore());


            CreateMap<Loai, LoaiDTO>().ReverseMap();
            CreateMap<MonAn, MonAnDTO>()
                .ForMember(t => t.ThuongHieu, opt => opt.MapFrom(src =>
                    src.ThuongHieu != null && !string.IsNullOrWhiteSpace(src.ThuongHieu.Ten)
                    ? src.ThuongHieu.Ten : ""))
                .ForMember(dg => dg.TheLoai, opt => opt.MapFrom(src =>
                    src.TheLoai != null && !string.IsNullOrWhiteSpace(src.TheLoai.Ten)
                    ? src.TheLoai.Ten : ""))
                .ForMember(a => a.AnhDaTai, opt => opt.MapFrom(src =>
                    src.ChiTietMonAns != null 
                    ? src.ChiTietMonAns
                        .Where(ct => ct.Anhs != null && ct.TrangThai == true && ct.Anhs.Any() &&
                                        !string.IsNullOrWhiteSpace(ct.Anhs.First().DuongDan))
                        .Select(ct => ct.Anhs.First().DuongDan)
                        .FirstOrDefault() ?? ""
                    : ""))
                .ForMember(dg => dg.Gia, opt => opt.MapFrom(src =>
                    src.ChiTietMonAns != null
                        ? src.ChiTietMonAns
                            .Where(ct => ct.TrangThai == true && ct.Gia > 0)
                            .Select(ct => ct.Gia)
                            .FirstOrDefault()
                        : 0))
                .ReverseMap();
            CreateMap<MonAnDTO, MonAn>()
                .ForMember(dest => dest.ThuongHieu, opt => opt.Ignore())
                .ForMember(dest => dest.TheLoai, opt => opt.Ignore());
            CreateMap<NguoiDung, NguoiDungDTO>().ReverseMap();
            CreateMap<NhaCungCap, NhaCungCapDTO>().ReverseMap();
            CreateMap<NhanVien, NhanVienDTO>()
            .ForMember(dest => dest.Ho, opt => opt.MapFrom(src =>
                src.NguoiDung != null && !string.IsNullOrWhiteSpace(src.NguoiDung.Ho)
                ? src.NguoiDung.Ho : ""))
            .ForMember(dest => dest.Ten, opt => opt.MapFrom(src =>
                src.NguoiDung != null && !string.IsNullOrWhiteSpace(src.NguoiDung.Ten)
                ? src.NguoiDung.Ten : ""))
            .ForMember(dest => dest.Sdt, opt => opt.MapFrom(src =>
                src.NguoiDung != null && !string.IsNullOrWhiteSpace(src.NguoiDung.Sdt)
                ? src.NguoiDung.Sdt : ""))
            .ForMember(dest => dest.Gmail, opt => opt.MapFrom(src =>
                src.NguoiDung != null && !string.IsNullOrWhiteSpace(src.NguoiDung.Gmail)
                ? src.NguoiDung.Gmail : ""))
            .ForMember(dest => dest.TenChucVu, opt => opt.MapFrom(src =>
                src.ChucVu != null && !string.IsNullOrWhiteSpace(src.ChucVu.Ten)
                ? src.ChucVu.Ten : ""))
            .ForMember(dest => dest.NgaySinh, opt => opt.MapFrom(src =>
                src.NguoiDung != null ? src.NguoiDung.NgaySinh : DateTime.Now))
            .ReverseMap(); 

            CreateMap<NhanVienDTO,NhanVien>()
                .ForMember(dest => dest.NguoiDung, opt => opt.Ignore());


            CreateMap<TaiKhoan, TaiKhoanDTO>()
            .ForMember(dest => dest.Quyen, opt => opt.MapFrom(src =>
                src.NguoiDung != null &&
                src.NguoiDung.NhanVien != null &&
                src.NguoiDung.NhanVien.ChucVu != null &&
                !string.IsNullOrWhiteSpace(src.NguoiDung.NhanVien.ChucVu.Ten)
                    ? src.NguoiDung.NhanVien.ChucVu.Ten
                    : "User"

            ))
            .ForMember(tt => tt.TrangThai, opt => opt.MapFrom(src =>
                src.NguoiDung != null &&
                src.NguoiDung.NhanVien != null &&
                src.NguoiDung.NhanVien.ChucVu != null &&
                !string.IsNullOrWhiteSpace(src.NguoiDung.NhanVien.ChucVu.Ten)
                    ? src.NguoiDung.NhanVien.ChucVu.TrangThai : false
            ))
            .ForMember(id => id.Id , opt => opt.MapFrom(src => 
                src.NguoiDung != null &&
                src.NguoiDung.NhanVien != null ?
                src.NguoiDung.NhanVien.Id : ""));
            CreateMap<TaiKhoan, TaiKhoanKHDTO>()
            .ForMember(dest => dest.Quyen, opt => opt.MapFrom(src =>
                src.NguoiDung != null &&
                src.NguoiDung.NhanVien != null &&
                src.NguoiDung.NhanVien.ChucVu != null &&
                !string.IsNullOrWhiteSpace(src.NguoiDung.NhanVien.ChucVu.Ten)
                    ? src.NguoiDung.NhanVien.ChucVu.Ten
                    : "User"

            ))
            .ForMember(tt => tt.TrangThai, opt => opt.MapFrom(src =>
                src.NguoiDung != null &&
                src.NguoiDung.KhachHang != null 
                    ? src.NguoiDung.KhachHang.TrangThai : false
            ))
            .ForMember(id => id.Id, opt => opt.MapFrom(src =>
                src.NguoiDung != null &&
                src.NguoiDung.KhachHang != null ?
                src.NguoiDung.KhachHang.Id : ""));
            CreateMap<TaiKhoanDTO, TaiKhoan>();
            CreateMap<TheLoai, TheLoaiDTO>().ReverseMap();
            CreateMap<ThuongHieu, ThuongHieuDTO>().ReverseMap();

        }
    }
}
