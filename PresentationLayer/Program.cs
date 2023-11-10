using BusinessLayer.Extensions;
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.Extensions;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.LoadDataLayerExtension(builder.Configuration);
builder.Services.LoadBusinessLayerExtension(); // configi kaldýrdýðýmýz için bizden istemedi configurationu
builder.Services.AddSession();
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
//builder.Services.AddDbContext<ProjeContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnetion")));//appsettings te db yolunu vermiþsen burdan da kod böyle yazýlýr , bunu extensions sýnýfýna taþýdým

builder.Services.AddIdentity<AppUser, AppRole>(opt => //AppUser, AppRole bu sýnýflarý oluþturmauþna bunlarý ya oluþturmamýþsan ýdentityuser yada ýdentityrole yaz ýdentityýn kensi sýnýflarýný yani
{
    opt.Password.RequireNonAlphanumeric = false; // numara zorunluluðu
    opt.Password.RequireLowercase = false; //küçük harf zorunluluðu
    opt.Password.RequireUppercase = false;//buyuk harf zorunluluðu , projeyi canlýya taþýdýðýn zaman bunlarý kaldýr.
})
    .AddRoleManager<RoleManager<AppRole>>()
    .AddEntityFrameworkStores<ProjeContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = new PathString("/Admin/Auth/Login");
    config.LogoutPath = new PathString("/Admin/Auth/Logoutt");
    config.Cookie = new CookieBuilder
    {
        Name="BlogProje",
        HttpOnly=true,
        SameSite=SameSiteMode.Strict,
        SecurePolicy=CookieSecurePolicy.SameAsRequest//Always
    };
    config.SlidingExpiration = true;
    config.ExpireTimeSpan= TimeSpan.FromDays(1);//bu cookie nin sistemde ne kadar kayýtýlýý kalacak 1 gün oturum bir gün bayunca acýk kalacak yani
    config.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied"); /// hata safyasý gibi biþey aslýnda 
});


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
app.UseSession();
app.UseRouting();
app.UseAuthentication();// sýralama böyle olmasý lazým, sisteme login olan birinin bigsini tutar
app.UseAuthorization();

app.MapControllerRoute(
         name: "areas",
         pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
       );


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
