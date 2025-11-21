using API.Data;
using API.HeThong;
using API.HeThong.IHeThong;
using API.Repository;
using API.Repository.IRepository;
using Blazored.LocalStorage;
using JollyWeb.Components;
using JollyWeb.Service;
using JollyWeb.Service.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string url = "https://localhost:7047/api/";
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(url) });


builder.Services.AddDbContext<DBAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHttpClient<IApiService, ApiService>(client =>
{
    client.BaseAddress = new Uri(url);
});
builder.Services.AddScoped<ITaiKhoanService, TaiKhoanService>();
builder.Services.AddScoped<IUploadService, ImageUploadService>();
builder.Services.AddScoped<IMonAnService, MonAnService>();
builder.Services.AddScoped<IChiTietMonAnService, ChiTietMonAnService>();
builder.Services.AddScoped<IKhachHangService, KhachHangService>();
builder.Services.AddScoped<INhanVienService, NhanVienService>();
builder.Services.AddScoped<IHoaDonService, HoaDonService>();
builder.Services.AddScoped<IHoaDonRepository, HoaDonRepository>();
builder.Services.AddScoped<IVnPayService, VnPayService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IXuLyDiaChi, XuLyDiaChi>();
builder.Services.AddScoped<XulyId>();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.Lax;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
