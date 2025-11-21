using API.Data;
using API.HeThong;
using API.Models.DTO;

//using API.Models.DTO;
using API.Repository;
using API.Repository.IRepository;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<DBAppContext>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DBAppContext>();

    if (context != null)
    {
        DbInitializer.SeedData(context); // Call your seeding logic
    }
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.Run();
