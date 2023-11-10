using BusinessLayer.Extensions;
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.Extensions;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.LoadDataLayerExtension(builder.Configuration);
builder.Services.LoadBusinessLayerExtension(); // configi kald�rd���m�z i�in bizden istemedi configurationu
builder.Services.AddSession();
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
//builder.Services.AddDbContext<ProjeContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnetion")));//appsettings te db yolunu vermi�sen burdan da kod b�yle yaz�l�r , bunu extensions s�n�f�na ta��d�m

builder.Services.AddIdentity<AppUser, AppRole>(opt => //AppUser, AppRole bu s�n�flar� olu�turmau�na bunlar� ya olu�turmam��san �dentityuser yada �dentityrole yaz �dentity�n kensi s�n�flar�n� yani
{
    opt.Password.RequireNonAlphanumeric = false; // numara zorunlulu�u
    opt.Password.RequireLowercase = false; //k���k harf zorunlulu�u
    opt.Password.RequireUppercase = false;//buyuk harf zorunlulu�u , projeyi canl�ya ta��d���n zaman bunlar� kald�r.
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
    config.ExpireTimeSpan= TimeSpan.FromDays(1);//bu cookie nin sistemde ne kadar kay�t�l�� kalacak 1 g�n oturum bir g�n bayunca ac�k kalacak yani
    config.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied"); /// hata safyas� gibi bi�ey asl�nda 
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
app.UseAuthentication();// s�ralama b�yle olmas� laz�m, sisteme login olan birinin bigsini tutar
app.UseAuthorization();

app.MapControllerRoute(
         name: "areas",
         pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
       );


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
