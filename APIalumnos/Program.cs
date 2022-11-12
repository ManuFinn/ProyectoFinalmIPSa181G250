using APIalumnos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<FestifyContext>(opt =>
//opt.UseSqlServer(
//            Configuration.GetConnectionString("Festify")));

builder.Services.AddDbContext<tecContext>(opt =>
opt.UseMySql("server=127.0.0.1;database=tec;user=root;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.10.1-mariadb")));

builder.Services.AddMvc();

builder.Services.AddControllers();

var app = builder.Build();



app.UseDeveloperExceptionPage();

app.UseRequestLocalization("es-MX");

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});


app.Run();
