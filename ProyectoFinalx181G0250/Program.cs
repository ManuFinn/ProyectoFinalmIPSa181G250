var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseFileServer();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
    endpoints.MapRazorPages()
);

app.Run();
