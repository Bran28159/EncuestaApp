global using Microsoft.EntityFrameworkCore;
using EncuestaDisenio.Components;
using EncuestaDisenio.DATA;
using Microsoft.JSInterop;
using OfficeOpenXml;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//global using Microsoft.EntityFrameworkCore;
builder.Services.AddRazorPages();//después de esta linea
QuestPDF.Settings.License = LicenseType.Community;

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
});
builder.Services.AddDbContext<ApplicationDbContextEncuesta>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("EncuestaConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("EncuestaConnection"))
    );
});
builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("https://localhost:44316/"); // usa tu puerto HTTPS real
});

// Esto permite inyectar HttpClient normal
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("API"));



builder.Services.AddHttpClient();

var app = builder.Build();


// Limpiar localStorage al arrancar (se ejecuta solo una vez)
using (var scope = app.Services.CreateScope())
{
    var jsRuntime = scope.ServiceProvider.GetRequiredService<IJSRuntime>();

}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
