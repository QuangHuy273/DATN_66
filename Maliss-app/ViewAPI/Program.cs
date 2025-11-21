using API.Data;
using API.HeThong;
using API.Models.DTO;
using API.Repository;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Data.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DBAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                         sqlOptions => sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
//string dbFilePath = "dbconfig.txt";
//string Dbcheck = "check.txt";
//string Db = "";
//string check = "";


//if (File.Exists(Dbcheck))
//{
//    check = File.ReadAllText(Dbcheck).Trim();
//    Db = File.ReadAllText(dbFilePath).Trim();
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

//builder.Services.AddDbContext<DBAppContext>(options =>
//    options.UseSqlServer(connectionString));




builder.Services.AddScoped<XulyId>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
builder.Services.AddScoped<ITaiKhoanRepository, TaiKhoanRepository>();
builder.Services.AddScoped<INhanVienRepository, NhanVienRepository>();
builder.Services.AddScoped<IChiTietMonAnRepository, ChiTietMonAnRepository>();
builder.Services.AddScoped<IKhachHangRepository, KhachHangRepository>();
builder.Services.AddScoped<IChiTietHoaDonRepository, ChiTietHoaDonRepository>();
builder.Services.AddScoped<IHoaDonRepository, HoaDonRepository>();
builder.Services.AddScoped<IMonAnRepository, MonAnRepository>();
builder.Services.AddAuthorization();
builder.WebHost.UseUrls("https://localhost:5021");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Images");
if (!Directory.Exists(uploadPath))
{
    Directory.CreateDirectory(uploadPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Uploads", "Images")),
    RequestPath = "/Uploads/Images"
});


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();