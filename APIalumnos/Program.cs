using APIalumnos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<itesrcne_jeancarloContext>(opt =>
opt.UseMySql("server=204.93.216.11;database=itesrcne_jeancarlo;user=itesrcne_jeancar;password=2G@4ykMwqR3xyCZ", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.10.1-mariadb")));

builder.Services.AddCors();

builder.Services.AddMvc();

builder.Services.AddControllers();



var app = builder.Build();

app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true)
                    .AllowCredentials()
                    );

app.UseDeveloperExceptionPage();

app.UseRequestLocalization("es-MX");

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});



app.Run();
