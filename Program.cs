using MustDo;
using System.Text.Json.Serialization;

Setup.OnStart();

var builder = WebApplication.CreateBuilder(args);

if (!Setup.AppConfig.ApiConfig.IsDevelopment) {
    builder.WebHost.UseKestrel(hostBuilder => {
        hostBuilder.ListenAnyIP(Setup.AppConfig.ApiConfig.Port);
    });
}

builder.Services.AddControllersWithViews();

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();