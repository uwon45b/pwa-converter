using pwa_converter.Models;
using pwa_converter.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IPwaControllerService, PwaControllerService>();
builder.Services.AddTransient<IHomeControllerService, HomeControllerService>();
builder.Services.AddTransient<ILighthouseAuditResultContainer, LighthouseAuditResultContainer>();
builder.Services.AddTransient<IImagesControllerService, ImagesControllerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
