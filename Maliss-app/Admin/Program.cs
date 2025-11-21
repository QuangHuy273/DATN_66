using Admin.Components;
using Admin.Service;
using Admin.Service.IService;
using API.Data;
using API.HeThong;
using API.HeThong.IHeThong;
using API.Models.DTO;
using API.Repository;
using API.Repository.IRepository;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string url = "https://localhost:5021/api/";
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(url) });


builder.Services.AddDbContext<DBAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHttpClient<IApiService, ApiService>(client =>
{
    client.BaseAddress = new Uri(url);
});

builder.Services.AddScoped<XulyId>();

builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options => options.LoginPath = "/");

builder.Services.AddScoped<ITaiKhoanService, TaiKhoanService>();
builder.Services.AddScoped<IUploadService, ImageUploadService>();
builder.Services.AddScoped<IMonAnService, MonAnService>();
builder.Services.AddScoped<IChiTietMonAnService, ChiTietMonAnService>();
builder.Services.AddScoped<IKhachHangService, KhachHangService>();
builder.Services.AddScoped<INhanVienService, NhanVienService>();
builder.Services.AddScoped<IHoaDonService, HoaDonService>();
builder.Services.AddScoped<IHoaDonRepository, HoaDonRepository>();
builder.Services.AddScoped<IHoaDonChiTietService, HoaDonChiTietService>();
builder.Services.AddScoped<IVnPayService, VnPayService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IXuLyDiaChi, XuLyDiaChi>();
builder.Services.AddHttpClient();
builder.WebHost.UseUrls("http://localhost:6005", "https://localhost:6006");
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddServerSideBlazor().AddCircuitOptions(options =>
{
    options.DetailedErrors = true;
});

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AppStateService>();
builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddAuthorizationCore();
builder.Services.AddHttpContextAccessor();
builder.Services.AddBlazoredLocalStorage();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DBAppContext>();
    dbContext.Database.Migrate();
    DbInitializer.SeedData(dbContext);
}
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.Run();
