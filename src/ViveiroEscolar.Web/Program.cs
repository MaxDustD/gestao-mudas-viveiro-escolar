using ViveiroEscolar.Library.Infra.Memory;
using ViveiroEscolar.Library.Application.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// register memory repositories and application service
builder.Services.AddSingleton<MemoryEspecieRepository>();
builder.Services.AddSingleton<MemoryCanteiroRepository>();
builder.Services.AddSingleton<MemoryResponsavelRepository>();
builder.Services.AddSingleton<MemoryLoteRepository>();
builder.Services.AddSingleton<ViveiroApplicationService>(sp =>
{
    var e = sp.GetRequiredService<MemoryEspecieRepository>();
    var c = sp.GetRequiredService<MemoryCanteiroRepository>();
    var r = sp.GetRequiredService<MemoryResponsavelRepository>();
    var l = sp.GetRequiredService<MemoryLoteRepository>();
    return new ViveiroApplicationService(e, c, r, l);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
