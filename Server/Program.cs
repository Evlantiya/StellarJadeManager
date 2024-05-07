using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IPatchRepository, LocalPatchRepositroy>();
builder.Services.AddScoped<IWarpService, WarpService>();
builder.Services.AddMemoryCache();
builder.Services.AddCors();
builder.Services.AddHttpClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
// app.UseCors(builder => builder.AllowAnyOrigin());
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");
// app.Use(async (context, next) =>
//             {
//                 await Task.Delay(1000000000);
//                 await next.Invoke();
//                 await Task.Delay(35);
//             });

app.Run();
