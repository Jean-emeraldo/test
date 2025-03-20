using CrudDotNet.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


// Configuration de la chaîne de connexion et du DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                     new MySqlServerVersion(new Version(8, 0, 21))));


// Ajouter les services MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware de gestion des erreurs
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

// Configurer les routes pour le contrôleur User
app.MapControllerRoute(
    name: "default",
    // pattern: "{controller=Home}/{action=Index}");
    pattern: "{controller=User}/{action=Index}/{id?}");  // La route par défaut va diriger vers le contrôleur User et l'action Index

app.Run();
