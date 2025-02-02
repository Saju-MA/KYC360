using KYC360.Web.Services;
using KYC360.Web.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<ISecretUrlService, SecretUrlService>();

#pragma warning disable CS8601 // Possible null reference assignment.
Utility.ServicesAPIBase = builder.Configuration["ServiceUrls:ServicesAPI"];
#pragma warning restore CS8601 // Possible null reference assignment.

builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<ISecretUrlService, SecretUrlService>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=AdminPage}/{action=Index}/{id?}");

app.Run();
